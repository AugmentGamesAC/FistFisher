using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reward 
{
    [SerializeField]
    protected HashSet<IItem> m_Items;
    public HashSet<IItem> Items => m_Items;

    [SerializeField]
    protected int m_clams;
    public int Clams => m_clams;
}
