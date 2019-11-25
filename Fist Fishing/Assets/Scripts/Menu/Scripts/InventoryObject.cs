using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> ItemContainer = new List<InventorySlot>();
    public void AddItem(AItem item, int amount)
    {
        bool hasItem = false;
        for (int i = 0; i < ItemContainer.Count; i++)
        {
            if(ItemContainer[i].m_item == item)
            {
                ItemContainer[i].AddAmount(amount);
                hasItem = true;
                break;
            }
        }
        if(!hasItem)
        {
            ItemContainer.Add(new InventorySlot(item, amount));
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public AItem m_item;
    public int m_amount;
    public InventorySlot(AItem item, int amount)
    {
        m_item = item;
        m_amount = amount;
    }

    public void AddAmount(int value)
    {
        m_amount += value;
    }
}