using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OxygenTracker : MonoBehaviour
{
    [SerializeField]
    protected PercentageTracker m_oxy;

    public delegate void OnLowOxygenEvent();
    public event OnLowOxygenEvent OnLowOxygen;

    public float m_OxygenRegeneration = 20.0f;
    public float m_OxygenDegeneration = 5.0f;
    public float m_noOxygenDamage = 5.0f;

    public float m_OxygenTickFrequency = 1.0f;
    public float m_OxygenTickTimer = 0.0f;

    public bool m_isUnderWater = false;


    void Start()
    {
        m_oxy = new PercentageTracker(100.0f);

        ResetOxygen();
        m_OxygenTickTimer = m_OxygenTickFrequency;
    }

    private void Update()
    {
        //Regen/degen oxygen process.
        m_OxygenTickTimer -= Time.deltaTime;
        if (m_OxygenTickTimer > 0)
            return;

        ModifyOxygen(m_isUnderWater ? -m_OxygenDegeneration: m_OxygenRegeneration);

        m_OxygenTickTimer = m_OxygenTickFrequency;

        NoOxygenCheck();
    }

    public void ModifyOxygen(float changeAmount)
    {
        m_oxy.IncrementCurrent(changeAmount);
    }


    protected void NoOxygenCheck()
    {
        if (m_oxy.Current > 0)
            return;
        //trigger anything that needs to happen when we have no oxygen. Make sure we subscribe this event to something before invoke().
        if (OnLowOxygen != null)
            OnLowOxygen.Invoke();

    }

    public void ResetOxygen()
    {
        m_oxy.SetCurrent(m_oxy.Max);
    }

}
