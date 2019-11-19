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
    public float HealthPercentage { get { return m_healthPercentage; } }

    public delegate void OnDeathEvent();
    public event OnDeathEvent OnDeath;

    public float m_regenTimer = 0.0f;
    public float m_regenDelay = 3.0f;
    public float m_regenRate = 5.0f;

    public Slider m_HealthSlider;

    // Start is called before the first frame update
    void Start()
    {
        m_HealthSlider = GetComponentInChildren<Slider>();

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
        m_currentHealth = Mathf.Clamp(m_currentHealth, 0.0f, m_maxHealth);

        m_regenTimer = 0.0f;

        UpdateHealthPercentage();

        UpdateDeathStatus();
    }

    public void TakeDamage(float damageAmount)
    {
        ModifyHealth(-damageAmount);
    }

    private void UpdateHealthPercentage()
    {
        m_healthPercentage = m_currentHealth / m_maxHealth;

        //Health Canvas Value = m_healthPercentage;
        //m_HealthSlider.value = Mathf.Lerp(m_HealthSlider.value, m_healthPercentage, .1f);

        m_HealthSlider.value = m_healthPercentage;
    }

    private bool UpdateDeathStatus()
    {
        if (m_currentHealth > 0.0f)
            return false;

        Death();

        return true;
    }

    public void ResetHealth()
    {
        m_currentHealth = m_maxHealth;
    }

    protected void Death()
    {
        OnDeath.Invoke(); //get around to actually using

        //Disable Object, ObjectPool should Handle fish but not the player.

        //player should be sent to respawn. 

        //Fish Type and Player should override this function if it does something special.
    }
}
