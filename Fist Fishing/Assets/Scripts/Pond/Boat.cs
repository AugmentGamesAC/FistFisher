using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    protected Vector3 m_boatHeight = Vector3.zero;

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

    private void Awake()
    {
        m_boatHeight.y = transform.position.y;
    }


    private void Update()
    {
        Vector3 v = transform.position;
        v.y = m_boatHeight.y;
        transform.position = v;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerMovement PlayerController = other.GetComponent<PlayerMovement>();

            PlayerController.m_canMount = false;
            PlayerController.m_boatMountPosition = m_mountTransform.position;
            PlayerController.m_boatDismountPosition = m_dismountTransform.position;
        }
    }
}
