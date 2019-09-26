using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IMagicUser
{
    public float m_HealthPercentage { get; set; }
    public float m_ManaPercentage { get; set; }
	public float m_health { get; set; }
	public float m_mana { get; set; }
	public float m_healthMax { get; set; }
	public float m_manaMax { get; set; }
	public float m_manaRegen { get; set; }
	public float m_actualMana { get; set; }
	public bool m_predictingManaUsage { get; set; }

	public float m_damageCooldown;
	private float m_maxDamageCooldown = 120f;

	public float m_setMaxAura = 100.0f;
	public float m_setMaxMana = 100.0f;
	public float m_setManaRegen = 5.0f;

    public float m_MovementY;

    public GameObject m_RespawnPoint;

    public UnityEvent OnDeath;
    public UnityEvent OnModifyHealth;
    public UnityEvent OnModifyMana;


    // Start is called before the first frame update
    void Start()
    {
        m_HealthPercentage = 1.0f;
        m_ManaPercentage = 1.0f;

		m_healthMax = m_setMaxAura;
		m_manaMax = m_setMaxMana;
		m_manaRegen = m_setManaRegen;
		m_actualMana = m_manaMax;
		m_predictingManaUsage = false;

		m_health = m_healthMax;
		m_mana = m_manaMax;

		m_damageCooldown = m_maxDamageCooldown;
	}

    // Update is called once per frame
    void Update()
    {
        ////placeholder until damage implemented.
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    ModifyHealth(-10f);
        //}
        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    ModifyHealth(+10f);
        //}
    }

    //any physics manipulations go here.
    void FixedUpdate()
    {
        if(m_damageCooldown != m_maxDamageCooldown)
		{
			m_damageCooldown -= 1f;

			if (m_damageCooldown <= 0f)
				m_damageCooldown = m_maxDamageCooldown;
		}
    }

    public void Death()
    {
        OnDeath.Invoke();
        GameObject.Destroy(gameObject);
    }

    void UpdateHealthPercentage()
    {
        m_HealthPercentage = (float)m_health / (float)m_healthMax;
    }

    void UpdateManaPercentage()
    {
        m_ManaPercentage = (float)m_mana / (float)m_manaMax;
    }

	public void TakeDamage(float damage)
	{
		if (m_damageCooldown != m_maxDamageCooldown)
			return;

		m_damageCooldown -= 1f;

		print("DAMAGE");

		//Change local health based on the spell's damage or healing property.
		m_health -= damage;

		//clamp health to not pass 100.
		m_health = Mathf.Clamp(m_health, 0.0f, m_healthMax);

		//Update mana percentage here instead of update to avoid unecessary calls. 
		UpdateHealthPercentage();

		if (m_health <= 0f)
			Death();

		//Trigger event for Health, this could be OnLostHealth for hurt sound and OnGainHealth for healing sound later.
		OnModifyHealth.Invoke();
	}

	public void TakeDamage(float damage, float duration)
	{
		throw new System.NotImplementedException();
	}

	public void UseMana(float mana)
	{

		//Change local mana based on consumption.
		m_mana -= mana;
		m_mana = Mathf.Clamp(m_mana, 0.0f, m_manaMax);

		//clamp mana.
		if (m_mana > 100f)
		{
			m_mana = 100f;
		}
		else if (m_mana < 0f)
		{
			m_mana = 0f;
		}

		//update mana percentage here instead of update to avoid unecessary calls. 
		UpdateManaPercentage();

		//if mana low, could have sound to warn player if character is human.
		OnModifyMana.Invoke();
	}
}
