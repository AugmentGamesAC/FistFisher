using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject m_inventory;

    public int m_xStartPos;
    public int m_yStartPos;
    public int m_xSpaceBetweenItems;
    public int m_ySpaceBetweenItems;
    public int m_numberOfColumns;
    Dictionary<InventorySlot, GameObject> m_itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    private void CreateDisplay()
    {
        for (int i = 0; i < m_inventory.m_itemContainer.Count; i++)
        {
            var obj = Instantiate(m_inventory.m_itemContainer[i].m_item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetGridPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = m_inventory.m_itemContainer[i].m_amount.ToString("n0");//n0 formats with comas when you get to high numbers.
        }
    }

    //Only used in a for loop for creating grid positions.
    public Vector3 GetGridPosition(int i)
    {
        return new Vector3(m_xStartPos + (m_xSpaceBetweenItems * (i % m_numberOfColumns)), m_yStartPos + (-m_ySpaceBetweenItems * (i / m_numberOfColumns)), 0f);
    }

    private void UpdateDisplay()
    {
        for (int i = 0; i < m_inventory.m_itemContainer.Count; i++)
        {
            //if it's in the inventory 
            if (m_itemsDisplayed.ContainsKey(m_inventory.m_itemContainer[i]))
            {
                if (m_inventory.m_itemContainer[i].m_amount <= 0)
                {
                    //remove from display
                    m_inventory.m_itemContainer.RemoveAt(i);
                    m_itemsDisplayed.Remove(m_inventory.m_itemContainer[i]);
                    //return
                    return;
                }
                m_itemsDisplayed[m_inventory.m_itemContainer[i]].GetComponentInChildren<TextMeshProUGUI>().text = m_inventory.m_itemContainer[i].m_amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(m_inventory.m_itemContainer[i].m_item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetGridPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = m_inventory.m_itemContainer[i].m_amount.ToString("n0");//n0 formats with comas when you get to high numbers.
                m_itemsDisplayed.Add(m_inventory.m_itemContainer[i], obj);
            }
        }
    }
}
