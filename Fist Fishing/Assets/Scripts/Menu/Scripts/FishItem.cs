using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fish Object", menuName = "Inventory System/Items/Fish")]
public class FishItem : AItem
{
    public float m_worthInCurrency;
    public float weight;
    private void Awake()
    {
        Init(ItemType.Fish);
    }
}
