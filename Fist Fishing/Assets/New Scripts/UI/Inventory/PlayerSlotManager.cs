using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;

public class PlayerSlotManager : RetangleSlotManager
{
    public new  void Start()
    {
        base.Start();
        PlayerInstance.RegisterPlayerInventory(this);
    }
}

