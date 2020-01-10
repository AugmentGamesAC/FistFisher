using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LockedCameraBehaviour : CameraBehaviour
{
    [SerializeField]
    protected OrbitPoint m_lookAtPoint;
    [SerializeField]
    protected OrbitPoint m_orbitPoint;
    
    public override void UpdateRotation(float yawAmount, float pitchAmount)
    {
        m_orbitPoint.Increment(yawAmount, pitchAmount);
    }

    public override void UpdateCamera()
    {
        m_camera.transform.position = m_player.transform.position + m_orbitPoint.ReturnTargetPoint();

        SetFacingDirection(m_player.transform.position + m_lookAtPoint.ReturnTargetPoint());
    }

    public override void SetFacingDirection(Vector3 goalPosition)
    {
        m_camera.transform.LookAt(goalPosition, Vector3.up);
    }
}
