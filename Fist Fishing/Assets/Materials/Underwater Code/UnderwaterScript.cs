using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterScript : MonoBehaviour
{

    public float m_WaterLevel;
    public GameObject m_WaterPlane;
    public Material m_UnderwaterColor;
    public bool isUnderwater;
    // Start is called before the first frame update
    void Start()
    {
        //m_OverwaterColor = new Vector4(0.5f, 0.5f, 0.5f, 0.5f);
        m_WaterLevel = m_WaterPlane.transform.position.y;
        //Color m_FogColor = m_UnderwaterColor.color;
        Color m_FogColor = new Vector4(0.4f, 0.7f, 1.0f, 0.5f); // hard set values as the material method of setting color lacked too much documentation to figure out a solution
        RenderSettings.fogDensity = 0.002f;
        RenderSettings.fogColor = m_FogColor;
        //RenderSettings.fog = true; // Code for quick color testing purposes
        RenderSettings.fog = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position.y < m_WaterLevel) != isUnderwater)
        {
            isUnderwater = transform.position.y < m_WaterLevel;
            if (isUnderwater)
            {
                SetUnderWater();

            }
            if (!isUnderwater)
            {
                SetOutofWater();
            }
        }
        void SetUnderWater()
        {
            RenderSettings.fog = true;
        }
        void SetOutofWater()
        {
            RenderSettings.fog = false;
        }
    }
}
