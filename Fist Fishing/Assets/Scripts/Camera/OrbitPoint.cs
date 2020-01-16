using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OrbitPoint
{
    [SerializeField]
    protected float m_yawRotationAroundPivit;
    [SerializeField]
    protected float m_pitchRotationAroundPivit;
    [SerializeField]
    protected float m_distanceFromPivot;
    [SerializeField]
    protected float m_maxPitch;
    [SerializeField]
    protected float m_minPitch;
    [SerializeField]
    protected float m_maxYaw;
    [SerializeField]
    protected float m_minYaw;

    public void Increment(float yaw, float pitch)
    {
        m_yawRotationAroundPivit = Mathf.Clamp(m_yawRotationAroundPivit + yaw, m_minYaw, m_maxYaw);
        m_pitchRotationAroundPivit = Mathf.Clamp(m_pitchRotationAroundPivit + pitch, m_minPitch, m_maxPitch);
    }

    /// <summary>
    /// the purpose of return target point it to calcuate the desired location of an object based on a rotation given a pitch and yaw and a distance
    /// </summary>
    /// <returns>returns a direction from a point in the rotation which is a unit vector distance * distance away  </returns>
    public Vector3 ReturnTargetPoint()
    {
        return Quaternion.Euler(m_pitchRotationAroundPivit, m_yawRotationAroundPivit, 0.0f) * Vector3.forward * m_distanceFromPivot;
    }
}
