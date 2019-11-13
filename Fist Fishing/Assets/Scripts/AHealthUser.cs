using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AHealthUser : MonoBehaviour
{
    protected float m_currentHealth;
    public float CurrentHealth { get { return m_currentHealth; } }

    protected float m_maxHealth = 100.0f;
    public float MaxHealth { get { return m_maxHealth; } }

    protected float m_healthPercentage;
    public float HealthPercentage { get { return m_healthPercentage; } }

    public delegate void OnDeathEvent();
    public event OnDeathEvent OnDeath;

    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
        UpdateHealthPercentage();
    }

    public virtual void ModifyHealth(float changeAmount)
    {
        m_currentHealth += Mathf.Clamp(changeAmount, 0, m_maxHealth);

        UpdateHealthPercentage();

        UpdateDeathStatus();
    }

    public virtual void TakeDamage(float damageAmount)
    {
        ModifyHealth(-damageAmount);
    }

    private void UpdateHealthPercentage()
    {
        m_healthPercentage = m_currentHealth / m_maxHealth;
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

    protected virtual void Death()
    {
        OnDeath.Invoke();

        //Disable Object, ObjectPool should Handle fish but not the player.

        //player should be sent to respawn. 

        //Fish Type and Player should override this function if it does something special.
    }
}
