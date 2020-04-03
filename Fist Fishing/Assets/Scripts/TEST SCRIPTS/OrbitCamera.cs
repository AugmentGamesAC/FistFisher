using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OrbitCamera : MonoBehaviour
{
   
    public GameObject m_LookAtObject;
    public GameObject m_OrbitObject;
    float m_Distance;
    float m_Yaw;
    float m_Pitch;
    float m_AngleSpeed = 5.0f;
    float m_maxVerticalAngle = 60.0f;
    float m_OffsetHeight = 3.0f;

    //Yaw and pitch should be the input multiplied by speed
    public void IncrementRotation(float yawInput, float pitchInput)
    {

        if (yawInput == 0 || pitchInput == 0)
        {
            return;
        }

            //Set Pitch - Clamp Pitch to 89 degrees
            m_Pitch = Mathf.Clamp(m_Pitch - (pitchInput * m_AngleSpeed), -m_maxVerticalAngle, m_maxVerticalAngle);
        //Set Yaw
        
        m_Yaw += yawInput * m_AngleSpeed;

        

    }

    public void SetOffset()
    {
        Vector3 offset = transform.position - m_OrbitObject.transform.position;


        m_Distance = Mathf.Clamp(offset.magnitude, 5, 7);

        offset = Quaternion.Euler(m_Pitch, m_Yaw, 0.0f) * Vector3.forward;

        //Bring camera out by distance given. this can be modified for different camera behaviours.

        offset *= m_Distance;

        transform.position = offset + m_OrbitObject.transform.position;
       transform.position += transform.up * m_OffsetHeight;
        m_LookAtObject.transform.position = -offset + m_OrbitObject.transform.position; 
    }



    void LateUpdate()
    {
        IncrementRotation(ALInput.GetAxis(ALInput.AxisType.LookHorizontal), ALInput.GetAxis(ALInput.AxisType.LookVertical));
        SetOffset();
        transform.LookAt(m_LookAtObject.transform);
        m_OrbitObject.transform.up = transform.forward;

    }
}
