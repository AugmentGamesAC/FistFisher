using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillLevelTest : MonoBehaviour
{
    public float m_changePerPress = 10.0f;
    public ASpellUser m_spellUser;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (m_spellUser != null)
        {
            //mana +/-
            if (Input.GetKeyDown(KeyCode.N))
            {
                m_spellUser.ModifyMana(-m_changePerPress);
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                m_spellUser.ModifyMana(m_changePerPress);
            }

            //aura +/-
            if (Input.GetKeyDown(KeyCode.J))
            {
                m_spellUser.ModifyShield(-m_changePerPress);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                m_spellUser.ModifyShield(m_changePerPress);
            }
        }
    }
}
