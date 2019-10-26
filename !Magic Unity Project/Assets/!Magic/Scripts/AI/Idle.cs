using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : ABehaviour
{
    public override void OnBehaviourStart()
    {
        Init();

        m_updateTimer = 0;

        m_data.state = OnBehaviourUpdate;
    }

    public override void OnBehaviourUpdate()
    {
        m_updateTimer += Time.deltaTime;

        //look to which state to go from here.
        if (PlayerInLineOfSight())
        {
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
