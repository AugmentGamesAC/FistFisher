using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFollow : ABehaviour
{
    public override void OnBehaviourStart()
    {
        InitPatrolBot();
        //if distance between the two is smaller than the max distance.
        if (PlayerInLineOfSight())
        {
            m_data.m_agent.SetDestination(m_data.followObject.position);
            m_updateTimer = 0;
            m_data.state = OnBehaviourUpdate;
        }
        else
        {
            TransitionBehaviour(AIData.Behaviour.Idle);
        }
    }


    public override void OnBehaviourUpdate()
    {
        m_updateTimer += Time.deltaTime;

        //if too far from me, go to idle.
        if (!PlayerInLineOfSight())
        {
            TransitionBehaviour(AIData.Behaviour.Idle);
        }
        else if(PlayerInAttackRange())
        {
            TransitionBehaviour(AIData.Behaviour.Attack);
        }
        else if (m_updateTimer > m_updateDelay && PlayerInLineOfSight())
        {
            m_data.m_agent.SetDestination(m_data.followObject.position);
            m_updateTimer = 0;
        }
        
        //if player is out of line of sight for certain amount of time, go to Idle.
    }

    public override void OnBehaviourEnd()
    {
        //empty for now.
    }
}
