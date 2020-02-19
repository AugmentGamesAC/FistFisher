using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlotData : UITracker<ISlotData> , ISlotData
{
    [SerializeField]
    protected IItem m_item;
    public IItem Item => m_item;

    [SerializeField]
    protected int m_index = -1;
    public int Index => m_index;

    [SerializeField]
    protected int m_count = 0;
    public int Count => m_count;

    [SerializeField]
    protected SlotManager m_Manager;
    public SlotManager Manager => m_Manager;
    protected new void UpdateState()
    {
        OnStateChange?.Invoke(this);
    }

    public void SetIndex(int newIndex)
    {
        m_index = newIndex;
    }

    public void SetSlotManager(SlotManager newManger)
    {
        m_Manager = newManger;
    }

    /// <summary>
    /// adds item and updates as a tracker.
    /// </summary>
    /// <param name="item">the item to be added to the slot</param>
    /// <returns>the number of IItems that can not be added to this slot</returns>
    public int AddItem(IItem item, int count)
    {
        int remainder;
        if (m_item == default)
        {
            remainder = CheckAddItem(item, count);
            m_item = item;
            m_count = Mathf.Min(count, m_item.StackSize);
            m_Manager.UseSlot(this);
            UpdateState();
            return remainder;
        }

        if (!m_item.CanMerge(item))
            return count;
        remainder = CheckAddItem(item, count);
        m_count = Mathf.Min(m_count + count, m_item.StackSize);
        UpdateState();
        return remainder;
    }

    /// <summary>
    /// This function does not add the item but checks to see if it can be added to the information
    /// </summary>
    /// <param name="item">the item to be added to the slot</param>
    /// <returns>the number of IItems that can not be added to this slot</returns>
    public int CheckAddItem(IItem item, int count)
    {
        if (m_item == default)
            return Mathf.Max(0, count - item.StackSize);
        if (m_item.CanMerge(item))
            return Mathf.Max(0, count + m_count - item.StackSize);
        return count;
    }

    public void RemoveItem()
    {
        m_count = 0;
        m_item = default;
        m_Manager.FreeSlot(this);
        UpdateState();
    }

    public void RemoveCount(int count)
    {
        m_count = Mathf.Max(0, m_count - count);

        if (m_count == 0)
        {
            RemoveItem();
            return;
        }

        UpdateState();
    }
}

