using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABehaviour : MonoBehaviour
{
    protected AIData m_data;
    protected float m_updateTimer;
    protected float m_updateDelay;

    public abstract void OnBehaviourStart();
    public abstract void OnBehaviourUpdate();
    public abstract void OnBehaviourEnd();

    protected void Init()
    {
        m_data = GetComponent<AIData>();
        m_updateDelay = 1.0f;
        m_data.m_agent.stoppingDistance = m_data.maxDistToPlayer;
    }
    protected void Init(AIData aiData, float updateDelay)
    {
        m_data = aiData;
        m_updateDelay = updateDelay;
    }
}
