using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBasicSlottable : BasicSlottable
{

    public bool m_showDebugSpheres = true;
    //if toggled on, shows inner and ouret spheres to refine slottable slot detection radius
    void OnDrawGizmos()
    {
        if (m_showDebugSpheres)
        {
            Color innerColour = new Color(1, 0, 0, 0.5f);
            Gizmos.color = innerColour;
            Gizmos.DrawSphere(transform.position, m_MinDistanceToDetectSlots);

            Color outerColour = new Color(0, 1, 0, 0.25f);
            Gizmos.color = outerColour;
            Gizmos.DrawSphere(transform.position, m_MaxDistanceToDetectSlots);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) //for non-vr testing, allows to set mode to held.
            PlayerGrab();
        if (Input.GetKeyDown(KeyCode.H))
            PlayerDrop();
    }
}
