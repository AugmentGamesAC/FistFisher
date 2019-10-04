using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is attached to a gameobject that can point forwards, so that the rest of the spell scripts casn know what way the player is pointing
public class GauntletAiming : MonoBehaviour, IAimingDirection
{
    //gets where this gameobject is (in the players hand)
    public Vector3 StartingPoint { get { return gameObject.transform.position; }  }

    //gets a forward direction vector
    public Vector3 Direction { get { return gameObject.transform.forward; } }

    //returns the transform for this object
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
