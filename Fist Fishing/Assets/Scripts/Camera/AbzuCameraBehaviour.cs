using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbzuCameraBehaviour : CameraBehavoir
{
    public override void ResolveInput(float orbitX, float orbitY, float lookatX, float lookatY)
    {
        //apply input values to orbit points
        m_cameraPoint.Increment(orbitX, orbitY);
        m_lookAtPoint.Increment(lookatX, lookatY);

        base.ResolveInput(orbitX, orbitY, lookatX, lookatY);

        //offset and position camera around followObject(Player)
        Vector3 CameraWantsToMoveHere = m_followObject.transform.position + (Vector3)(m_followObject.transform.localToWorldMatrix * m_cameraPoint.ReturnTargetPoint());

        Vector3 CameraWantsToLookAtThis = m_followObject.transform.localToWorldMatrix * m_lookAtPoint.ReturnTargetPoint();

        MoveCameraTowards(CameraWantsToMoveHere);

        CameraLooksTowards(CameraWantsToLookAtThis);
    }
}
