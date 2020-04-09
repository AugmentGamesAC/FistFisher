using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// attached to anything that could be targetted to check if the player _could_ target them
/// </summary>
public class Targetable : MonoBehaviour
{
    private Camera m_camera;
    public TargetController m_targetController;//don't set this manually.
    public bool m_isListed;

    private void Start()
    {
        m_camera = Camera.main;
        m_isListed = false;
    }

    private void Update()
    {
        //move all this stuff to a targetable class.
        Vector3 fishPosition = m_camera.WorldToViewportPoint(gameObject.transform.position);

        //Checks if the position is in viewport.
        bool onScreen = fishPosition.z > 0 && fishPosition.x > 0 && fishPosition.x < 1 && fishPosition.y > 0 && fishPosition.y < 1;

        if (m_targetController == null)
            return;

        //adds and removes fish from a targetable list.
        if (onScreen && !m_isListed)
        {
            m_targetController.m_fishInViewList.Add(gameObject);

            m_isListed = true;
        }
        else if (!onScreen && m_isListed)
        {
            m_targetController.m_fishInViewList.Remove(gameObject);

            m_isListed = false;
        }
    }
}
