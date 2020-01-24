using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbzuCameraBehaviour : CameraBehavoir
{
    public AbzuCameraBehaviour(OrbitPoint lookAtPoint, OrbitPoint cameraPoint) : base(lookAtPoint, cameraPoint) {}

    public override void ResolveInput(float orbitX, float orbitY, float lookatX, float lookatY)
    {
        //apply input values to orbit points
        m_cameraPoint.Increment(orbitX, orbitY);
        m_lookAtPoint.Increment(lookatX, lookatY);

        //base.ResolveInput(orbitX, orbitY, lookatX, lookatY);

        //offset and position camera around followObject(Player)
        Vector3 CameraWantsToMoveHere = m_followObject.transform.position + (m_cameraPoint.ReturnTargetPoint());

        Vector3 CameraWantsToLookAtThis = m_followObject.transform.position + m_lookAtPoint.ReturnTargetPoint();

        MoveCameraTowards(CameraWantsToMoveHere);

        CameraLooksTowards(CameraWantsToLookAtThis);
    }
}
