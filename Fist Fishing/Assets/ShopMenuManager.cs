using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenuManager : MonoBehaviour
{
    public ShopMenuDisplayInventory m_shopMenuDisplayInventory;

    List<AItem> ShopItems;

    public BaitItem bait;

    // Start is called before the first frame update
    void Start()
    {
        ShopItems = new List<AItem>();

        ShopItems.Add(bait);
        ShopItems.Add(bait);
        ShopItems.Add(bait);
        ShopItems.Add(bait);
        ShopItems.Add(bait);
        ShopItems.Add(bait);

        ShopItems.Add(bait);
        ShopItems.Add(bait);
        ShopItems.Add(bait);
        ShopItems.Add(bait);
        ShopItems.Add(bait);
        ShopItems.Add(bait);

        ShopItems.Add(bait);
        ShopItems.Add(bait);
        ShopItems.Add(bait);
        ShopItems.Add(bait);
        ShopItems.Add(bait);
        ShopItems.Add(bait);

        foreach (var item in ShopItems)
        {
            m_shopMenuDisplayInventory.AddItem(bait, 10);
        }
    }

    void OnApplicationQuit()
    {
        for (int i = 0; i < m_shopMenuDisplayInventory.m_inventorySlots.Length; i++)
        {
            m_shopMenuDisplayInventory.m_inventorySlots[i].UpdateSlot(-1, null, 0, null);
        }
    }
}
