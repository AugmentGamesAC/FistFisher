using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// unused camera mode
/// used for testing various camera controls
/// </summary>
public class LockedCameraBehaviour : CameraBehavior
{
    //LookAt Point doesn't move
    //camera rotates around followObject by a restricted amount of degrees.

    public LockedCameraBehaviour(OrbitPoint lookAtPoint, OrbitPoint cameraPoint) : base(lookAtPoint, cameraPoint) { }

    public override void ResolveInput(float orbitX, float orbitY, float lookatX, float lookatY)
    {
        //apply input values to orbit points
        //if (ALInput.GetKey(ALInput.ManualCamera))
        //    m_cameraPoint.Increment(orbitX, orbitY);

        //offset and position camera around followObject(Player)
        Vector3 CameraWantsToMoveHere = m_followObject.transform.position + (Vector3)(m_followObject.transform.localToWorldMatrix * m_cameraPoint.ReturnTargetPoint());

        Vector3 CameraWantsToLookAtThis = m_followObject.transform.position + (Vector3)( m_followObject.transform.localToWorldMatrix * m_lookAtPoint.ReturnTargetPoint());


        MoveCameraTowards(CameraWantsToMoveHere);
        CameraLooksTowards(CameraWantsToLookAtThis);
    }
}
