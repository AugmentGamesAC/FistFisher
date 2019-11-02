using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : ABehaviour
{
    public override void OnBehaviourStart()
    {
        InitPatrolBot();

        m_updateTimer = 0;

        m_data.state = OnBehaviourUpdate;
    }

    public override void OnBehaviourUpdate()
    {
        m_updateTimer += Time.deltaTime;

        //look to which state to go from here.
        if (PlayerInLineOfSight())
        //m_data.m_currentBehaviour = AIData.Behaviour.Follow;
        //m_data.m_currentBehaviour = AIData.Behaviour.FlyTo; //need a smarter switch
        {
            //if (m_data.m_isFlying)
                //TransitionBehaviour(AIData.Behaviour.FlyTo);
            //else
                TransitionBehaviour(AIData.Behaviour.Follow);
        }
        //if player if player is out of line of sight for certain amount of time, go to patrol.
        else if(m_updateTimer > m_data.patrolDelay)
        {
            TransitionBehaviour(AIData.Behaviour.Patrol);
            m_updateTimer = 0;
        }
    }
    public override void OnBehaviourEnd()
    {

    }
}
