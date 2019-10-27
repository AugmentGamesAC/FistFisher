using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABehaviour : MonoBehaviour
{
    protected AIData m_data;
    protected float m_updateTimer;
    protected float m_updateDelay;
    protected RaycastHit m_Hit;
    protected Ray m_Ray;
    protected LineRenderer m_Line;




    public abstract void OnBehaviourStart();
    public abstract void OnBehaviourUpdate();
    public abstract void OnBehaviourEnd();

    protected void Init()
    {
        m_data = GetComponent<AIData>();
        m_updateDelay = 1.0f;
        m_data.m_agent.stoppingDistance = m_data.m_stoppingDistance;
        m_data.m_agent.speed = m_data.m_movementSpeed;
        m_data.m_agent.angularSpeed = m_data.m_angularSpeed;
        m_data.m_agent.acceleration = m_data.m_acceleration;
        m_Line = gameObject.GetComponent<LineRenderer>();
    }
    protected void Init(AIData aiData, float updateDelay)
    {
        m_data = aiData;
        m_updateDelay = updateDelay;
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
                (m_Hit.distance < m_data.maxDistToPlayer))
            {
                m_Line.SetPosition(1, m_Hit.point);

                //Set to Follow for now.
                return true;
            }
        }
        return false;
    }
}
