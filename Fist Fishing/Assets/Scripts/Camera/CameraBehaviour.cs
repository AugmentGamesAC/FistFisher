using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraBehaviour
{
    public float ObstacleCheckRadius = 0.5f;
    public Vector3 PlayerLocalObstructionMovePos = Vector3.zero;
    public CameraBehaviour()
    {

    }

    public virtual void Init(ThirdPersonCamera camera, GameObject player)
    {
        m_Camera = camera;
        m_Player = player;

        //this is important so you don't snap camera pos to player. Set these in inspector.
        m_RaycastHitMask = ~LayerMask.GetMask("Player", "Ignore Raycast", "Water");
    }

    public virtual void Activate()
    {

    }

    public virtual void Deactivate()
    {

    }

    public abstract void UpdateCamera();
    public abstract void UpdateRotation(float yawAmount, float pitchAmount);
    public abstract void SetFacingDirection(Vector3 direction);

    //this is useful to place player in correct rotation.
    public virtual Vector3 GetControlRotation()
    {
        return m_Camera.transform.rotation.eulerAngles;
    }

    public virtual bool UsesStandardControlRotation()
    {
        return true;
    }

    //Raycasts from player pos to camera pos to check for obstacles to obstruct vision.
    protected float HandleObstacles()
    {
        Vector3 rayStart = m_Player.transform.TransformPoint(PlayerLocalObstructionMovePos);
        Vector3 rayEnd = m_Camera.transform.position;

        //direction between player and camera.
        Vector3 rayDir = rayEnd - rayStart;

        //how far to shoot raycast.
        float rayDist = rayDir.magnitude;


        if(rayDist <= 0.0f)
        {
            return 0.0f;
        }

        rayDir /= rayDist;

        RaycastHit[] hitInfos = Physics.SphereCastAll(rayStart, ObstacleCheckRadius, rayDir, rayDist, m_RaycastHitMask);
        if(hitInfos.Length <= 0)
        {
            return rayDist;
        }

        float minMoveUpDist = float.MaxValue;
        foreach (RaycastHit hitInfo in hitInfos)
        {
            minMoveUpDist = Mathf.Min(minMoveUpDist, hitInfo.distance);
        }

        //set camera in front of the closest hit info from sphere cast.
        if(minMoveUpDist < float.MaxValue)
        {
            m_Camera.transform.position = rayStart + rayDir * minMoveUpDist;
        }

        Debug.DrawLine(rayStart, rayEnd, Color.red);

        return minMoveUpDist;
    }

    protected ThirdPersonCamera m_Camera;

    protected GameObject m_Player;

    int m_RaycastHitMask;
}
