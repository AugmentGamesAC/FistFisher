using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// unspecified items
/// </summary>
[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Default")]
public class DefaultItemObject : AItem
{
    private void Awake()
    {
        Init(ItemType.Default);
    }
}
