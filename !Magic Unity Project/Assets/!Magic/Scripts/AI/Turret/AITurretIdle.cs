using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITurretIdle : ABehaviour
{
    public override void OnBehaviourStart()
    {
        InitTurret();

        m_updateTimer = 0;

        m_data.state = OnBehaviourUpdate;
    }

    public override void OnBehaviourUpdate()
    {
        m_updateTimer += Time.deltaTime;

        //animate head to "search".

        //raycast should always fire forward.

        //once detected, should lock onto player until out of sight again.

        //look to which state to go from here.
        if (PlayerInLineOfSight())
        {
            TransitionBehaviour(AIData.Behaviour.Attack);
        }
    }

    public override void OnBehaviourEnd()
    {

    }
}
