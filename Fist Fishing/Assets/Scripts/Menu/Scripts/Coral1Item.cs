﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Coral1 Object", menuName = "Inventory System/Items/Coral1")]
public class Coral1Item : AItem
{
    private void Awake()
    {
        Init(ItemType.Coral1);
    }
}