﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;

/// <summary>
/// slot manager for the shops
/// </summary>
public class ShopSlotManager : RetangleSlotManager
{
    [System.Serializable]
    public class InitItemsAmount : InspectorDictionary<Bait_IItem, int> { }
    [SerializeField]
    protected InitItemsAmount m_initialShopItems = new InitItemsAmount();
    public InitItemsAmount InitItems => m_initialShopItems;

    public void Start()
    {
        if (m_initialShopItems.Count <= 0)
            throw new System.Exception("no Init Shop items");

        foreach (var item in m_initialShopItems)
        {
            AddItem(item.Key, item.Value);
        }
    }

    public override void RegisterSlot(ISlotData slot)
    {
        base.RegisterSlot(slot);
        if (slot.Count > 0 )
        {
            var something = slot.Item;
        }
    }

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
            PlayerInstance.Instance.Clams.SetValue(PlayerInstance.Instance.Clams + slotref.Tracker.Item.WorthInCurrency * delta);

        base.HandleSlotDrop(eventData, droppedOn);
    }

    protected override void Init()
    {
        List<KeyValuePair<Bait_IItem, int>> newList = m_initialShopItems.ToList();

        for (int i = 0; i < m_slotCount; i++)
        {
            var obj = Instantiate(m_inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetGridPosition(i);
            obj.GetComponent<RectTransform>().sizeDelta = Vector2.one * m_boxScale;
            obj.GetComponentInChildren<ASlotRender>().SetSlotIndex(i);

            int baitchoice = i % m_numberOfColumns;
            if (baitchoice < newList.Count)
                obj.GetComponentInChildren<ASlotRender>().Tracker.AddItem(newList[baitchoice].Key, newList[baitchoice].Value);
        }
    }
}

