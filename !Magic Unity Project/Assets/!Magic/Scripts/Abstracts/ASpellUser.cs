using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ASpellUser : MonoBehaviour
{
    protected float m_HealthCurrent;
    public float HealthCurrent { get { return m_HealthCurrent; } }
    protected float m_HealthPredicted;
    public float HealthPredicted { get { return m_HealthPredicted; } }
    protected float m_HealthMax;
    public float HealthMax { get { return m_HealthMax; } }
    protected float m_HealthRegen;
    public float HealthRegen { get { return m_HealthRegen; } }
    protected float m_HealthPercentage;
    public float HealthPercentage { get { return m_HealthPercentage; } }

    protected float m_ManaCurrent;
    public float ManaCurrent { get { return m_ManaCurrent; } }
    protected float m_ManaPredicted;
    public float ManaPredicted { get { return m_ManaPredicted; } }
    protected float m_ManaMax;
    public float ManaMax { get { return m_ManaMax; } }
    protected float m_ManaRegen;
    public float ManaRegen { get { return m_ManaRegen; } }
    protected float m_ManaPercentage;
    public float ManaPercentage { get { return m_ManaPercentage; } }


    public bool PredictHealthModify(float change)
    {
        m_HealthPredicted = Mathf.Clamp(m_HealthCurrent + change, 0, m_HealthMax);
        return true;
    }
    public void ApplyHealthPrediction() { m_HealthCurrent = HealthPredicted; }
    /// <summary>
    /// for instant forced drain, no prediction.
    /// </summary>
    /// <param name="manaLoss"></param>
    public void ModifyHealth(float change)
    {
        m_HealthPredicted = Mathf.Clamp(m_HealthPredicted + change, 0, m_HealthMax);
        m_HealthCurrent = Mathf.Clamp(m_HealthCurrent + change, 0, m_HealthMax);

        UpdateHealthPercentage();
    }

    /// <summary>
    /// With only one spell able to effect mana at a time, we only need to check against the current and overwrite 
    /// prediciotn each time
    /// </summary>
    /// <param name="manaLoss"> positive or negative value based on current </param>
    /// <returns></returns>
    public bool PredictManaModify(float change)
    {
        if (change > m_ManaCurrent)
            return false;

        m_ManaPredicted = Mathf.Clamp(m_ManaCurrent + change, 0, m_ManaMax);
        

        return true;
    }
    public void ApplyPrediction() { m_ManaCurrent = m_ManaPredicted; }
    /// <summary>
    /// for instant forced drain, no prediction.
    /// </summary>
    /// <param name="manaLoss"></param>
    public void ModifyMana(float change)
    {
        m_ManaPredicted = Mathf.Clamp(m_ManaPredicted + change, 0, m_ManaMax);
        m_ManaCurrent = Mathf.Clamp(m_ManaCurrent + change, 0, m_ManaMax);

        UpdateManaPercentage();
    }

    private void UpdateHealthPercentage()
    {
        m_HealthPercentage = m_HealthCurrent / m_HealthMax;
    }

    private void UpdateManaPercentage()
    {
        m_ManaPercentage = m_ManaCurrent / m_ManaMax;
    }

    public abstract Transform Aiming { get; }
}

