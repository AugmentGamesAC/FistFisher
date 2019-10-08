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

    protected float m_ManaCurrent;
    public float ManaCurrent { get { return m_ManaCurrent; } }
    protected float m_ManaPredicted;
    public float ManaPredicted { get { return m_ManaPredicted; } }
    protected float m_ManaMax;
    public float ManaMax { get { return m_ManaMax; } }
    protected float m_ManaRegen;
    public float ManaRegen { get { return m_ManaRegen; } }

    /// <summary>
    /// With only one spell able to effect mana at a time, we only need to check against the current and overwrite 
    /// prediciotn each time
    /// </summary>
    /// <param name="manaLoss"> positive or negative value based on current </param>
    /// <returns></returns>
    public bool PredictManaLoss(float manaLoss)
    {
        if (manaLoss > m_ManaCurrent)
            return false;

        m_ManaPredicted = Mathf.Clamp(m_ManaCurrent - manaLoss, 0, m_ManaMax);
        

        return true;
    }
    public void ApplyPrediction() { m_ManaCurrent = m_ManaPredicted; }
    /// <summary>
    /// for instant forced drain, no prediction.
    /// </summary>
    /// <param name="manaLoss"></param>
    public void DrainMana(float manaLoss)
    {
        m_ManaPredicted = Mathf.Clamp(m_ManaPredicted - manaLoss, 0, m_ManaMax);
        m_ManaCurrent = Mathf.Clamp(m_ManaCurrent - manaLoss, 0, m_ManaMax);
    }


    public abstract Transform Aiming { get; }
}

