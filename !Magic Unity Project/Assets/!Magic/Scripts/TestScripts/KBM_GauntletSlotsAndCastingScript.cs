using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KBM_GauntletSlotsAndCastingScript : Gauntlet
{
    public KeyCode m_castOrAssignToSlot1 = KeyCode.Alpha1;
    public KeyCode m_castOrAssignToSlot2 = KeyCode.Alpha2;
    public KeyCode m_castOrAssignToSlot3 = KeyCode.Alpha3;

    // Start is called before the first frame update
    void Start()
    {
        m_ManaCurrent = m_ManaMax;
        m_ShieldCurrent = m_ShieldMax;
    }


    void CastSpell(int index)
    {
        if (m_spellList == null)
            return;
        Spell spell = m_spellList.ReadSpell(index);
        if (spell == null)
            return;

        m_currentSpell = spell;

        ResolveSimpleTriggering();
    }


    // Update is called once per frame
    void Update()
    {


        //do stuff here for getting the correct slot
        if (Input.GetKeyDown(m_castOrAssignToSlot1))
        {
            CastSpell(0);
        }
        if (Input.GetKeyDown(m_castOrAssignToSlot2))
        {
            CastSpell(1);
        }
        if (Input.GetKeyDown(m_castOrAssignToSlot3))
        {
            CastSpell(2);
        }
    }
}
