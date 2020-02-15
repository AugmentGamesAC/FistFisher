using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraBehavoir
{
    //these values set in inspector
    [SerializeField]
    protected OrbitPoint m_lookAtPoint;
    [SerializeField]
    protected OrbitPoint m_cameraPoint;

    protected GameObject m_camera;
    protected GameObject m_followObject;

    //made to initialize the orbit point values since new up above cancels inspector values.
    public CameraBehavoir(OrbitPoint lookAtPoint, OrbitPoint cameraPoint)
    {
        m_lookAtPoint = lookAtPoint;
        m_cameraPoint = cameraPoint;
    }

    public Vector3 GetCameraPos { get { return m_camera.transform.position; } }
    public Vector3 GetCameraLookatPos { get { return m_lookAtPoint.ReturnTargetPoint(); } }

    public virtual void ResolveInput(float orbitX, float orbitY, float lookatX, float lookatY)
    {
        m_cameraPoint.Increment(orbitX, orbitY);
        m_lookAtPoint.Increment(lookatX, lookatY);
    }

    //set GameObjects to work with and return this behaviour.
    public CameraBehavoir SetCamBehavObjects(GameObject followObject, GameObject camera)
    {
        m_followObject = followObject;
        m_camera = camera;

        return this;
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

}

