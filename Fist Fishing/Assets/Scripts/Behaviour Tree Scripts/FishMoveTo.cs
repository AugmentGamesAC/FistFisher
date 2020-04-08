using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// basic behaviour for fish that just told it where it was to go and moved there 
/// </summary>
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

        ResolveAwareness();
        CollisionAvoidance();
        MoveToward();

        return NodeResult.SUCCESS;
    }

    /// <summary>
    /// handles all influences on a fish to determine points of interest while moving
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// figures out next direction to apply transform to based on influences and target
    /// </summary>
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
                    pointsOfIntrest.Select(kvp => CalculateDirectionIntensity(kvp.Value)).Sum() + 1
                    );

        m_direction = averageGoal - m_me.transform.position;
    }

    /// <summary>
    /// Influences change by type of influence and fish type. 
    /// This enum determines what this fishes response is
    /// </summary>
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


    /// <summary>
    /// a dictionary of responses based on fish types
    /// </summary>
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
            {//meh fish behavior
                FishBrain.FishClassification.Passive,
                new Dictionary<FishBrain.FishClassification, FishResponse>()
                {
                    { FishBrain.FishClassification.BaitSensitive1, FishResponse.Indifferent },
                    { FishBrain.FishClassification.Player, FishResponse.Indifferent }
                }
            },
            {//Attacked meh fish behavior
                FishBrain.FishClassification.Passive| FishBrain.FishClassification.Aggressive,
                new Dictionary<FishBrain.FishClassification, FishResponse>()
                {
                    { FishBrain.FishClassification.BaitSensitive1, FishResponse.Indifferent },
                    { FishBrain.FishClassification.Player, FishResponse.Attracted }
                }
            },
            {//Aggressive fish behavior
                FishBrain.FishClassification.Aggressive,
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
           (myMood.TryGetValue(reaction.m_fishRecognition, out response)) &&
            (response == FishResponse.Indifferent))
        {
            return 0;
        }
        return reaction.m_intensity;
    }

    /// <summary>
    /// given all weights and intensities of influences/reaction, figures out direction for fish to move
    /// </summary>
    /// <param name="pointOfInterest"></param>
    /// <param name="reaction"></param>
    /// <returns></returns>
    protected Vector3 CalculateDirectionWeight(Vector3 pointOfInterest, FishReaction reaction)
    {
        BasicFish myfish = m_me.GetComponent<BasicFish>();
        Vector3 directionOfInterest = pointOfInterest;
        Dictionary<FishBrain.FishClassification, FishResponse> myMood;
        FishResponse response;
        if ((awayReactionDirection.TryGetValue(myfish.FishClass, out myMood))&&
           (myMood.TryGetValue(reaction.m_fishRecognition, out response)))
        {
            if (response == FishResponse.Avoid)
                directionOfInterest = Vector3.Reflect(directionOfInterest, m_direction);
            if (response == FishResponse.Indifferent)
                directionOfInterest = Vector3.zero;
        }
        return directionOfInterest * reaction.m_intensity; 
    }

    /// <summary>
    /// raycasts forwards to ensure fish does not collide with something
    /// </summary>
    protected void CollisionAvoidance()
    {

        RaycastHit hit;
        BasicFish myfish = m_me.GetComponent<BasicFish>();

        if (!Physics.Raycast(myfish.LookFrom.position, m_direction, out hit, m_speed * 2, ~LayerMask.GetMask("Player", "Ignore Raycast", "Water", "BoatMapOnly")))
            return;

        m_direction = Vector3.Reflect(m_direction, hit.normal);
    }

    /// <summary>
    /// given direction, rotates then applies forwards transform
    /// </summary>
    protected void MoveToward()
    {
        Quaternion turnDirection = Quaternion.FromToRotation(Vector3.forward, m_direction);
        m_me.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, turnDirection, Time.deltaTime * m_turnSpeed);
        // then move

        m_me.transform.position = m_me.transform.position + m_me.transform.forward * m_speed * Time.deltaTime;
    }

}