using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedCameraBehaviour : CameraBehaviour
{

    public GameObject m_lookAtObject;
    public GameObject m_orbitObject;
    float m_distance;
    float m_yaw;
    float m_yawInput;
    float m_pitchInput;
    float m_pitch;
    float m_angleSpeed = 5.0f;
    float m_maxVerticalAngle = 60.0f;
    float m_offsetHeight = 3.0f;
    public override void UpdateRotation(float yawAmount, float pitchAmount)
    {
        m_yawInput = yawAmount;
        m_pitchInput = pitchAmount;
    }

    //Yaw and pitch should be the input multiplied by speed
    public void IncrementRotation(float yawInput, float pitchInput)
    {

        if (yawInput == 0 || pitchInput == 0)
        {
            return;
        }

        //Set Pitch - Clamp Pitch to 89 degrees
        m_pitch = Mathf.Clamp(m_pitch - (pitchInput * m_angleSpeed), -m_maxVerticalAngle, m_maxVerticalAngle);

        //Set Yaw.
        m_yaw += yawInput * m_angleSpeed;
    }

    public void SetOffset()
    {
        Vector3 offset = m_camera.transform.position - m_orbitObject.transform.position;


        m_distance = Mathf.Clamp(offset.magnitude, 5, 7);

        offset = Quaternion.Euler(m_pitch, m_yaw, 0.0f) * Vector3.forward;

        //Bring camera out by distance given. this can be modified for different camera behaviours.

        offset *= m_distance;

        m_camera.transform.position = offset + m_orbitObject.transform.position;
        m_camera.transform.position += m_camera.transform.up * m_offsetHeight;
        m_lookAtObject.transform.position = -offset + m_orbitObject.transform.position;
    }

    public override void UpdateCamera()
    {
        IncrementRotation(ALInput.GetAxis(ALInput.AxisCode.MouseX), ALInput.GetAxis(ALInput.AxisCode.MouseY));
        SetOffset();
        m_camera.transform.LookAt(m_lookAtObject.transform);
        m_orbitObject.transform.up = m_camera.transform.forward;
    }

    public override void SetFacingDirection(Vector3 direction)
    {
        throw new System.NotImplementedException();
    }
}
