using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class OxygenTracker 
{
    public OxygenTracker(float max)
    {
        m_oxy = new PercentageTracker(max);
        ResetOxygen();
        m_OxygenTickTimer = m_OxygenTickFrequency;
    }

    [SerializeField]
    protected PercentageTracker m_oxy;
    public PercentageTracker Tracker => m_oxy;

    [SerializeField]
    public bool m_isUnderWater = false;
    public bool IsUnderWater => m_isUnderWater;

    public delegate void OnLowOxygenEvent(float change);
    public event OnLowOxygenEvent OnLowOxygen;

    public float m_OxygenRegeneration = 50.0f;
    public float m_OxygenDegeneration = 3.0f;
    public float m_noOxygenDamage = 5.0f;



    public float m_OxygenTickFrequency = 1.0f;
    public float m_OxygenTickTimer = 0.0f;

   


    public void Update()
    {
        //don't update if game is paused
        if (NewMenuManager.PausedActiveState)
            return;
        //Regen/degen oxygen process.
        m_OxygenTickTimer -= Time.deltaTime;
        if (m_OxygenTickTimer > 0)
            return;

        ModifyOxygen(m_isUnderWater ? -m_OxygenDegeneration: m_OxygenRegeneration);

        m_OxygenTickTimer = m_OxygenTickFrequency;
    }

    public void ModifyOxygen(float changeAmount)
    {
        if (NoOxygenCheck(changeAmount))
            return;
        m_oxy.IncrementCurrent(changeAmount);
    }


    protected bool NoOxygenCheck(float change)
    {
        if (m_oxy.Current > 0)
            return false;
        OnLowOxygen?.Invoke(change*2);
        return true;
    }

    public void ResetOxygen()
    {
        m_oxy.SetCurrent(m_oxy.Max);
    }

}
