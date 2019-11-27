using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public InventorySlot[] m_inventorySlots = new InventorySlot[32];

    public void AddItem(AItem item, int amount)
    {
        if (item.ID == -1)
            return;

        for (int i = 0; i < m_inventorySlots.Length; i++)
        {
            if (m_inventorySlots[i].m_ID == item.ID)
            {
                m_inventorySlots[i].AddAmount(amount);
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
                m_inventorySlots[i].UpdateSlot(item.ID, item, amount);
                return m_inventorySlots[i];
            }
        }
        //come back to this to set up full inventory.
        return null;
    }

    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot tempSlot = new InventorySlot(item2.m_ID, item2.m_item, item2.m_amount);
        item2.UpdateSlot(item1.m_ID, item1.m_item, item1.m_amount);
        item1.UpdateSlot(tempSlot.m_ID, tempSlot.m_item, tempSlot.m_amount);
    }

    public void RemoveItem(AItem item)
    {
        for (int i = 0; i < m_inventorySlots.Length; i++)
        {
            if(m_inventorySlots[i].m_item == item)
            {
                m_inventorySlots[i].UpdateSlot(-1, null, 0);
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

    public InventorySlot()
    {
        m_ID = -1;
        m_item = null;
        m_amount = 0;
    }
    public InventorySlot(int id, AItem item, int amount)
    {
        m_ID = id;
        m_item = item;
        m_amount = amount;
    }

    public void AddAmount(int value)
    {
        m_amount += value;
    }

    public void UpdateSlot(int id, AItem item, int amount)
    {
        m_ID = id;
        m_item = item;
        m_amount = amount;
    }
}