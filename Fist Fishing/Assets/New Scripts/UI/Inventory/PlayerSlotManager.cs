using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;

public class PlayerSlotManager : RetangleSlotManager
{
    public void Start()
    {
        PlayerInstance.RegisterPlayerInventory(this);
    }
}

