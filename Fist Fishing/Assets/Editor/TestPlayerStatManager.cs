using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System;

public class TestPlayerStatManager : PlayerStatManager
{

    [Test]
    public void StatManagerConstructor()
    {
        Assert.NotNull(m_statTrackerContainer);

        //Makes sure a KeyValuePair<Stats, StatTracker> was created for each StatType.
        var ListOfStats = Enum.GetValues(typeof(Stats));
        foreach (var stat in ListOfStats)
        {
            //Test key
            Assert.True(m_statTrackerContainer.ContainsKey((Stats)stat));

            //test value
            StatTracker tracker = null;
            Assert.True(m_statTrackerContainer.TryGetValue((Stats)stat, out tracker));
            Assert.NotNull(tracker, String.Format("StatTracker {1} was not set during the Constructor! : {0}",tracker.ToString(),stat.ToString()   ));
        }


        Assert.True(UpdateStat(Stats.MaxAir, 10.0f), "StatTrackerContainer does not contain key!");
        //Assert.AreEqual(10.0f, m_statTrackerContainer[Stats.MaxAir].CurrentAmount);
    }
}
