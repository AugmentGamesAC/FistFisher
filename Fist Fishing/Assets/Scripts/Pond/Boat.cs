using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public Transform m_mountTransform;
    public Transform m_dismountTransform;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerMovement PlayerController = other.GetComponent<PlayerMovement>();

            PlayerController.m_canMount = true;
            PlayerController.m_boatMountPosition = m_mountTransform.position;
            PlayerController.m_boatDismountPosition = m_dismountTransform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerMovement PlayerController = other.GetComponent<PlayerMovement>();

            PlayerController.m_canMount = false;
        }
    }
}
