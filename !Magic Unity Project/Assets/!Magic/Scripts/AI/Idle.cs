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

        if (PlayerInLineOfSight())
            m_data.m_currentBehaviour = AIData.Behaviour.Follow;
    }
    public override void OnBehaviourEnd()
    {

    }
}
