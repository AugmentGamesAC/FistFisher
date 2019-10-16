using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ASpellUser : MonoBehaviour
{

    protected float m_ShieldCurrent;
    public float ShieldCurrent { get { return m_ShieldCurrent; } }
    protected float m_ShieldPredicted;
    public float ShieldPredicted { get { return m_ShieldPredicted; } }
    protected float m_ShieldMax;
    public float ShieldMax { get { return m_ShieldMax; } }
    protected float m_ShieldRegen;
    public float ShieldRegen { get { return m_ShieldRegen; } }
    protected float m_ShieldPercentage;
    public float ShieldPercentage { get { return m_ShieldPercentage; } }


    protected float m_HealthCurrent;
    public float HealthCurrent { get { return m_HealthCurrent; } }

    protected float m_HealthMax;
    public float HealthMax { get { return m_HealthMax; } }

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

    //use this 
    public void TakeDamage(float change)
    {
        if (m_ShieldPercentage <= 0.0f)
            ModifyShield(change);
        else
            ModifyHealth(change);
    }

    public void ModifyHealth(float change)
    {
        m_HealthCurrent = Mathf.Clamp(m_ManaCurrent + change, 0, m_ManaMax);

        UpdateHealthPercentage();
    }

    public bool IsDead()
    {
        if (m_HealthCurrent <= 0.0f)
            return true;
        return false;
    }

    public bool PredictShieldModify(float change)
    {
        m_ShieldPredicted = Mathf.Clamp(m_ShieldCurrent + change, 0, m_ShieldMax);
        return true;
    }
    public void ApplyShieldPrediction() { m_ShieldCurrent = ShieldPredicted; }
    /// <summary>
    /// for instant forced drain, no prediction.
    /// </summary>
    /// <param name="manaLoss"></param>
    public void ModifyShield(float change)
    {
        m_ShieldPredicted = Mathf.Clamp(m_ShieldPredicted + change, 0, m_ShieldMax);
        m_ShieldCurrent = Mathf.Clamp(m_ShieldCurrent + change, 0, m_ShieldMax);

        UpdateShieldPercentage();
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

    private void UpdateShieldPercentage()
    {
        m_ShieldPercentage = m_ShieldCurrent / m_ShieldMax;
    }

    private void UpdateManaPercentage()
    {
        m_ManaPercentage = m_ManaCurrent / m_ManaMax;
    }


    private void UpdateHealthPercentage()
    {
        m_HealthPercentage = m_HealthCurrent / m_HealthMax;
    }

    public abstract Transform Aiming { get; }
}

