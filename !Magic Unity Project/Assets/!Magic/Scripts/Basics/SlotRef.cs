using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO: this class shouldn't be an abstract class should just be a reference class
public abstract class SlotRef
{
    protected ASlotManager m_SlotManager;
    protected ASlot m_Slot;
    protected int m_SlotIndex;
   

    public abstract void Detatch();
    public abstract void ToggleHighlighting(bool toggle);
    public abstract void ToggleHighlighting();
}
