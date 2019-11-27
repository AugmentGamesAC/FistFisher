using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasicMenu : MonoBehaviour
{

    [SerializeField]
    protected GameObject m_HUD;
    public GameObject HUD { get { return m_HUD; } }

    // Start is called before the first frame update
    void Start()
    {
        if (m_HUD == null)
            m_HUD = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
