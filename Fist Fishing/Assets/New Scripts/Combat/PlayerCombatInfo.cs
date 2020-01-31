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

    public void UpdateOxygen(float change)
    {
        //Oxygen module.Change(change);
    }
    public void ConsumeItem()
    {
        
    }
    public void TakeDamage(float damage)
    {
        //health module.Change(damage);
    }
    public void UpdateNoise(float change)
    {
        m_noiseTracker.Change(change);
    }

    //this or get these values from player.
    public NoiseTracker m_noiseTracker;
    public PlayerHealth m_playerHealth;
    public OxygenTracker m_oxygenTracker;

    public PinWheel<Bait> m_baitOptions = new PinWheel<Bait>();
    public PinWheel<CombatMoveInfo> m_attackPinwheel = new PinWheel<CombatMoveInfo>();
}
