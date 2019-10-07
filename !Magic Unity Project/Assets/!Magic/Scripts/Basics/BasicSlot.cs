using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSlot : ASlot
{
    public override void ToggleHighlighting(bool toggle)
    {
        throw new System.NotImplementedException();
    }

    public override void WasEmptied()
    {
        if (m_SlotManager != null)
            m_SlotManager.SlotUpdate(this);
    }
}
