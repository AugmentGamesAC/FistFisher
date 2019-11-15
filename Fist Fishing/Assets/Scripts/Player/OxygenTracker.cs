﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenTracker : MonoBehaviour
{
    [SerializeField]
    protected float m_currentOxygen;
    public float CurrentOxygen { get { return m_currentOxygen; } }

    protected float m_maxOxygen = 100.0f;
    public float MaxOxygen { get { return m_maxOxygen; } }

    [SerializeField]
    protected float m_OxygenPercentage;
    public float OxygenPercentage { get { return m_OxygenPercentage; } }

    public delegate void OnLowOxygenEvent();
    public event OnLowOxygenEvent OnLowOxygen;

    public float m_OxygenRegeneration = 20.0f;
    public float m_OxygenDegeneration = 5.0f;

    public float m_OxygenTickFrequency = 1.0f;
    public float m_OxygenTickTimer = 0.0f;
    public float m_OxygenHealthDamageTimer = 0.0f;

    public bool m_isUnderWater = false;

    public HealthModule m_healthComponent;


    void Start()
    {
        ResetHealth();
        UpdateOxygenPercentage();
    }

    private void Update()
    {
        m_OxygenTickTimer += Time.deltaTime;

        if (m_OxygenTickTimer > m_OxygenTickFrequency)
        {
            if (m_isUnderWater)
            {
                ReduceOxygen(m_OxygenDegeneration);
            }
            else
            {
                ModifyOxygen(m_OxygenRegeneration);
            }
            ResetOxygenTickTimer();
        }
    }

    public virtual void ModifyOxygen(float changeAmount)
    {
        m_currentOxygen += Mathf.Clamp(changeAmount, -m_maxOxygen, m_maxOxygen);
        m_currentOxygen = Mathf.Clamp(m_currentOxygen, 0.0f, m_maxOxygen);

        UpdateOxygenPercentage();
    }

    public virtual void ReduceOxygen(float reductionAmount)
    {
        ModifyOxygen(-reductionAmount);

        NoOxygenCheck();
    }

    private void UpdateOxygenPercentage()
    {
        m_OxygenPercentage = m_currentOxygen / m_maxOxygen;
    }

    private bool NoOxygenCheck()
    {
        if (m_currentOxygen > 0.0f)
            return false;

        m_healthComponent.TakeDamage(m_OxygenDegeneration);

        ResetOxygenTickTimer();

        LowOxygen();

        return true;
    }

    public void ResetHealth()
    {
        m_currentOxygen = m_maxOxygen;
    }

    protected virtual void LowOxygen()
    {
        OnLowOxygen.Invoke();
    }

    private void ResetOxygenTickTimer()
    {
        m_OxygenTickTimer = 0.0f;
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Water")
        {
            m_isUnderWater = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Water")
        {
            m_isUnderWater = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Water")
        {
            m_isUnderWater = true;
        }
    }
}
