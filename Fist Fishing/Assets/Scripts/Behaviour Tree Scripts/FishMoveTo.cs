using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class FishMoveTo : FishTask
{
    [SerializeField]
    protected Vector3 m_direction;
    [SerializeField]
    protected float m_forgetfulness = 0.25f;

    public override NodeResult Execute()
    {
        ReadInfo();

        if (Vector3.Distance(m_me.transform.position, m_target.transform.position) < m_accuracy)
            return NodeResult.SUCCESS;

        ResolveAwareness();
        CollisionAvoidance();
        MoveToward();

        return NodeResult.RUNNING;
    }

    protected Dictionary<GameObject, FishReaction> RemeberHistory()
    {
        Dictionary<GameObject, FishReaction> myHistory = (Dictionary<GameObject, FishReaction>)m_tree.GetValue(FishBrain.MoodName);

        myHistory.Keys.ToList<GameObject>().ForEach(
            rememberedObject =>
            {
                FishReaction nextReaction = myHistory[rememberedObject];
                //forgets /reduces intensity 
                nextReaction.m_intensity *= 1 - (m_forgetfulness * Time.deltaTime); 

                if (Mathf.Abs(nextReaction.m_intensity) < 0.00007)
                    myHistory.Remove(rememberedObject);
            });



        return myHistory;
    }

    protected void ResolveAwareness()
    {
        m_direction = m_target.transform.position - m_me.transform.position;

        var pointsOfIntrest = RemeberHistory();
        int numberOfIntrests = pointsOfIntrest.Count;
        if (numberOfIntrests == 0)
            return;

        Vector3 averageGoal = (
                m_target.transform.position
                + pointsOfIntrest.Select(kvp => CalculateDirectionWeight(kvp.Key.transform.position, kvp.Value))
                    .Aggregate((subtotal, pointOfIntrest) => subtotal += pointOfIntrest)
                    ) / (
                    pointsOfIntrest.Select(kvp => kvp.Value.m_intensity).Sum() + 1
                    );

        m_direction = averageGoal - m_me.transform.position;
    }
    public enum FishResponse
    {
        /// <summary>
        /// move away
        /// </summary>
        Avoid,
        /// <summary>
        /// move towards
        /// </summary>
        Attracted,
        /// <summary>
        /// No influence
        /// </summary>
        Indifferent
    }

    public static Dictionary<FishBrain.FishClassification, Dictionary<FishBrain.FishClassification, FishResponse>> awayReactionDirection =
        new Dictionary<FishBrain.FishClassification, Dictionary<FishBrain.FishClassification, FishResponse>>()
        {
            {//fearful fish influence resolution
                FishBrain.FishClassification.Fearful|FishBrain.FishClassification.BaitSensitive1,
                new Dictionary<FishBrain.FishClassification, FishResponse>()
                {
                    { FishBrain.FishClassification.BaitSensitive1, FishResponse.Attracted },
                    { FishBrain.FishClassification.Player, FishResponse.Avoid }
                }
            },
            {//meh fish behavoir
                FishBrain.FishClassification.Passive,
                new Dictionary<FishBrain.FishClassification, FishResponse>()
                {
                    { FishBrain.FishClassification.BaitSensitive1, FishResponse.Indifferent },
                    { FishBrain.FishClassification.Player, FishResponse.Indifferent }
                }
            },
            {//Attacked meh fish behavoir
                FishBrain.FishClassification.Passive| FishBrain.FishClassification.Agressive,
                new Dictionary<FishBrain.FishClassification, FishResponse>()
                {
                    { FishBrain.FishClassification.BaitSensitive1, FishResponse.Indifferent },
                    { FishBrain.FishClassification.Player, FishResponse.Attracted }
                }
            },
            {//Agressive fish behavoir
                FishBrain.FishClassification.Agressive,
                new Dictionary<FishBrain.FishClassification, FishResponse>()
                {
                    { FishBrain.FishClassification.BaitSensitive1, FishResponse.Indifferent },
                    { FishBrain.FishClassification.Player, FishResponse.Attracted }
                }
            }
        };
    /// <summary>
    /// for averaging it should ignore indifferent intensities
    /// </summary>
    /// <param name="reaction"> the reaaction involved</param>
    /// <returns>returns 0 if the fish is indiffernt otherwise returns intensity</returns>
    protected float CalculateDirectionIntensity(FishReaction reaction)
    {
        BasicFish myfish = m_me.GetComponent<BasicFish>();
        Dictionary<FishBrain.FishClassification, FishResponse> myMood;
        FishResponse response;
        if ((awayReactionDirection.TryGetValue(myfish.FishClass, out myMood)) &&
           (myMood.TryGetValue(reaction.m_fishReconition, out response)) &&
            (response == FishResponse.Indifferent))
        {
            return 0;
        }
        return reaction.m_intensity;
    }

    protected Vector3 CalculateDirectionWeight(Vector3 pointOfIntrest, FishReaction reaction)
    {
        BasicFish myfish = m_me.GetComponent<BasicFish>();
        Vector3 directionOfIntrest = pointOfIntrest;
        Dictionary<FishBrain.FishClassification, FishResponse> myMood;
        FishResponse response;
        if ((awayReactionDirection.TryGetValue(myfish.FishClass, out myMood))&&
           (myMood.TryGetValue(reaction.m_fishReconition, out response)))
        {
            if (response == FishResponse.Avoid)
                directionOfIntrest = Vector3.Reflect(directionOfIntrest, m_direction);
            if (response == FishResponse.Indifferent)
                directionOfIntrest = Vector3.zero;
        }
        return directionOfIntrest * reaction.m_intensity; 
    }

    protected void CollisionAvoidance()
    {
        RaycastHit hit;
        BasicFish myfish = m_me.GetComponent<BasicFish>();

        if (!Physics.Raycast(myfish.LookFrom.position, m_direction, out hit, m_speed * 2))
            return;

        m_direction = Vector3.Reflect(m_direction, hit.normal);
    }

    protected void MoveToward()
    {
        Quaternion turnDirection = Quaternion.FromToRotation(Vector3.forward, m_direction);
        m_me.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, turnDirection, Time.deltaTime * m_turnSpeed);
        // then move

        m_me.transform.position = m_me.transform.position + m_me.transform.forward * m_speed * Time.deltaTime;
    }

}