using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInstance : IPlayerData
{
    [SerializeField]
    protected PlayerHealth m_health;
    public PlayerHealth Health => m_health;

    [SerializeField]
    protected OxygenTracker m_oxygen;
    public OxygenTracker Oxygen => m_oxygen;

    [SerializeField]
    protected float m_attackRange;
    public float AttackRange => m_attackRange;

    [SerializeField]
    protected Sprite m_iconDisplay;
    public Sprite IconDisplay => m_iconDisplay;
}
