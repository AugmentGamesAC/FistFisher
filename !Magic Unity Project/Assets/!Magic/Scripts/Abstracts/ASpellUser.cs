using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ASpellUser : MonoBehaviour
{

    protected float m_ShieldCurrent;
    public float ShieldCurrent { get { return m_ShieldCurrent; } }
    protected float m_ShieldPredicted;
    public float ShieldPredicted { get { return m_ShieldPredicted; } }

    [SerializeField]
    protected float m_ShieldMax;
    public float ShieldMax { get { return m_ShieldMax; } }

    [SerializeField]
    protected float m_ShieldRegen = 0.0f; //shield does not regen unles we have a spell or actio for it
    public float ShieldRegen { get { return m_ShieldRegen; } }
    protected float m_ShieldPercentage;
    public float ShieldPercentage { get { return m_ShieldPercentage; } }




    protected float m_ManaCurrent;
    public float ManaCurrent { get { return m_ManaCurrent; } }
    protected float m_ManaPredicted;
    public float ManaPredicted { get { return m_ManaPredicted; } }

    [SerializeField]
    protected float m_ManaMax;
    public float ManaMax { get { return m_ManaMax; } }

    [SerializeField]
    protected float m_ManaRegen;
    public float ManaRegen { get { return m_ManaRegen; } }
    protected float m_ManaPercentage;
    public float ManaPercentage { get { return m_ManaPercentage; } }


    void Start()
    {
        m_ShieldCurrent = m_ShieldMax;
        m_ManaCurrent = m_ManaMax;
        UpdateShieldPercentage();
        UpdateManaPercentage();
    }

    //use this 
    public virtual void TakeDamage(float change)
    {
        ModifyShield(change);
    }

    public virtual bool IsDead()
    {
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



    public abstract Transform Aiming { get; }
}

