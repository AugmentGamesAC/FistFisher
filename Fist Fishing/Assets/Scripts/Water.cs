using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "First Person Player")
        {
            other.GetComponent<PlayerMovement>().m_IsSwimming = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "First Person Player")
        {
            other.GetComponent<PlayerMovement>().m_IsSwimming = false;
        }
    }
}
