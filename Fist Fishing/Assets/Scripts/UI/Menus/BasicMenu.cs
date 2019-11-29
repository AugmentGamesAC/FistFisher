using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// base class for all menu items
/// presently just gets a ref to attached gameobject, and deactivates after instantiated
/// </summary>
[System.Serializable]
public class BasicMenu : MonoBehaviour
{

    [SerializeField]
    protected GameObject m_HUD;
    public GameObject HUD { get { return m_HUD; } }
    [SerializeField]
    protected Canvas m_canvas;
    public Canvas Canvas { get { return m_canvas; } }

    void Awake()
    {
        if (m_HUD == null)
            m_HUD = gameObject;
        if (m_canvas == null)
            m_canvas = m_HUD.GetComponent<Canvas>();
        //CloseMenu();
    }

    public bool CloseMenu()
    {
        //m_HUD.SetActive(false);
        m_canvas.enabled = false;
        return true;
    }
    public bool OpenMenu()
    {
        //m_HUD.SetActive(true);
        m_canvas.enabled = true;
        return true;
    }
}
