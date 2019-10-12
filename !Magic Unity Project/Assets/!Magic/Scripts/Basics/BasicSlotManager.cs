using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSlotManager : ASlotManager
{
    public override void RegisterSlot(ASlot newslot)
    {
        //m_Slots.Add(newslot);
    }

    public override void SlotUpdate(ASlot changedSlot)
    {
        throw new System.NotImplementedException();
    }
}
