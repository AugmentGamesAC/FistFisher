using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade
{
    /*
     Upgrade 
 --
 PlayerStatManager statManager;
 float cost;
 delegate wasUpdated;
 Dictionary<Stats, float> statsModifier;
 --
 UpdateCost(Func<Dictionary<Stats, float>, float>);
 ApplyUpgrade();
 */
    PlayerStatManager statManager;

    float cost;

    public delegate void wasUpdated();
    public event wasUpdated OnUpdated;

    Dictionary<PlayerStatManager.Stats, float> statsModifier;

    public void UpdateCost(System.Func<Dictionary<PlayerStatManager.Stats, float>, float> func)
    {

    }

    public void ApplyUpgrade()
    {
        if (statsModifier.Count <= 0 && statManager != default)
            return;

        foreach (var item in statsModifier)
        {
            statManager.UpdateStat(item.Key, item.Value);
        }
    }
}
