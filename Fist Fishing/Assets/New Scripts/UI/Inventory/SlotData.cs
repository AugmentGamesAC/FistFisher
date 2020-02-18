using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotData
{
    [SerializeField]
    protected IItem m_item;
    public IItem Item => m_item;

    [SerializeField]
    protected int m_index = -1;
    public int Index => m_index;

    [SerializeField]
    protected int m_count = -1;
    public int Count => m_count;

    [SerializeField]
    protected SlotManager m_Manager;
    public SlotManager Manager => m_Manager;
}

