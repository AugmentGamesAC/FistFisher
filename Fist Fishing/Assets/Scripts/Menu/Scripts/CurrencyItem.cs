using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Currency Item", menuName = "Inventory System/Items/Currency")]
public class CurrencyItem : AItem
{
    private void Awake()
    {
        Init(ItemType.Currency);
    }
}
