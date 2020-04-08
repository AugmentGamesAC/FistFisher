using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// a menu that displays for a set duration before deactivating itself
/// </summary>
public class DisappearingMenu : Menu, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    protected float m_timer;
    [SerializeField]
    protected float m_displayDuration;
    protected bool m_isHovered = false;
    public bool HoveringOn => m_isHovered;
    /// <summary>
    /// Show menu
    /// </summary>
    public void WasKeyed()
    {
        if (!gameObject.activeSelf)
            Show(true);
        m_timer = m_displayDuration;   
    }

    public void FixedUpdate()
    {
        if (HoveringOn)
            return;
        if (m_timer <= 0)
            return;
        m_timer -= Time.deltaTime;
        if (m_timer <= 0)
            Show(false);
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        m_isHovered = true;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        m_isHovered = false;
    }
}
