using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Coral2 Object", menuName = "Inventory System/Items/Coral2")]
public class Coral2Item : AItem
{
    private void Awake()
    {
        Init(ItemType.Coral2);
    }
}
