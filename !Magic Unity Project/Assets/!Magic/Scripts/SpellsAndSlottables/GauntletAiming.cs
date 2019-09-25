using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GauntletAiming : MonoBehaviour, IAimingDirection
{
    public Vector3 StartingPoint { get { return gameObject.transform.position; }  }
    public Vector3 Direction { get { return gameObject.transform.forward; } }
    public Transform LatchForSpells { get { return gameObject.transform; } }

    public bool m_showDebugAim = true;

    // Update is called once per frame
    void Update()
    {

        if (m_showDebugAim)
        {
            Debug.DrawRay(StartingPoint, Direction, Color.red);
        }
    }
}
