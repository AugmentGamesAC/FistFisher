using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class CameraBehaviour
{
    public float m_obstacleCheckRadius = 0.5f;
    public Vector3 m_playerLocalObstructionMovePos = Vector3.zero;

    public TargetController m_targetController;
    public CameraBehaviour()
    {

    }

    public virtual void Init(ThirdPersonCamera camera, GameObject player)
    {
        m_camera = camera;
        m_player = player;

        m_targetController = m_player.GetComponent<TargetController>();

        //this is important so you don't snap camera pos to player. Set these in inspector.
        m_raycastHitMask = ~LayerMask.GetMask("Player", "Ignore Raycast", "Water", "BoatMapOnly");
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
        return m_camera.transform.rotation.eulerAngles;
    }

    public virtual bool UsesStandardControlRotation()
    {
        return true;
    }

    //Raycasts from player pos to camera pos to check for obstacles to obstruct vision.
    protected float HandleObstacles()
    {
        Vector3 rayStart = m_player.transform.TransformPoint(m_playerLocalObstructionMovePos);
        Vector3 rayEnd = m_camera.transform.position;

        //direction between player and camera.
        Vector3 rayDir = rayEnd - rayStart;

        //how far to shoot raycast.
        float rayDist = rayDir.magnitude;


        if(rayDist <= 0.0f)
        {
            return 0.0f;
        }

        rayDir /= rayDist;

        RaycastHit[] hitInfos = Physics.SphereCastAll(rayStart, m_obstacleCheckRadius, rayDir, rayDist, m_raycastHitMask);
        if(hitInfos.Length <= 0)
        {
            return rayDist;
        }

        float minMoveUpDist = float.MaxValue;
        foreach (RaycastHit hitInfo in hitInfos)
        {
            if(hitInfo.collider.isTrigger==false)
                minMoveUpDist = Mathf.Min(minMoveUpDist, hitInfo.distance);
        }

        //set camera in front of the closest hit info from sphere cast.
        if(minMoveUpDist < float.MaxValue)
        {
            m_camera.transform.position = rayStart + rayDir * minMoveUpDist;
        }

        Debug.DrawLine(rayStart, rayEnd, Color.red);

        return minMoveUpDist;
    }

    protected ThirdPersonCamera m_camera;

    protected GameObject m_player;

    int m_raycastHitMask;
}
