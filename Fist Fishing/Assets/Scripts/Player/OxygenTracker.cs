using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class OxygenTracker : StatTracker
{
    public OxygenTracker(float max)
    {
        m_oxy = new PercentageTracker(max);
        ResetOxygen();
        m_OxygenTickTimer = m_OxygenTickFrequency;


        m_oxygenRegeneration = PlayerInstance.Instance.PlayerStatMan[Stats.AirRestoration];
        m_oxygenConsumption = PlayerInstance.Instance.PlayerStatMan[Stats.AirConsumption];
    }

    [SerializeField]
    protected PercentageTracker m_oxy;
    public PercentageTracker Tracker => m_oxy;

    [SerializeField]
    public bool m_isUnderWater = false;
    public bool IsUnderWater => m_isUnderWater;

    public delegate void OnLowOxygenEvent(float change);
    public event OnLowOxygenEvent OnLowOxygen;

    [SerializeField]
    protected StatTracker m_oxygenRegeneration;
    public StatTracker OxygenRegeneration => m_oxygenRegeneration;

    [SerializeField]
    protected StatTracker m_oxygenConsumption;
    public StatTracker OxygenConsumption => m_oxygenConsumption;

    public float m_noOxygenDamage = 5.0f;


    public float m_OxygenTickFrequency = 1.0f;
    public float m_OxygenTickTimer = 0.0f;

    public override float MaxValue => m_oxy.Max;

    public void Update()
    {
        //don't update if game is paused
        if (NewMenuManager.PausedActiveState)
            return;
        //Regen/degen oxygen process.
        m_OxygenTickTimer -= Time.deltaTime;
        if (m_OxygenTickTimer > 0)
            return;

        Change(m_isUnderWater ? -OxygenConsumption: m_oxygenRegeneration);

        m_OxygenTickTimer = m_OxygenTickFrequency;
    }

    public override void Change(float changeAmount)
    {
        if (NoOxygenCheck(changeAmount))
            return;
        m_oxy.IncrementCurrent(changeAmount);
    }

    protected bool NoOxygenCheck(float change)
    {
        if (m_oxy.Current > 0 || !m_isUnderWater)
            return false;
        OnLowOxygen?.Invoke(change*2);
        return true;
    }

    public void ResetOxygen()
    {
        m_oxy.SetCurrent(m_oxy.Max);
    }

    public override void SetValue(float max)
    {
        m_oxy.SetMax(max);
    }
}
