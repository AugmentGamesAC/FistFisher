using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stats
{
    MaxHealth,
    Stealth,
    Power,
    MaxAir,
    AirConsumption,
    AirRestoration,
    MovementSpeed,
    TurnSpeed
}
[System.Serializable]
public class PlayerStatManager
{
    [SerializeField]
    protected static Dictionary<Stats, StatTracker> m_statTrackerContainer = new Dictionary<Stats, StatTracker>();

    [SerializeField]
    protected float m_baseMaxHealth;
    public float BaseMaxHealth => m_baseMaxHealth;
    [SerializeField]
    protected float m_baseStealth;
    public float BaseStealth => m_baseStealth;
    [SerializeField]
    protected float m_basePower;
    public float BasePower => m_basePower;
    [SerializeField]
    protected float m_baseMaxAir;
    public float BaseMaxAir => m_baseMaxAir;
    [SerializeField]
    protected float m_baseAirConsumption;
    public float BaseAirConsumption => m_baseAirConsumption;
    [SerializeField]
    protected float m_baseAirRestoration;
    public float BaseAirRestoration => m_baseAirRestoration;
    [SerializeField]
    protected float m_baseMoveSpeed;
    public float BaseMoveSpeed => m_baseMoveSpeed;
    [SerializeField]
    protected float m_baseTurnSpeed;
    public float BaseTurnSpeed => m_baseTurnSpeed;

    public StatTracker this[Stats value] { get { return m_statTrackerContainer[value]; } }

    public void Init()
    {
        m_statTrackerContainer = new Dictionary<Stats, StatTracker>()
        {
            { Stats.MaxHealth, new StatTracker(m_baseMaxHealth) },
            { Stats.Stealth, new StatTracker(m_baseStealth) },
            { Stats.Power, new StatTracker(m_basePower) },
            { Stats.MaxAir, new StatTracker(m_baseMaxAir) },
            { Stats.AirConsumption, new StatTracker(m_baseAirConsumption) },
            { Stats.AirRestoration, new StatTracker(m_baseAirRestoration) },
            { Stats.MovementSpeed, new StatTracker(m_baseMoveSpeed) },
            { Stats.TurnSpeed, new StatTracker(m_baseTurnSpeed) }
        };
    }

    /// <summary>
    /// Adds a KeyValuePair to the container.
    /// </summary>
    /// <param name="statType"></param>
    /// <param name="statTracker"></param>
    public void AddStat(Stats statType, StatTracker statTracker)
    {
        if (m_statTrackerContainer.ContainsKey(statType))
            return;

        m_statTrackerContainer.Add(statType, statTracker);
    }

    /// <summary>
    /// add amount to the stat tracker for statType
    /// </summary>
    /// <param name="trackerType"></param>
    /// <param name="amount"></param>
    public bool UpdateStat(Stats statType, float amount)
    {
        if (!m_statTrackerContainer.ContainsKey(statType))
            return false;

        m_statTrackerContainer[statType].SetValue(m_statTrackerContainer[statType].MaxValue + amount);

        return true;
    }

    public void SetTracker(Stats statType, StatTracker tracker)
    {
        m_statTrackerContainer[statType] = tracker;
    }
}
