using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// stores info of items being dragged between slots
/// </summary>
public class DragTracker : MonoBehaviour
{
    public PointerEventData eventData;
    public Image DragImage;
    public RectTransform Rect;
    public ISlotData SlotTarget;
    public Vector2 StartingPosition;
}

