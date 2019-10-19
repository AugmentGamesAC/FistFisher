using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFollow : ABehaviour
{


    //Initial function.
    public override void OnBehaviourStart()
    {
        Init();
        //if distance between the two is smaller than the max distance.
        if (PlayerInLineOfSight())
        {
            m_data.m_agent.SetDestination(m_data.followObject.position);
            m_updateTimer = 0;
            m_data.state = OnBehaviourUpdate;
        }
        else
        {
            m_data.m_currentBehaviour = AIData.Behaviour.Idle;
        }
    }


    public override void OnBehaviourUpdate()
    {
        m_updateTimer += Time.deltaTime;

        //if too far from me, go to idle, or in the future, go to attack and change min distanceToPoint to the AI's attack range.
        if (Vector3.SqrMagnitude(m_data.followObject.position - m_data.transform.position) >
            m_data.maxDistToPlayer * m_data.maxDistToPlayer)
        {
            //go into Idle
            m_data.m_currentBehaviour = AIData.Behaviour.Idle;
        }
        //if in attack range, switch to attack behaviour.
        else if(Vector3.SqrMagnitude(m_data.followObject.position - m_data.transform.position) <
            m_data.minDistToPlayer * m_data.minDistToPlayer)
        {
            //m_data.m_currentBehaviour = AIData.Behaviour.Attack;
        }
        else if (m_updateTimer > m_updateDelay && PlayerInLineOfSight())
        {
            m_data.m_agent.SetDestination(m_data.followObject.position);
            m_updateTimer = 0;
        }
    }

    public override void OnBehaviourEnd()
    {
        //empty for now.
    }
}
