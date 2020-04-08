using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// slices of a pinwheel that check if they're the activating one
/// </summary>
[RequireComponent(typeof(Image))]
public class PinwheelTab : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    protected int m_iD;
    public int ID => m_iD;
    Image m_image;

    Color defaultColor = Color.white;
    Color selectedColor = Color.grey;
    bool m_isSelected;
   
    AttackPinwheelUpdater m_manager;

    public void SetSelected(bool isSelected)
    {
        if (m_image != null)
            m_image.color = (isSelected) ? selectedColor : defaultColor;
        m_isSelected = isSelected;
    }

    protected void Start()
    {
        m_manager = GetComponentInParent<AttackPinwheelUpdater>();
        m_image = GetComponent<Image>();
        SetSelected(m_isSelected);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_manager != null)
            m_manager.SetValue(ID);
    }
   
}
