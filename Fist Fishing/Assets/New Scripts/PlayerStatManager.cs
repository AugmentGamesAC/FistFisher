using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager
{
    /*enum Stats;
    Dictionary<Stats, StatTracker> StatTrackerContainer;
--
AddStat();//for dictionary adding new objects.
    UpdateStat(Stats trackerType, float amount);//change tracker values.
--
Responsibilities
- keeps track of StatTrackers
- fetch statTrackers with StatTrackerContainer[Stats]
*/



    public enum Stats
    {
        Health,
        Location,
        Stealth,
        Power,
        AirCapacity,
        AirConsumption,
        AirRestoration,
        MovementSpeed,
        TurnSpeed
    }

    
    Dictionary<Stats, StatTracker> StatTrackerContainer = new Dictionary<Stats, StatTracker>();

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
        if (StatTrackerContainer.ContainsKey(statType))
            return;

        StatTrackerContainer.Add(statType, statTracker);
    }

    /// <summary>
    /// add amount to the stat tracker for statType
    /// </summary>
    /// <param name="trackerType"></param>
    /// <param name="amount"></param>
    public bool UpdateStat(Stats statType, float amount)
    {
        if (!StatTrackerContainer.ContainsKey(statType))
            return false;

        StatTrackerContainer[statType].Change(amount);

        return true;
    }
}
