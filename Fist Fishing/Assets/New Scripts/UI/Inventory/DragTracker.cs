using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragTracker : MonoBehaviour
{
    public PointerEventData eventData;
    public Image DragImage;
    public RectTransform Rect;
    public ISlotData SlotTarget;
}

