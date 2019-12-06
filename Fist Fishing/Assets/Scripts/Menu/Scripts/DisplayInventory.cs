using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DisplayInventory : MonoBehaviour
{
    //item that follows after you click.
    public MouseItem m_mouseItem;

    public InventoryObject m_inventory;

    public GameObject m_inventoryPrefab;

    public int m_xStartPos;
    public int m_yStartPos;
    public int m_xSpaceBetweenItems;
    public int m_ySpaceBetweenItems;
    public int m_numberOfColumns;
    protected Dictionary<GameObject, InventorySlot> m_itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
    void Start()
    {
        CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlots();
    }

    private void CreateSlots()
    {
        for (int i = 0; i < m_inventory.m_inventorySlots.Length; i++)
        {
            var obj = Instantiate(m_inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetGridPosition(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            m_itemsDisplayed.Add(obj, m_inventory.m_inventorySlots[i]);
        }
    }

    //Only used in a for loop for creating grid positions.
    public Vector3 GetGridPosition(int i)
    {
        return new Vector3(m_xStartPos + (m_xSpaceBetweenItems * (i % m_numberOfColumns)), m_yStartPos + (-m_ySpaceBetweenItems * (i / m_numberOfColumns)), 0f);
    }

    private void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> slot in m_itemsDisplayed)
        {
            if (slot.Value.m_ID >= 0)
            {
                slot.Key.GetComponent<RawImage>().color = new Color(1, 1, 1, .5f);
                //change sprite instead of changing color for future reference.
                slot.Key.GetComponent<RawImage>().color = slot.Value.m_item.prefab.GetComponent<Image>().color;
                slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = slot.Value.m_amount <= 0 ? "" : slot.Value.m_amount.ToString("n0");

                if (slot.Value.m_amount <= 0)
                {
                    slot.Value.UpdateSlot(-1, null, 0, null);
                }
            }
            else
            {
                slot.Key.GetComponent<RawImage>().color = new Color(1, 1, 1, .5f);
                slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    //check which obj we are hovering over.
    public void OnEnter(GameObject obj)
    {
        m_mouseItem.hoverObj = obj;
        if (m_itemsDisplayed.ContainsKey(obj))
            m_mouseItem.hoverSlot = m_itemsDisplayed[obj];

        m_mouseItem.hoverSlot.m_inventory = m_inventory;
    }
    public void OnExit(GameObject obj)
    {
        m_mouseItem.hoverObj = null;
        m_mouseItem.hoverSlot = null;

    }
    //grab a copy of whatever you clicked and set it to m_mouseItem's stats.
    public void OnDragStart(GameObject obj)
    {
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(32, 32);
        mouseObject.transform.SetParent(transform.parent);
        if (m_itemsDisplayed[obj].m_ID >= 0)
        {
            var img = mouseObject.AddComponent<Image>();
            img.color = m_itemsDisplayed[obj].m_item.prefab.GetComponent<Image>().color; //change sprite instead of changing color for future reference.
            img.raycastTarget = false;
        }
        m_mouseItem.obj = mouseObject;
        m_mouseItem.item = m_itemsDisplayed[obj];
    }
    public virtual void OnDragEnd(GameObject obj)
    {
        //if you're shuffling things in your own inventory use these functions.
        if (m_mouseItem.hoverObj && m_mouseItem.hoverSlot.m_inventory == m_inventory)
        {
            m_inventory.MoveItem(m_itemsDisplayed[obj], m_itemsDisplayed[m_mouseItem.hoverObj]);
        }

        else if (m_mouseItem.hoverSlot != null)//if it's dropped in another inventory, remove from current and add to new one in the empty slot's position.
        {
            if (m_mouseItem.hoverSlot.m_inventory.GetType() == typeof(ShopMenuDisplayInventory))
            {
                ((ShopMenuDisplayInventory)m_mouseItem.hoverSlot.m_inventory).OnSell(m_mouseItem.item);
            }
            else 
            {
                if (m_inventory.GetType() == typeof(ShopMenuDisplayInventory))
                {
                    ((ShopMenuDisplayInventory)m_inventory).OnBuy(m_mouseItem.item);
                }
                    m_mouseItem.hoverSlot.m_inventory.AddItemAtSlot(m_mouseItem.item.m_item, m_mouseItem.item.m_amount, m_mouseItem.hoverSlot);
            }
            m_inventory.RemoveItem(m_itemsDisplayed[obj]);
        }
        else
        {
            m_inventory.RemoveItem(m_itemsDisplayed[obj]);
        }
        Destroy(m_mouseItem.obj);
        m_mouseItem.item = null;
    }
    public void OnDrag(GameObject obj)
    {
        if (m_mouseItem.obj != null)
        {
            m_mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    void OnGUI()
    {
        if (!this.gameObject.activeSelf)
            return;
        if (m_mouseItem == null)
            return;
        if (m_mouseItem.hoverObj == null || m_mouseItem.hoverSlot == null)
            return;
        if (m_mouseItem.hoverSlot.m_item == null)
            return;
        //create position Vector2 for box.
        Vector2 DescriptionBoxPos = new Vector2(275, 40);
        //create offset that is Vector2 + offset for Label position.
        Vector2 DescriptionTextPos = DescriptionBoxPos + new Vector2(20, 40);

        // Make a background box
        GUI.Box(new Rect(DescriptionBoxPos.x, DescriptionBoxPos.y, 250, 250), "Description");

        GUI.Label(new Rect(DescriptionTextPos.x, DescriptionTextPos.y, 200, 200), m_mouseItem.hoverSlot.m_item.description);
    }
}



