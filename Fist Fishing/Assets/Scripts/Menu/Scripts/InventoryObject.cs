using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// base for inventories
/// has inventory slots and handles them
/// </summary>
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public int m_InventorySize = 30;
    public InventorySlot[] m_inventorySlots = new InventorySlot[30];


    public void Awake()
    {
        for (int i = 0; i < m_InventorySize; i++)
        {
            m_inventorySlots[i] = new InventorySlot(-1, null, 0, this);
        }
    }

    public void AddItem(AItem item, int amount)
    {
        if (item.ID == -1)
            return;

        for (int i = 0; i < m_inventorySlots.Length; i++)
        {
            if (m_inventorySlots[i].m_ID == item.ID)
            {
                if (m_inventorySlots[i].m_amount + amount > item.StackSize)
                    continue;

                m_inventorySlots[i].AddAmount(amount);
                return;
            }
        }
        SetFirstEmptySlot(item, amount);
    }

    //used to communicate between two menus.
    public void AddItemAtSlot(AItem item, int amount, InventorySlot slot)
    {
        if (item.ID == -1)
            return;

        //find the hoverSlot on this inventory list.
        for (int i = 0; i < m_inventorySlots.Length; i++)
        {
            if (m_inventorySlots[i] == slot)
            {
                //update that slot with info from dragged slot.
                m_inventorySlots[i].UpdateSlot(item.ID, item, amount, slot.m_inventory);
                return;
            }
        }

        SetFirstEmptySlot(item, amount);
    }

    public InventorySlot SetFirstEmptySlot(AItem item, int amount)
    {
        for (int i = 0; i < m_inventorySlots.Length; i++)
        {
            if (m_inventorySlots[i].m_ID <= -1)
            {
                m_inventorySlots[i].UpdateSlot(item.ID, item, amount, this);
                return m_inventorySlots[i];
            }
        }
        //come back to this to set up full inventory.
        return null;
    }

    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot tempSlot = new InventorySlot(item2.m_ID, item2.m_item, item2.m_amount, this);
        item2.UpdateSlot(item1.m_ID, item1.m_item, item1.m_amount, this);
        item1.UpdateSlot(tempSlot.m_ID, tempSlot.m_item, tempSlot.m_amount, tempSlot.m_inventory);
    }

    public void RemoveItem(InventorySlot Slot)
    {
        

        for (int i = 0; i < m_inventorySlots.Length; i++)
        {
            if (m_inventorySlots[i].m_item == Slot.m_item)
            {
                Slot.UpdateSlot(-1, null, 0, this);
                break;
            }
        }
    }

    public void RemoveAmount(AItem item, int amount)
    {
        for (int i = 0; i < m_inventorySlots.Length; i++)
        {
            if (m_inventorySlots[i].m_item == item)
            {
                m_inventorySlots[i].AddAmount(-amount);
            }
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public int m_ID = -1;
    public AItem m_item;
    public int m_amount;
    public InventoryObject m_inventory;

    public InventorySlot()
    {
        //should have list instead for later.
        m_ID = -1;
        m_item = null;
        m_amount = 0;
    }
    public InventorySlot(int id, AItem item, int amount, InventoryObject inventory)
    {
        m_ID = id;
        m_item = item;
        m_amount = amount;
        m_inventory = inventory;//giving this so that when another inventory calls a slot it can know which one the slot it's in.
    }

    public void AddAmount(int value)
    {
        m_amount += value;
    }

    public void UpdateSlot(int id, AItem item, int amount, InventoryObject inventory)
    {
        m_ID = id;
        m_item = item;
        m_amount = amount;
        m_inventory = inventory;

        ////if we're emptying the slot, set the sum to 0;
        //if (id <= -1)
        //{
        //    m_worthSum = 0;
        //    return;
        //}
        //else
        //{
        //    m_worthSum += item.m_worthInCurrency;
        //}
    }
}