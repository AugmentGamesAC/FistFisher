﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// old camera code
/// this handled the camera bahaviour when a fish was targetted
/// </summary>
public class TargetingCameraBehaviour : CameraBehaviour
{
    public float m_cameraHorizPosEaseSpeed = 5.0f;
    public float m_cameraVertPosEaseSpeed = 4.0f;
    public float m_lookPosEaseSpeed = 5.0f;

    public Vector3 m_playerMaxDistLocalLookPos = Vector3.zero;
    public Vector3 m_playerMinDistLocalLookPos = Vector3.zero;

    public Vector3 m_playerLocalPivotPos = Vector3.zero;

    public float m_yawRotateSpeed = 1.0f;
    public float m_pitchRotateSpeed = 1.0f;
    public float m_maxVerticalAngle = 70.0f;

    public float m_maxDistFromPlayer = 6.0f;
    public float m_minHorizDistFromPlayer = 5.0f;

    public override void Activate()
    {
        base.Activate();

        m_goalPos = m_camera.transform.position;
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }

    public override void UpdateRotation(float yawAmount, float pitchAmount)
    {
        m_yawInput = yawAmount;
        m_pitchInput = pitchAmount;
    }

    public override void SetFacingDirection(Vector3 direction)
    {

    }

    public override Vector3 GetControlRotation()
    {
        return base.GetControlRotation();
    }

    public override bool UsesStandardControlRotation()
    {
        return false;
    }

    public void SetTargetingController(TargetController targetingController)
    {
        m_targetingController = targetingController;
    }

    

    //should return where the camera should go every frame to keep two objects inside viewPort.
    private Vector3 CalcCameraPos()
    {




        return Vector3.zero;
    }

    //finds point between player and targeted object.
    private Vector3 CalcTargetPosition()
    {
        Vector3 TowardsFish = m_targetingController.m_targetedFish.transform.position - m_player.transform.position;
        Vector3 dir = TowardsFish.normalized;
        float Halfdistance = TowardsFish.magnitude * 0.5f; //change 0.5f to Camera Aim or something and not hardcode.
        Vector3 posBetweenPlayerFish = dir * Halfdistance;

        //get direction vector3 between camera and newPoint.
        Vector3 TowardsBetweenPos = posBetweenPlayerFish - m_camera.transform.position;
        Vector3 NewCameraDir = TowardsBetweenPos.normalized;

        return Vector3.zero;
    }

    public override void UpdateCamera()
    {
        if (m_targetingController == null)
            return;

        //need to initalize targetubgController before this hits.
        //if (m_targetingController == null)
        //    return;

        //Vector3 TowardsFish = m_targetingController.m_targetedFish.transform.position - m_player.transform.position;
        //TowardsFish.Normalize();

        //need TowardsFish

        //player's pivot point.
        Vector3 worldPivotPos = m_player.transform.TransformPoint(m_playerLocalPivotPos);

        //Canmera's desired position is behind the player by OffSetFromPlayer.Magnitude();
        Vector3 offsetFromPlayer = m_goalPos - worldPivotPos;

        //distance
        float distFromPlayer = offsetFromPlayer.magnitude;

        //rotate amount is a fraction of the full desired rotation.
        //

        //changed this back to zero so you have no control when you lock on.
        Vector3 rotateAmount = Vector3.zero;

        //get current YRotation:
        Vector3 pivotRotation = m_camera.PivotRotation;

        //XMouse Movement passed on.
        //GetAmount of degrees rotated by last input call.
        pivotRotation.y += rotateAmount.y;

        //pivotRotation.y += m_Player.GroundAngularVelocity.y * Time.deltaTime;

        //Change rotation for YMouse MOvement
        pivotRotation.x -= rotateAmount.x;

        //Clamp so you can't spin vertically infinitely.
        pivotRotation.x = Mathf.Clamp(pivotRotation.x, -m_maxVerticalAngle, m_maxVerticalAngle);

        //Set our camera's angles to the values after input.
        m_camera.PivotRotation = pivotRotation;

        //Clamp a maximum distance for camera to be. 
        distFromPlayer = Mathf.Clamp(distFromPlayer, m_minHorizDistFromPlayer, m_maxDistFromPlayer);

        //Set Camera Position with offset and the rotation from input.
        offsetFromPlayer = Quaternion.Euler(pivotRotation.x, pivotRotation.y, 0.0f) * Vector3.forward;

        //Bring camera out by distance given. this can be modified for different camera behaviours.
        offsetFromPlayer *= distFromPlayer;

        //position for camera to be is in the back of worldPivotPos(Player's pivot point) by offsetFromPlayer
        m_goalPos = offsetFromPlayer + worldPivotPos;

        //Keep local variable of old Camera Position to be changed and applied.
        Vector3 newCameraPosition = m_camera.transform.position;

        //Change only x values to move towards m_GoalPos.
        newCameraPosition = MathUtils.SlerpToHoriz(
            m_cameraHorizPosEaseSpeed,
            newCameraPosition,
            m_goalPos,
            worldPivotPos,
            Time.deltaTime);

        //lerp to GoalPos y position.
        newCameraPosition.y = MathUtils.LerpTo(
            m_cameraVertPosEaseSpeed,
            newCameraPosition.y,
            m_goalPos.y,
            Time.deltaTime);

        //Set Camera's current position to new position now having input applied.
        m_camera.transform.position = newCameraPosition;

        //Deal with obstacles
        float moveUpDist = HandleObstacles();

        //Update Look Position
        {
            float lookPosPercent = moveUpDist / m_maxDistFromPlayer;
            Vector3 localLookPos = Vector3.Lerp(m_playerMinDistLocalLookPos, m_playerMaxDistLocalLookPos, lookPosPercent);

            Vector3 goalLookPos = m_targetingController.m_targetedFish.transform.position;

            //goalLookPos.y = m_Camera.LookPos.y;

            m_camera.LookPos = MathUtils.LerpTo(
                m_lookPosEaseSpeed,
                m_camera.LookPos,
                goalLookPos,
                Time.deltaTime
                );

            Vector3 lookDir = m_camera.LookPos - m_camera.transform.position;

            m_camera.transform.rotation = Quaternion.LookRotation(lookDir);
        }
    }

    Vector3 m_goalPos;
    TargetController m_targetingController;

    float m_yawInput;
    float m_pitchInput;

    float m_timeTillAutoRotate;
    bool m_allowAutoRotate;
}
