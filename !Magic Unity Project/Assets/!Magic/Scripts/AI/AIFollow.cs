using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFollow : ABehaviour
{


    //Initial function.
    public override void OnBehaviourStart()
    {
        Init();

        if (Vector3.SqrMagnitude(m_data.followObject.position - m_data.m_AItransform.position) > m_data.maxDistToPlayer * m_data.maxDistToPlayer)
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

    public override void OnBehaviourEnd()
    {
        //empty for now.
    }

    public override void OnBehaviourUpdate()
    {
        m_updateTimer += Time.deltaTime;

        //if too close to me, go to idle, or in the future, go to attack and change min distanceToPoint to the AI's attack range.
        if (Vector3.SqrMagnitude(m_data.followObject.position - m_data.m_AItransform.position) < m_data.maxDistToPlayer * m_data.maxDistToPlayer)
        {
           // m_data.state = IdleFunction;
            //data.currentBehaviour = AIData.Behaviour.Idle;
        }
        else if (m_updateTimer > m_updateDelay)
        {
            m_data.m_agent.SetDestination(m_data.followObject.position);
            m_updateTimer = 0;
        }
    }
}
