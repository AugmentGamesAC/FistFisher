using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// tracker for mouse position relative to inventory objects
/// </summary>
public class MouseItem : MonoBehaviour
{
    public GameObject obj;
    public InventorySlot item;

    public GameObject hoverObj;
    public InventorySlot hoverSlot;
}