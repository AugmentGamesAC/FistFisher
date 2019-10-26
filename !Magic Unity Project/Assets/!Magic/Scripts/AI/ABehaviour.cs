using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABehaviour : MonoBehaviour
{
    protected AIData m_data;

    protected float m_updateTimer;
    protected float m_updateDelay;
    //searching in place time

    //Line to point at player to see if he's within range.
    protected RaycastHit m_Hit;
    protected Ray m_Ray;
    protected LineRenderer m_Line;

    //Must implement these for each new behaviour.
    public abstract void OnBehaviourStart();
    public abstract void OnBehaviourUpdate();
    public abstract void OnBehaviourEnd();

    protected void Init()
    {
        m_data = GetComponent<AIData>();
        m_updateDelay = 1.0f;
        m_data.m_agent.stoppingDistance = 2.0f;
        m_Line = gameObject.GetComponent<LineRenderer>();
    }

    protected virtual bool PlayerInLineOfSight()
    {
        Vector3 direction = m_data.followObject.position - transform.position;

        m_Ray = new Ray(transform.position, direction);

        m_Line.enabled = true;
        m_Line.SetPosition(0, m_Ray.origin);

        if (Physics.Raycast(m_Ray, out m_Hit, 3000))
        {
            //if target is player and distance between the two 
            if (m_Hit.collider.tag == "Player Target" &&
                (m_Hit.distance < m_data.sightRange))
            {
                m_Line.SetPosition(1, m_Hit.point);

                //Set to Follow for now.
                return true;
            }
        }
        return false;
    }

    protected virtual bool PlayerInAttackRange()
    {
        //if distance to the player is smaller than attack range, return true.
        if (Vector3.SqrMagnitude(m_data.followObject.position - m_data.transform.position) <
            m_data.attackRange * m_data.attackRange)
        {
            return true;
        }
        return false;
    }

    protected virtual void TransitionBehaviour(AIData.Behaviour behaviour)
    {
        m_data.m_currentBehaviour = behaviour;
    }
}
