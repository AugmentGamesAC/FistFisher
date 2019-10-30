using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this class handles the test cubes to be used as spell chips
public class KBMSlottableStuff: SpellChip
{
    public KBM_GauntletSlotsAndCastingScript m_KBMGauntlet; 

    Valve.VR.InteractionSystem.Interactable m_MyInteractble; //interactable script

    //gets the slot relative to slot index given by key press and tells the slot to slot this object
    private void DoSlotThings(int slotIndex)
    {
        ASlot slot = m_KBMGauntlet.m_KBMSpellList.GetSlot(slotIndex);
        if (slot != null)
        {
            AssignSlot(slot);
        }
    }

    //if this object is being hovered over, assing to slot of given number
    void Update()
    {
        if(m_MyInteractble==null)
            m_MyInteractble = gameObject.GetComponent<Valve.VR.InteractionSystem.Interactable>();



        if (m_MyInteractble.isHovering && m_KBMGauntlet != null)
        {
            if (Input.GetKeyDown(m_KBMGauntlet.m_castOrAssignToSlot1))
            {
                DoSlotThings(0);
            }
            if (Input.GetKeyDown(m_KBMGauntlet.m_castOrAssignToSlot2))
            {
                DoSlotThings(1);
            }
            if (Input.GetKeyDown(m_KBMGauntlet.m_castOrAssignToSlot3))
            {
                DoSlotThings(2);
            }
        }
    }
}