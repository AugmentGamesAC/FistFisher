﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FishCombatInfo : CombatInfo
{
    [SerializeField]
    protected IFishData m_fishData = new TestingFish();
    public IFishData FishData => m_fishData;

    public void TakeDamage(float damage)
    {
        m_fishData.Health.Change(-damage);
    }

    /// <summary>
    /// Slow is a percent modifier, slow * movespeed
    /// We only want this to happen for a specific amount of turns.
    /// </summary>
    /// <param name="slowAmount"></param>
    public void SlowDown(float slowAmount)
    {
        ResetMoveSpeed();
        Speed.SetValue(Speed * slowAmount);
    }

    public void ResetMoveSpeed()
    {
        Speed.SetValue(m_fishData.CombatSpeed);
    }

    [SerializeField]
    protected FloatTracker m_spawnChance = new FloatTracker();
    public FloatTracker SpawnChance => m_spawnChance;
    [SerializeField]
    public FloatTracker Speed = new FloatTracker();
    [SerializeField]
    public FloatTracker CombatDistance = new FloatTracker();
    [SerializeField]
    public FloatTracker Direction = new FloatTracker();
}
