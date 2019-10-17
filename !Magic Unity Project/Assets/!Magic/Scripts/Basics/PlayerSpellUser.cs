using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellUser : BasicSpellUser
{


    protected float m_HealthCurrent;
    public float HealthCurrent { get { return m_HealthCurrent; } }

    [SerializeField]
    protected float m_HealthMax;
    public float HealthMax { get { return m_HealthMax; } }

    protected float m_HealthPercentage;
    public float HealthPercentage { get { return m_HealthPercentage; } }




    // Start is called before the first frame update
    void Start()
    {
        m_HealthCurrent = m_HealthMax;
        UpdateHealthPercentage();
    }

    // Update is called once per frame
    void Update()
    {

    }
       
    public override void ModifyHealth(float change)
    {
        if (m_ShieldPercentage > 0.0f)
            ModifyShield(change);
        else
            ModifyExtraHealth(change);
    }


    public void ModifyExtraHealth(float change)
    {
        m_HealthCurrent = Mathf.Clamp(m_HealthCurrent + change, 0, m_HealthMax);

        UpdateHealthPercentage();
    }

    public override bool IsDead()
    {
        if (m_HealthCurrent <= 0.0f)
            return true;
        return false;
    }

    private void UpdateHealthPercentage()
    {
        m_HealthPercentage = m_HealthCurrent / m_HealthMax;
    }
}
