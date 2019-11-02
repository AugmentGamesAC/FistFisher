﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillLevelTest : MonoBehaviour
{
    public float m_maxMana = 100.0f;
    public float m_currentMana = 50.0f;
    public float m_amountChangedPerPress = 10.0f;
    public GameObject m_fillableObject;
    public Color m_manaColour;

    Renderer m_renderer;

    public float m_fillRange = 1.0f; //+/- this value

    public Vector3 m_orientation = Vector3.up;
    public float m_rotateAmount = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if(m_fillableObject!=null)
        {
            m_renderer = m_fillableObject.GetComponent<Renderer>();
        }
        else
        {
            m_renderer = gameObject.GetComponent<Renderer>();
        }


        if(m_renderer!=null && m_manaColour!=null)
        {
            m_renderer.material.SetColor("_Colour", m_manaColour);
        }
        
    }

    void ChangeMana(float amount)
    {
        m_currentMana += amount;
        m_currentMana = Mathf.Clamp(m_currentMana, 0.0f, m_maxMana);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            ChangeMana(-m_amountChangedPerPress);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            ChangeMana(m_amountChangedPerPress);
        }


        if (m_renderer != null)
        {

            float manaPercentage = m_currentMana / m_maxMana;
            float manaSquished = manaPercentage * (m_fillRange) * 2.0f;
            float manaOffset = manaSquished - m_fillRange;

            m_renderer.material.SetFloat("_FillAmount", manaOffset);
            m_renderer.material.SetVector("_Orientation", m_orientation);
            m_renderer.material.SetFloat("_RotateAmount", m_rotateAmount);
        }


    }
}