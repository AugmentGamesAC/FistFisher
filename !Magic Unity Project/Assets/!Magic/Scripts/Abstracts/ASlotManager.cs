using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASlotManager : MonoBehaviour
{
    [SerializeField]
    protected List<ASlot> m_Slots = new List<ASlot>();

    public ASlot GetSlot(int slot) { return m_Slots[slot]; }

    /// <summary>
    /// Called by a slot object to add itself to m_Slots
    /// </summary>
    /// <param name="newslot">slot child reference</param>
    public virtual void RegisterSlot(ASlot newslot)
    {
        if (newslot == default)
            return;
        m_Slots.Add(newslot);
    }
    /// <summary>
    /// Lets the slot manager update its information based on the changes to slot
    /// checks the slots.Slotted for destails about change
    /// </summary>
    /// <param name="changedSlot"></param>
    public abstract void SlotUpdate(ASlot changedSlot);
}
