using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;

public class RetangleSlotManager : SlotManager
{
    public int m_xStartPos;
    public int m_yStartPos;
    public int m_xSpaceBetweenItems;
    public int m_ySpaceBetweenItems;
    public int m_numberOfColumns;
    public int m_slotCount = 30;
    public int m_boxScale;

    public GameObject m_inventoryPrefab;

    public Vector3 GetGridPosition(int i)
    {
        return new Vector3(m_xStartPos + (m_xSpaceBetweenItems * (i % m_numberOfColumns)), m_yStartPos + (-m_ySpaceBetweenItems * (i / m_numberOfColumns)), 0f);
    }

    public new void Start()
    {
        base.Start();
        for (int i = 0; i < m_slotCount; i++)
        {
            var obj = Instantiate(m_inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetGridPosition(i);
            obj.GetComponent<RectTransform>().sizeDelta = Vector2.one * m_boxScale;
            obj.GetComponentInChildren<ASlotRender>().SetSlotIndex(i);
        }
    }
}

