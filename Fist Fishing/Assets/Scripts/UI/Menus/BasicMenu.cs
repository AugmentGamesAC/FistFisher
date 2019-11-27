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

    // Start is called before the first frame update
    void Start()
    {
    }

    void Awake()
    {
        if (m_HUD == null)
            m_HUD = gameObject;
        m_HUD.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public bool CloseMenu()
    {
        m_HUD.SetActive(false);
        return true;
    }
    public bool OpenMenu()
    {
        m_HUD.SetActive(true);
        return true;
    }

}
