using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : ABehaviour
{
    public GameObject[] patrolPoints;
    public List<Vector3> patrolPointPositions;
    public Vector3 closestPoint;
    public int patrolPointsIndex;

    public override void OnBehaviourStart()
    {
        Init();

        //m_data.m_currentBehaviour = AIData.Behaviour.Idle;
        //add wait at waypoint.

        //find all control points in the level if the list is empty.
        if (patrolPoints == null)
        {
            patrolPoints = GameObject.FindGameObjectsWithTag("Patrol Point");
        }


        //fill a list with all the collected patrol points positions.
        foreach (var point in patrolPoints)
        {
            patrolPointPositions.Add(point.transform.position);
        }

        //starts on first control point or closest patrol point.
        patrolPointsIndex = 0;
        FindClosestPatrolPoint();

        m_data.m_agent.SetDestination(patrolPointPositions[patrolPointsIndex]);

        m_data.state = OnBehaviourUpdate;
    }

    public override void OnBehaviourUpdate()
    {
        m_updateTimer += Time.deltaTime;

        //transition if player is in line of sight.
        if (PlayerInLineOfSight())
        {
            if (m_data.m_isFlying)
                TransitionBehaviour(AIData.Behaviour.FlyTo);
            else
                TransitionBehaviour(AIData.Behaviour.Follow);
            return;
        }

        //stall to look around.
        if (!m_data.m_agent.pathPending && 
            m_data.m_agent.remainingDistance < 2.0f &&
            m_updateTimer > m_data.patrolDelay)
        {
            GoToNextPoint();
            m_updateTimer = 0;
        } 
    }

    public override void OnBehaviourEnd()
    {

    }

    public void GoToNextPoint()
    {
        if (patrolPointPositions.Count == 0)
            return;

        patrolPointsIndex = (patrolPointsIndex + 1) % patrolPointPositions.Count;

        m_data.m_agent.SetDestination(patrolPointPositions[patrolPointsIndex]);
    }

    //gets closest patrol point and replaces the default index with the closest one. this should only happen in start so 
    public Vector3 FindClosestPatrolPoint()
    {

        float distanceToClosestPoint = Mathf.Infinity;

        //find closest patrol point in our list. List is empty so must be filled first.
        for (int i = 0; i < patrolPointPositions.Count; i++)
        {
            float distanceToPoint = (patrolPointPositions[i] - transform.position).sqrMagnitude;
            if (distanceToPoint < distanceToClosestPoint)
            {
                distanceToClosestPoint = distanceToPoint;
                closestPoint = patrolPointPositions[i];
                patrolPointsIndex = i;
            }
        }

        return closestPoint;
    }
}
