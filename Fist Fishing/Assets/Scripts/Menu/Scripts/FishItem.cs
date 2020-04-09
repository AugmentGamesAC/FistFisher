using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// item for fish
/// </summary>
[CreateAssetMenu(fileName = "New Fish Object", menuName = "Inventory System/Items/Fish")]
public class FishItem : AItem
{
    public float m_weight;
    private void Awake()
    {
        Init(ItemType.Fish);
    }
}
