using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        OxygenTracker oxy = other.GetComponent<OxygenTracker>();
        if (oxy != default)
            oxy.m_isUnderWater = true;

    }

    private void OnTriggerExit(Collider other)
    {
        OxygenTracker oxy = other.GetComponent<OxygenTracker>();
        if (oxy != default)
            oxy.m_isUnderWater = false;
    }
}
