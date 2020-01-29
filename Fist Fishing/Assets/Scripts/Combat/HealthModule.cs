using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthModule : MonoBehaviour
{
    [SerializeField]
    protected float m_currentHealth;
    public float CurrentHealth { get { return m_currentHealth; } }

    [SerializeField]
    protected float m_maxHealth = 100.0f;
    public float MaxHealth { get { return m_maxHealth; } }

    [SerializeField]
    protected float m_healthPercentage;
    public float HealthPercentage { get { return UpdateHealthPercentage(); } }

    public delegate void OnDeathEvent();
    public event OnDeathEvent OnDeath;

    public float m_regenTimer = 0.0f;
    public float m_regenDelay = 3.0f;
    public float m_regenRate = 5.0f;

    public Slider m_HealthSlider;

    // Start is called before the first frame update
    void Start()
    {
        //m_HealthSlider = GetComponentInChildren<Slider>();

        ResetHealth();
        UpdateHealthPercentage();
    }

    private void Update()
    {
        if (m_healthPercentage >= 1.0f)
            return;

        m_regenTimer += Time.deltaTime;
        if (m_regenTimer > m_regenDelay)
        {
            ModifyHealth(m_regenRate);
        }
    }

    public void ModifyHealth(float changeAmount)
    {
        m_currentHealth += Mathf.Clamp(changeAmount, -m_maxHealth, m_maxHealth);
        m_currentHealth = Mathf.Clamp(m_currentHealth, -1.0f, m_maxHealth);

        m_regenTimer = 0.0f;

        UpdateHealthPercentage();

        UpdateDeathStatus();
    }

    public void TakeDamage(float damageAmount)
    {
        ModifyHealth(-damageAmount);
    }

    private float UpdateHealthPercentage()
    {
        m_healthPercentage = m_currentHealth / m_maxHealth;

        //Health Canvas Value = m_healthPercentage;
        //m_HealthSlider.value = Mathf.Lerp(m_HealthSlider.value, m_healthPercentage, .1f);
        if(m_HealthSlider!=null)
            m_HealthSlider.value = m_healthPercentage;

        return m_healthPercentage;
    }

    private bool UpdateDeathStatus()
    {
        if (m_currentHealth >= 0)
            return false;

        OnDeath.Invoke();

        return true;
    }

    public void ResetHealth()
    {
        m_currentHealth = m_maxHealth;
        UpdateHealthPercentage();
    }
}
