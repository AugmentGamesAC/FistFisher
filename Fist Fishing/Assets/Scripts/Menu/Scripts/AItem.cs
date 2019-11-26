﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Currency,
    Bait,
    Harvestable,
    Fish,
    Equipment,
    Coral1,
    Coral2,
    Default
}

public abstract class AItem : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
    [TextArea(15, 20)]
    public string description;
}
