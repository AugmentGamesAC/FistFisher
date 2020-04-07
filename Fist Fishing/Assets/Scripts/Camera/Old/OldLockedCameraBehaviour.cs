using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// old camera code
/// this handled the camera behaviour when player was locked into facing a specific object
/// </summary>
[System.Serializable]
public class OldLockedCameraBehaviour : CameraBehaviour
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
        m_camera.transform.position = m_player.transform.position + (Vector3)(m_player.transform.localToWorldMatrix * m_orbitPoint.ReturnTargetPoint());

        //m_camera.transform.RotateAround()

        ///uses local to world to set the rotation to be based on the game object
        SetFacingDirection(m_player.transform.localToWorldMatrix * m_lookAtPoint.ReturnTargetPoint());
    }

    public override void SetFacingDirection(Vector3 goalPosition)
    {
        m_camera.transform.LookAt(m_player.transform.position + goalPosition, Vector3.up);
    }
}
