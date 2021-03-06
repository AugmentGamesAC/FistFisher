﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// figures out if player is under the water, and applies a fog effect
/// </summary>
public class UnderwaterScript : MonoBehaviour
{

    public float m_WaterLevel;
    public GameObject m_WaterPlane;
    public Material m_UnderwaterColor;
    public bool isUnderwater;
    [SerializeField]
    protected float m_fogDensity = 0.011f; 


    // Start is called before the first frame update
    void Start()
    {
        //m_OverwaterColor = new Vector4(0.5f, 0.5f, 0.5f, 0.5f);
        m_WaterLevel = m_WaterPlane.transform.position.y;
        //Color m_FogColor = m_UnderwaterColor.color;
        Color m_FogColor = new Vector4(0.4f, 0.7f, 1.0f, 0.5f); // hard set values as the material method of setting color lacked too much documentation to figure out a solution
        RenderSettings.fogDensity = m_fogDensity;
        RenderSettings.fogColor = m_FogColor;
        //RenderSettings.fog = true; // Code for quick color testing purposes
        RenderSettings.fog = false;
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.fogDensity = m_fogDensity;
            isUnderwater = transform.position.y < m_WaterLevel;
            RenderSettings.fog = isUnderwater;
        
    }
}
