using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPocket : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Waist")
        {
            other.GetComponentInParent<PlayerMovement>().m_isSwimming = false;
        }
        if (other.tag == "FaceMask")
        {
            other.GetComponent<OxygenTracker>().m_isUnderWater = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Waist")
        {
            other.GetComponentInParent<PlayerMovement>().m_isSwimming = true;
        }
        if (other.tag == "FaceMask")
        {
            other.GetComponent<OxygenTracker>().m_isUnderWater = true;
        }
    }
}
