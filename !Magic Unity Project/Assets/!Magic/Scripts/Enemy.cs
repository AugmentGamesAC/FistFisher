using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public float m_HealthPercentage { get; set; }
    public float m_ManaPercentage { get; set; }

    private int m_Health = 100;
    private int m_Mana = 50;
    private int m_MaxHealth = 100;
    private int m_MaxMana = 100;

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
    }

    // Update is called once per frame
    void Update()
    {
        //placeholder until damage implemented.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ModifyHealth(-10);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            ModifyHealth(+10);
        }
    }

    //any physics manipulations go here.
    void FixedUpdate()
    {
        
    }

    public void ModifyHealth(int deltaHealth)
    {
        //Change local health based on the spell's damage or healing property.
        m_Health += deltaHealth;

        //clamp health to not pass 100.
        if (m_Health >= 100)
        {
            m_Health = 100;
        }

        //Update mana percentage here instead of update to avoid unecessary calls. 
        UpdateHealthPercentage();

        if (m_Health <= 0)
        {
            Death();
        }

        //Trigger event for Health, this could be OnLostHealth for hurt sound and OnGainHealth for healing sound later.
        OnModifyHealth.Invoke();
    }

    public void ModifyMana(int deltaMana)
    {
        //Change local mana based on consumption.
        m_Mana += deltaMana;

        //clamp mana.
        if (m_Mana > 100)
        {
            m_Mana = 100;
        }
        else if (m_Mana < 0)
        {
            m_Mana = 0;
        }

        //update mana percentage here instead of update to avoid unecessary calls. 
        UpdateManaPercentage();

        //if mana low, could have sound to warn player if character is human.
        OnModifyMana.Invoke();
    }

    public void Death()
    {
        OnDeath.Invoke();
        GameObject.Destroy(gameObject);
    }

    void UpdateHealthPercentage()
    {
        m_HealthPercentage = (float)m_Health / (float)m_MaxHealth;
    }

    void UpdateManaPercentage()
    {
        m_ManaPercentage = (float)m_Mana / (float)m_MaxMana;
    }
}
