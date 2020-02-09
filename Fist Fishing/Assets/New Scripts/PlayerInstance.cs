using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstance 
{
    [SerializeField]
    protected IPlayerData m_playerDef;
    public IPlayerData playerData => m_playerDef;

    [SerializeField]
    protected PlayerHealth m_health;
    public PlayerHealth Health => m_health;

    [SerializeField]
    protected OxygenTracker m_oxygen;
    public OxygenTracker Oxygen => m_oxygen;


    public PlayerInstance(IPlayerData playerDef)
    {
        m_playerDef = playerDef;
        m_health = new PlayerHealth(m_playerDef.Health.Percentage.Max);
    }
}
