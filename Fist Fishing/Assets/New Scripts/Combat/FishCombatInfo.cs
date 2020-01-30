using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCombatInfo : CombatInfo
{
    //data needed for combat
    //Health
    //UI image
    //Move Speed
    //Behaviour type.

        [SerializeField]
    protected IFishData m_fishData;
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

    public float m_combatDistance;
}
