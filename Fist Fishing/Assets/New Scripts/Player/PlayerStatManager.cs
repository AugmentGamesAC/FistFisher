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
public class PlayerStatManager
{
    [SerializeField]
    protected Dictionary<Stats, StatTracker> m_statTrackerContainer = new Dictionary<Stats, StatTracker>();

    public StatTracker this[Stats value] { get { return m_statTrackerContainer[value]; } }
    /// <summary>
    /// Sets StatTrackerContainer.
    /// </summary>
    public PlayerStatManager()
    {
        var ListOfStats = Enum.GetValues(typeof(Stats));
        foreach (var stat in ListOfStats)
        {
            AddStat((Stats)stat, new StatTracker());
        }
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

        m_statTrackerContainer[statType].SetMax(m_statTrackerContainer[statType].MaxValue + amount);

        return true;
    }

    public void SetTracker(Stats statType, StatTracker tracker)
    {
        m_statTrackerContainer[statType] = tracker;
    }
}
