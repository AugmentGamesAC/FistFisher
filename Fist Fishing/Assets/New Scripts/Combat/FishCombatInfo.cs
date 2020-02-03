using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCombatInfo : CombatInfo
{
    [SerializeField]
    protected IFishData m_fishData = new TestingFish();
    public IFishData FishData => m_fishData;

    public void TakeDamage(float damage)
    {
        //Health module.Change(-damage);\
        m_fishData.Health.Change(-damage);
    }

    /// <summary>
    /// Slow is a percent modifier, slow * movespeed
    /// We only want this to happen for a specific amount of turns.
    /// </summary>
    /// <param name="slowAmount"></param>
    public void SlowDown(float slowAmount)
    {
        //slow* movespeed;
    }

    public void ChangeSpawnChance(float change)
    {
        m_spawnChance.Change(change);
    }
    
    protected StatTracker m_spawnChance;
    public StatTracker SpawnChance => m_spawnChance;

    public float m_combatDistance;
}
