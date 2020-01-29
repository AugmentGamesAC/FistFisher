using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// info tracker for player for when in combat
/// </summary>
public class PlayerCombatInfo : CombatInfo
{
    //Things we need access to 
    //current oxgen
    //current health/changing health
    //players bait inventory

    public void UpdateOxygen(float change) { }
    public void ConsumeItem() { }
    public void TakeDamage(float damage) { }


    public PinWheel<Bait> m_baitOptions = new PinWheel<Bait>();
    public PinWheel<CombatMoveInfo> m_attackPinwheel = new PinWheel<CombatMoveInfo>();
}
