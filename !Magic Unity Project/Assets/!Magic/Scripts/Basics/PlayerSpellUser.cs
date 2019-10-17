using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//player has an additional health variable that can be decreased as a bit of a buffer
public class PlayerSpellUser : BasicSpellUser
{


    protected float m_HealthCurrent;
    public float HealthCurrent { get { return m_HealthCurrent; } }

    [SerializeField]
    protected float m_HealthMax = 1.0f;
    public float HealthMax { get { return m_HealthMax; } }

    [SerializeField]
    protected float m_HealthRegen = 0.0f; //0-1
    public float HealthRegen { get { return m_HealthRegen; } }

    protected float m_HealthPercentage;
    public float HealthPercentage { get { return m_HealthPercentage; } }

    //regen health at shield percentage in the event that 
    protected float m_RegenHealthAtShieldPercentage = 100.0f;
    public float m_RegenHealthAtShieldPercentage { get { return m_RegenHealthAtShieldPercentage; } }


    // Start is called before the first frame update
    void Start()
    {
        m_HealthCurrent = m_HealthMax;
        UpdateHealthPercentage();
    }

    // Update is called once per frame
    void Update()
    {
        if(HealthRegen > 0.0f && ShieldPercentage >= (m_RegenHealthAtShieldPercentage / 100.0f)) //if we're regening health
        {
            ModifyExtraHealth(m_HealthRegen * Time.deltaTime);
        }
    }
       
    //player has to check shield as well as health
    public override void ModifyHealth(float change)
    {
        if (m_ShieldPercentage > 0.0f)
            ModifyShield(change);
        else
            ModifyExtraHealth(change);
    }

    //terrible name, but necessary to call it this for the above
    public void ModifyExtraHealth(float change)
    {
        m_HealthCurrent = Mathf.Clamp(m_HealthCurrent + change, 0, m_HealthMax);

        UpdateHealthPercentage();
    }

    //player checks for health instead
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
