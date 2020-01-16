using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FirstPersonCameraBehaviour : CameraBehavoir
{
    //Same as Warthog but the cameraPoint Distance is right in front of followObject(Set in the inspector)
    //camera also does not need to take input, but only needs to looktowards lookatpoint if camera is at 0 distance.
    public override void ResolveInput(float orbitX, float orbitY, float lookatX, float lookatY)
    {
        //apply input values to orbit points
        m_cameraPoint.Increment(orbitX, orbitY);
        m_lookAtPoint.Increment(lookatX, lookatY);

        //offset and position camera around followObject(Player)
        Vector3 CameraWantsToMoveHere = m_followObject.transform.position + (Vector3)(m_followObject.transform.localToWorldMatrix * m_cameraPoint.ReturnTargetPoint());

        Vector3 CameraWantsToLookAtThis = m_followObject.transform.localToWorldMatrix * m_lookAtPoint.ReturnTargetPoint();


        MoveCameraTowards(CameraWantsToMoveHere);

        CameraLooksTowards(CameraWantsToLookAtThis);

        //rotate follow object to camera's direction vector
        FollowObjectLooksTowards(CameraWantsToLookAtThis);
    }
}
