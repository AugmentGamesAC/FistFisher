using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavoir
{
    //these values set in inspector
    [SerializeField]
    protected OrbitPoint m_lookAtPoint;
    [SerializeField]
    protected OrbitPoint m_cameraPoint;


    protected GameObject m_camera;
    protected GameObject m_followObject;

    public virtual void ResolveInput(float orbitX, float orbitY, float lookatX, float lookatY)
    {
        m_cameraPoint.Increment(orbitX, orbitY);
        m_lookAtPoint.Increment(lookatX, lookatY);
    }

    //Move camera towards new pos.
    protected void MoveCameraTowards(Vector3 CameraNewPos)
    {
        //Do lerping towards pos in future.

        m_camera.transform.position = CameraNewPos;
    }

    //turns camera towards where it needs to.
    protected void CameraLooksTowards(Vector3 LookAtPos)
    {
        //Do lerping towards pos in future.

        m_camera.transform.LookAt(LookAtPos);
    }

    protected void FollowObjectLooksTowards(Vector3 LookAtPos)
    {
        //Do lerping towards pos in future.

        m_followObject.transform.LookAt(LookAtPos);
    }
}

