using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biomass : MonoBehaviour
{

    public float m_Size = 0.0f;
    public float m_Density = 0.0f;
    protected float m_Biomass = 0.0f;
    public float Mass { get { return m_Biomass; } }

    public void CalcMass(float size, float density)
    {
        m_Size = size;
        m_Density = density;

        m_Biomass = m_Size * m_Density * transform.localScale.magnitude;
    }
}
