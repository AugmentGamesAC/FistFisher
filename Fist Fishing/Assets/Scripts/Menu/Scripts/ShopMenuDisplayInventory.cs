using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/ShopMenuInventory")]
public class ShopMenuDisplayInventory : InventoryObject
{
    public int m_sellAmount;
    public Inventory m_playerInventory;

    public void OnSell(InventorySlot inventorySlot)
    {
        m_sellAmount = 0;

        m_sellAmount += inventorySlot.m_item.m_worthInCurrency * inventorySlot.m_amount;//bad later

        if (m_playerInventory == null)
        {
            GameObject m_player = GameObject.FindGameObjectWithTag("Player");
            m_playerInventory = m_player.GetComponent<Inventory>();
        }

        if (m_playerInventory == null)
            return;

        m_playerInventory.GainMoney(m_sellAmount);
    }
}
