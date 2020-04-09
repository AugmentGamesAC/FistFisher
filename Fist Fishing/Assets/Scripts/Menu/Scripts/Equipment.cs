using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// item for equipment
/// </summary>
[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
public class Equipment : AItem
{
    public float DamageModifier;
    public float DefenseModifier;
    private void Awake()
    {
        Init(ItemType.Equipment);
    }
}
