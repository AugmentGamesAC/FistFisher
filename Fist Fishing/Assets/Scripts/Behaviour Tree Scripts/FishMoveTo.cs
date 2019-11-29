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
    public static Dictionary<FishBrain.FishClassification, Dictionary<FishBrain.FishClassification, bool>> awayReactionDirection =
        new Dictionary<FishBrain.FishClassification, Dictionary<FishBrain.FishClassification, bool>>()
        {
            {//simple fish behavior
                FishBrain.FishClassification.Fearful|FishBrain.FishClassification.BaitSensitive1,
                new Dictionary<FishBrain.FishClassification, bool>()
                {
                    { FishBrain.FishClassification.BaitSensitive1, false },
                    { FishBrain.FishClassification.Player, true }
                }
            }
        };

    protected Vector3 CalculateDirectionWeight(Vector3 pointOfIntrest, FishReaction reaction)
    {
        BasicFish myfish = m_me.GetComponent<BasicFish>();
        Vector3 directionOfIntrest = pointOfIntrest;
        Dictionary<FishBrain.FishClassification, bool> myMood;
        bool runaway;
        if (awayReactionDirection.TryGetValue(myfish.FishClass, out myMood))
            if (myMood.TryGetValue(reaction.m_fishReconition, out runaway))
                if (runaway)
                    directionOfIntrest = Vector3.Reflect(directionOfIntrest, m_direction);

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