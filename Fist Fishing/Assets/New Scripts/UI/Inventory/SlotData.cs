using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotData
{
    [SerializeField]
    protected AItem m_AItem;
    public AItem AItem => m_AItem;

    [SerializeField]
    protected int m_index = -1;
    public int Index => m_index;

    [SerializeField]
    protected SlotManager m_Manager;
    public SlotManager Manager => m_Manager;


    public void Start()
    {

    }


}

