﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;

/// <summary>
/// specific slot manager for the player inventory
/// </summary>
public class PlayerSlotManager : RetangleSlotManager
{
    public override void HandleSlotDrop(PointerEventData eventData, ISlotData droppedOn)
    {
        var slotref = CommonMountPointer.eventData.pointerDrag.GetComponent<ASlotRender>();
        int newvalue = droppedOn.CheckAddItem(slotref.Tracker.Item, slotref.Tracker.Count);
        if (newvalue == slotref.Tracker.Count)
        {
            OnDrop(eventData);
            return;
        }
        var delta = slotref.Tracker.Count - newvalue;

        if (slotref.SlotMan != this)
        {
            if (PlayerInstance.Instance.Clams < slotref.Tracker.Item.WorthInCurrency * delta)
            {
                OnDrop(eventData);
                return;
            }
            PlayerInstance.Instance.Clams.SetValue(PlayerInstance.Instance.Clams - slotref.Tracker.Item.WorthInCurrency * delta);
        }
        base.HandleSlotDrop(eventData, droppedOn);
    }

    public void Start()
    {
        PlayerInstance.RegisterPlayerInventory(this);
    }
}

