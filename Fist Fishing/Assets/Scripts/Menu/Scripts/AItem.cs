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
    public int ID;
    public GameObject prefab;
    public ItemType type;
    [TextArea(15, 20)]
    public string description;
}
