using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraBehavoir
{
    //these values set in inspector
    [SerializeField]
    protected OrbitPoint m_lookAtPoint = new OrbitPoint();
    [SerializeField]
    protected OrbitPoint m_cameraPoint = new OrbitPoint();


    protected GameObject m_camera;
    protected GameObject m_followObject;

    public virtual void ResolveInput(float orbitX, float orbitY, float lookatX, float lookatY)
    {
        m_cameraPoint.Increment(orbitX, orbitY);
        m_lookAtPoint.Increment(lookatX, lookatY);
    }

    public void SetOrbitObjects(GameObject followObject, GameObject camera)
    {
        m_followObject = followObject;
        m_camera = camera;
    }

    //Move camera towards new pos.
    protected void MoveCameraTowards(Vector3 cameraNewPos)
    {
        //Do lerping towards pos in future.

        m_camera.transform.position = cameraNewPos;
    }

    //turns camera towards where it needs to.
    protected void CameraLooksTowards(Vector3 lookAtPos)
    {
        //Do lerping towards pos in future.

        m_camera.transform.LookAt(lookAtPos);
    }

    protected void FollowObjectLooksTowards(Vector3 lookAtPos)
    {
        //Do lerping towards pos in future.

        m_followObject.transform.LookAt(lookAtPos);
    }
}

