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

    [SerializeField]
    protected PlayerHealth m_playerHealth = new PlayerHealth();
    public PlayerHealth PlayerHealth => m_playerHealth;


    [SerializeField]
    protected OxygenTracker m_oxygenTracker = new OxygenTracker();
    public OxygenTracker OxygenTracker => m_oxygenTracker;

    public PinWheel<Bait> m_baitOptions = new PinWheel<Bait>(1, default);
    public PinWheel<CombatMoveInfo> m_attackPinwheel = new PinWheel<CombatMoveInfo>(1, default);

    public void UpdateOxygen(float change)
    {
        m_oxygenTracker.ModifyOxygen(change);
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
}
