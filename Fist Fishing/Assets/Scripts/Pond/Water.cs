using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "FaceMask")
        {
            other.GetComponent<OxygenTracker>().m_isUnderWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "FaceMask")
        {
            other.GetComponent<OxygenTracker>().m_isUnderWater = false;
        }
    }
}
