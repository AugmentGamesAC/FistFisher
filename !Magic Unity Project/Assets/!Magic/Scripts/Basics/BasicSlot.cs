using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSlot : ASlot
{
    public override void ToggleHighlighting(bool toggle)
    {
        m_IsHighlighted = toggle;
        //Debug.Log("Toggled Highlighting to " + m_IsHighlighted);
    }

    public override void WasEmptied()
    {
        if (m_SlotManager != null)
            m_SlotManager.SlotUpdate(this);

        ToggleHighlighting(false);
        Debug.LogWarning("detached from slot");

        
        m_Slotted = null;
        m_SlotFilter = ASlottable.SlotTypes.Null;
    }


}
