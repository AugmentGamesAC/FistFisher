using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerData : MonoBehaviour, IMagicUser
{


    public UnityEvent OnModifyPlayerMana;
    public UnityEvent OnModifyPlayerManaConfirmed;

    public float m_health { get; set; }
    public float m_mana { get; set; }
    public float m_healthMax { get; set; }
    public float m_manaMax { get; set; }
    public float m_manaRegen { get; set; }
    public float m_actualMana { get; set; }
    public bool m_predictingManaUsage { get; set; }

    public float m_setMaxAura = 100.0f;
    public float m_setMaxMana = 100.0f;
    public float m_setManaRegen = 5.0f;


    float oldMana;


    // Start is called before the first frame update
    void Start()
    {


        m_healthMax = m_setMaxAura;
        m_manaMax = m_setMaxMana;
        m_manaRegen = m_setManaRegen;
        m_actualMana = m_manaMax;
        m_predictingManaUsage = false;
        oldMana = m_manaMax;

        m_health = m_healthMax;
        m_mana = m_manaMax;
    }

    //call when a spell is cancelled
    public void RefundMana()
    {
        m_mana = m_actualMana;
    }

    public void CancelledSpell()
    {
        RefundMana();
        m_predictingManaUsage = false;
    }

    public void TakeDamage(float damage)
    {
        m_health -= damage;
        m_health = Mathf.Clamp(m_health, 0.0f, m_healthMax);
    }

    //unimplemented
    public void TakeDamage(float damage, float duration)
    {
        throw new System.NotImplementedException();
    }

    public void UseMana(float mana)
    {
        m_mana -= mana;
        m_mana = Mathf.Clamp(m_mana, 0.0f, m_manaMax);
    }


    // Update is called once per frame
    void Update()
    {

        oldMana = m_mana;

        UseMana(-m_manaRegen*Time.deltaTime); //regen by taking negative damage to mana

        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    this.UseMana(-25.0f);
        //}
        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    this.UseMana(25.0f);
        //}



    }

    //at the end of an update, if we're predicting usage it will change small bar. If we're not, change both
    void LateUpdate()
    {
        if (!m_predictingManaUsage)
        {
            OnModifyPlayerManaConfirmed.Invoke();
            m_actualMana = m_mana;
        }
        if (oldMana != m_mana)
        {
            OnModifyPlayerMana.Invoke();
        }

    }

}
