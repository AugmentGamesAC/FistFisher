using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITurretIdle : ABehaviour
{
    Animator m_turretAnimator;
    public override void OnBehaviourStart()
    {
        InitTurret();

        if (m_turretAnimator == null)
        {
            m_turretAnimator = GetComponent<Animator>();
        }
        m_turretAnimator.SetTrigger("Idle");
        m_turretAnimator.ApplyBuiltinRootMotion();

        m_updateTimer = 0;

        m_data.state = OnBehaviourUpdate;
    }

    public override void OnBehaviourUpdate()
    {
        m_updateTimer += Time.deltaTime;

        //if play gets hit by raycast, attack.
        if (PlayerInTurretSight())
        {
            TransitionBehaviour(AIData.Behaviour.Attack);
            //exit Idle animation.
            m_turretAnimator.SetTrigger("Found Player");
        }
    }

    public override void OnBehaviourEnd()
    {

    }
}
