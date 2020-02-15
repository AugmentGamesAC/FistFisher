using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Tab : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField]
    protected bool IsSelectedOnInit = false;

    protected bool m_isSelected = false;
    protected bool m_isHovered = false;

    [SerializeField]
    protected Sprite m_selectedSprite;
    [SerializeField]
    protected Sprite m_deselectedSprite;
    [SerializeField]
    protected Sprite m_HoverSprite;


    protected Image m_image;

    protected TabManager m_parentTabManager;
    protected string m_description;

    [SerializeField]
    protected MenuList m_menuList;

    protected virtual void SwapSprite()
    {
        m_image.sprite = m_isSelected ? m_selectedSprite : m_isHovered ? m_HoverSprite : m_deselectedSprite;
    }

    /// <summary>
    /// Tells the menuList to show or hide when tab is Selected or not.
    /// </summary>
    /// <param name="activeState"></param>
    protected virtual void SwapMenuList()
    {
        m_menuList.ShowActive(m_isSelected);
    }

    protected virtual void Start()
    {
        m_parentTabManager = GetComponentInParent<TabManager>();

        m_image = GetComponent<Image>();
        SwapSprite();

        if (IsSelectedOnInit)
            m_parentTabManager.NewActivation(this);
    }

    /// <summary>
    /// Call this when click event triggers 
    /// Call this when a tab in the parent group gets changed.
    /// </summary>
    /// <param name="selected"></param>
    public virtual void SetIsSelected(bool selected)
    {
        m_isSelected = selected;
        SwapSprite();
        SwapMenuList();
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        m_parentTabManager.NewActivation(this);
    }

    //
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        m_isHovered = true;
        SwapSprite();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        m_isHovered = false;
        SwapSprite();
    }
}
