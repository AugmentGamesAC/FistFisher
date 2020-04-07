using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// scriptable object that handles the shop window inventory
/// </summary>
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/ShopMenuInventory")]
public class ShopMenuDisplayInventory : InventoryObject
{
    public int m_sellAmount;
    public int m_spendAmount;
    public Inventory m_playerInventory;

    /// <summary>
    /// figures out how much the things are worth and adds that value to player clams
    /// </summary>
    /// <param name="inventorySlot"></param>
    public void OnSell(InventorySlot inventorySlot)
    {
        if (!EnsureInventory())
            return;

        m_sellAmount = 0;
        m_sellAmount += inventorySlot.m_item.m_worthInCurrency * inventorySlot.m_amount;//bad later

        m_playerInventory.GainMoney(m_sellAmount);
    }

    /// <summary>
    /// ensure there is a valid inventory
    /// </summary>
    /// <returns>true if valid transaction</returns>
    protected bool EnsureInventory()
    {

        //using this twice, do it on awake or something.
        if (m_playerInventory == null)
        {
            GameObject m_player = GameObject.FindGameObjectWithTag("Player");
            m_playerInventory = m_player.GetComponentInChildren<Inventory>();
        }

        if (m_playerInventory == null)
            return false;

        return true;
    }
    /// <summary>
    /// figures out how much things bought are worth and tries to remove that from player
    /// </summary>
    /// <param name="Slot"></param>
    /// <returns>true if valid transaction</returns>
    public bool OnBuy(InventorySlot Slot)
    {
        if (!EnsureInventory())
            return false;

        m_spendAmount = 0;

        m_spendAmount += Slot.m_item.m_worthInCurrency * Slot.m_amount;//bad later

        return m_playerInventory.SpendMoney(m_spendAmount);
    }

    void OnApplicationQuit()
    {
        for (int i = 0; i < m_inventorySlots.Length; i++)
        {
            m_inventorySlots[i].UpdateSlot(-1, null, 0, null);
        }
    }
}
