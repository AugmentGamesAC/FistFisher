using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Default = -1,
    Currency,
    Bait,
    Harvestable,
    Fish,
    Equipment,
    Coral1,
    Coral2,
}

public abstract class AItem : ScriptableObject
{
    public int StackSize;
    public int ID;
    public int m_worthInCurrency;
    public GameObject prefab;
    public ItemType type;
    [TextArea(15, 20)]
    public string description;

    protected virtual void Init(ItemType _type)
    {
        type = _type;
        ID = (int)type;
    }
}
