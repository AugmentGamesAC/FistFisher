using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SlotManager
{
    /// <summary>
    /// contains all the slot data for the manager, the key being the index of the slot 
    /// so a "next" previous logic can be maintained/observed.
    /// </summary>
    protected Dictionary<int, SlotData> m_mySLots;

    protected HashSet<int> m_freeSlots;


    /// <summary>
    /// Adds a new slote to the SlotManager to handle
    /// </summary>
    /// <param name="slot"></param>
    public void RegisterSlot(SlotData slot)
    {
        if (slot == default)
            return;

        m_mySLots.Add(slot.Index, slot);
        if (slot.Item == default)
            m_freeSlots.Add(slot.Index);
    }
    /// <summary>
    /// records when a slot's data is emptied
    /// </summary>
    /// <param name="slot"></param>
    public void FreeSlot(SlotData slot)
    {
        if (slot == default)
            return;

        m_freeSlots.Remove(slot.Index);
    }

    /// <summary>
    /// Handle Slot Drop handles the logic between different slots and determins solution
    /// </summary>
    /// <param name="Droper">The slot being draged</param>
    /// <param name="Dropee">The slot being draged to</param>
    /// <returns></returns>
    public virtual bool HandleSlotDrop(SlotData Droper, SlotData Dropee)
    {
        return false;
    }

    /// <summary>
    /// Adds the item as many times as needed depending on slot restrictions 
    /// returns fall if cannot add.
    /// </summary>
    /// <param name="item"> the item desired to be added</param>
    /// <param name=""></param>
    /// <returns></returns>
    public virtual bool AddItem(IItem item, int count)
    {
        return false;
    }
}

