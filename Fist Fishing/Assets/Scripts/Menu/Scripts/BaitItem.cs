using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bait Object", menuName = "Inventory System/Items/Bait")]
public class BaitItem : AItem
{
    //add extra variables to display here.

    private void Awake()
    {
        type = ItemType.Bait;
        ID = (int)ItemType.Bait;
    }
}
