using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FishCombatInfo : CombatInfo
{
    [SerializeField]
    protected FishInstance m_fish;
    public FishInstance FishInstance => m_fish;

    [SerializeField]
    public FloatTracker Speed = new FloatTracker();
    [SerializeField]
    public FloatTracker CombatDistance = new FloatTracker();
    [SerializeField]
    public FloatTracker Direction = new FloatTracker();

    public void TakeDamage(float damage)
    {
        m_fish.Health.Change(-damage);
    }

    public FishCombatInfo(FishInstance fish)
    {
        m_fish = fish;
        Speed.SetValue(fish.FishData.CombatSpeed);
        CombatDistance.SetValue(15);
        Direction.SetValue(fish.FishData.FishClassification == FishBrain.FishClassification.Agressive ? 1 : -1);
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
        Speed.SetValue(m_fish.FishData.CombatSpeed);
    }
}
