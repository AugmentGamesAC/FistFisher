using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// info tracker for player for when in combat
/// </summary>
public class PlayerCombatInfo : CombatInfo
{
    //this or get these values from player.
    [SerializeField]
    protected NoiseTracker m_noiseTracker = new NoiseTracker();
    public NoiseTracker NoiseTracker => m_noiseTracker;

    public PinwheelTracker<Bait> m_baitOptions ;
    public PinwheelTracker<CombatMoveInfo> m_attackPinwheel;
    //Add List of Bait_IItems into the object
    public PlayerCombatInfo(List<CombatMoveInfo> moves)
    {
        m_attackPinwheel = new PinwheelTracker<CombatMoveInfo>(1, moves);
        //Create bait options from passed in items
        m_baitOptions = new PinwheelTracker<Bait>(1, default);
    }

    public void UpdateOxygen(float change)
    {
        PlayerInstance.Instance.Oxygen.Change(change);
    }
    /// <summary>
    /// Called when an item is used
    /// </summary>
    public void ConsumeItem()
    {
        //inventory stuff.
        //When an item is used in combat remove it from the inventory 
    }
    public void TakeDamage(float damage)
    {
        PlayerInstance.Instance.Health.Change(-damage);
    }
    public void UpdateNoise(float change)
    {
        m_noiseTracker.Change(change);
    }
}
