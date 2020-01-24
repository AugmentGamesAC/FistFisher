using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCombatInfo : CombatInfo
{
    //data needed for combat
    // not included in a fish instance

    public float SlowEffect;
    public float Speed;
    public float Distance;

    /// <summary>
    /// Need a fish instance class.
    /// </summary>
    public FishInstance FishInstance = new FishInstance();
}
