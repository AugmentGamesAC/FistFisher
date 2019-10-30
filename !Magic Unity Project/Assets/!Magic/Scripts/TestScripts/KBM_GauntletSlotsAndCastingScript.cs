using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KBM_GauntletSlotsAndCastingScript : MonoBehaviour
{
    public KeyCode m_castOrAssignToSlot1 = KeyCode.Alpha1;
    public KeyCode m_castOrAssignToSlot2 = KeyCode.Alpha2;
    public KeyCode m_castOrAssignToSlot3 = KeyCode.Alpha3;

    public Gauntlet m_GauntletScript;
    public KBMSpellList m_KBMSpellList;

    // Start is called before the first frame update
    void Start()
    {
        m_GauntletScript = gameObject.GetComponent<Gauntlet>();
    }


    void CastSpell(int index)
    {
        if (m_KBMSpellList == null)
            return;
        Spell spell = m_KBMSpellList.ReadSpell(index);
        if (spell == null)
            return;

        //INSERT THINGS HERE TO CAST THE SPELL
        throw new System.NotImplementedException();
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
