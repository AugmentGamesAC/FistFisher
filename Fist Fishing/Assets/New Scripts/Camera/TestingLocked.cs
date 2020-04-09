using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// unused camera mode
/// used for testing various camera controls
/// </summary>
[System.Serializable]
public class TestingLocked : OldLockedCameraBehaviour
{
    [SerializeField]
    protected GameObject lookatPoint;
    [SerializeField]
    protected GameObject cameraPoint;

    [SerializeField]
    protected bool IsDisplayingLookatPoint;
    [SerializeField]
    protected bool IsDisplayingCameraPoint;

    public override void Activate()
    {
        base.Activate();
        if (lookatPoint == default)
            lookatPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        if (cameraPoint == default)
            cameraPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    }


    public void Update()
    {
        lookatPoint.SetActive(!IsDisplayingLookatPoint);
        cameraPoint.SetActive(!IsDisplayingCameraPoint);
    }

    public override void UpdateCamera()
    {
        base.UpdateCamera();
        cameraPoint.transform.position = m_camera.transform.position;
    }

    public override void SetFacingDirection(Vector3 goalPosition)
    {
        lookatPoint.transform.position = m_player.transform.position + goalPosition;
        m_camera.transform.LookAt(lookatPoint.transform.position, Vector3.up);
    }
}
