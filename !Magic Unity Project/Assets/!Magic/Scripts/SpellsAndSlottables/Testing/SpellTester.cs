using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTester : MonoBehaviour
{

    public SpellDescription.Aiming _Aiming;
    public SpellDescription.Aiming m_Aiming { get { return _Aiming; } set { _Aiming = value; } }
    public SpellDescription.Effect _Effect;
    public SpellDescription.Effect m_Effect { get { return _Effect; } set { _Effect = value; } }
    public SpellDescription.Shape _Shape;
    public SpellDescription.Shape m_Shape { get { return _Shape; } set { _Shape = value; } }
    public SpellDescription.Usage _Usage;
    public SpellDescription.Usage m_Usage { get { return _Usage; } set { _Usage = value; } }

    public SpellManager spellManager;
    public GauntletAiming m_AimingDirection;

    public Spell m_Spell;
    public int m_spellIDTemp;

    public GameObject m_playerPrefab;

    public GameObject SpellPrismPrefab;

    //public List<Spell> m_spellList; //temp for testing

    private void FixedUpdate()
    {
        //Make
        if (Input.GetKeyDown(KeyCode.M))
        {
            CleanupOldSpell();
            m_Spell = new Spell(new SpellDescription(m_Shape, m_Effect, m_Usage, m_Aiming), null);
            m_Spell.m_SpellManager = spellManager;
            m_Spell.m_AimPoint = m_AimingDirection;
            m_Spell.m_spellOwner = m_playerPrefab.GetComponent<IMagicUser>();


            //this is for creating a spell prism, not for using the spell prism to cast a spell
            GameObject newSpellPrism = Instantiate(SpellPrismPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            SlottableSpellPrism spellPrismScript = newSpellPrism.GetComponent<SlottableSpellPrism>();
            if(spellPrismScript!=null)
            {
                spellPrismScript.SetSpellID(m_spellIDTemp);//change ID to whatever you get from airsig
                spellPrismScript.m_spell=m_Spell;
            }

        }

        if ((Input.GetKeyDown(KeyCode.N)) && (m_Spell != null))
        {
            m_Spell.BeginAiming();
        }

        if ((Input.GetKeyDown(KeyCode.Z)) && (m_Spell != null))
        {
            m_Spell.Cancel();
        }

    }


    void CleanupOldSpell()
    {
        if (m_Spell == null)
            return;

        m_Spell.Cancel();
        m_Spell = null;
    }
}
