using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSlotManager : ASlotManager
{
    public override void SlotUpdate(ASlot changedSlot)
    {
        Debug.Log("Slot Changed");
    }
}
