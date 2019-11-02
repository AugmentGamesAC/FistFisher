using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCore : MonoBehaviour
{
    public GameObject m_manaObject;
    public Color m_manaColour;
    Renderer m_manaRenderer;

    public GameObject m_auraObject;
    public Color m_auraColour;
    Renderer m_auraRenderer;

    public ASpellUser m_spellUser;




    // Start is called before the first frame update
    void Start()
    {
        if (m_manaObject != null)
        {
            m_manaRenderer = m_manaObject.GetComponent<Renderer>();
        }

        if (m_manaRenderer != null && m_manaColour != null)
        {
            m_manaRenderer.material.SetColor("_Colour", m_manaColour);
        }


        if (m_auraObject != null)
        {
            m_auraRenderer = m_auraObject.GetComponent<Renderer>();
        }

        if (m_auraRenderer != null && m_auraColour != null)
        {
            m_auraRenderer.material.SetColor("_Colour", m_auraColour);
        }



    }

    // Update is called once per frame
    void Update()
    {
        if (m_manaRenderer != null)
        {
            float manaPercentage = m_spellUser.ManaPercentage;
            m_manaRenderer.material.SetFloat("_FillAmount", manaPercentage);
        }
        if (m_auraRenderer != null)
        {
            float auraPercentage = m_spellUser.ShieldPercentage;
            m_auraRenderer.material.SetFloat("_FillAmount", auraPercentage);
        }
    }
}
