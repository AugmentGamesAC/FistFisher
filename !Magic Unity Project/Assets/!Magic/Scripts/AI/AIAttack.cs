using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : ABehaviour
{
    public Sword m_sword;

    public override void OnBehaviourStart()
    {
        Init();

        //get sword component
        m_sword = GetComponentInChildren<Sword>();

        m_updateDelay = 0.0f;

        //perform attack on the animator
        //m_sword.PerformAttack();

        m_data.state = OnBehaviourUpdate;
    }

    public override void OnBehaviourUpdate()
    {
        m_updateTimer += Time.deltaTime;
        //if player is dead, go to idle or patrol.

        //if player is out of range, go to follow.
        if (!PlayerInAttackRange())
        {
            TransitionBehaviour(AIData.Behaviour.Follow);
        }
        else if(PlayerInAttackRange() && m_updateTimer > m_updateDelay)
        {
            m_sword.PerformAttack();
            m_updateTimer = 0;
            m_updateDelay = 3.0f;
        }
    }

    public override void OnBehaviourEnd()
    {

    }
}
