﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class appears to be a placeholder to store a float value and get it.
/// thing replacing this has more functionality that gives reason for it to exist
/// </summary>
public class statclassPlaceholder
{
    public float Value = 30;
    public static implicit operator float(statclassPlaceholder reference)
    {
        return reference.Value;
    }
}
/// <summary>
/// Camera manager is to have a list of behaviors
/// we are using input controls to switch states
/// </summary>
[System.Serializable]
public class PlayerMotion : MonoBehaviour
{
    CameraManager m_vision;

    public bool m_CanMove;
    public bool m_CanMoveForward;

    protected statclassPlaceholder turningSpeedRef = new statclassPlaceholder();
    protected statclassPlaceholder movementSpeedRef = new statclassPlaceholder();

    protected Dictionary<CameraManager.CameraState, System.Action> m_movementResoultion;


    /// <summary>
    /// gets the camera manager on the main camera, then sets up a dictionary of all the possible camera states paired to movement resolution functions
    /// </summary>
    public void Start()
    {
        m_vision = Camera.main.GetComponent<CameraManager>();
        m_movementResoultion = new Dictionary<CameraManager.CameraState, System.Action>()
        {
           {CameraManager.CameraState.FirstPerson, FirstPersonMovement },
           {CameraManager.CameraState.Warthog, WarthogMovement },
        };
        turningSpeedRef.Value = 180.0f;
    }
    /// <summary>
    /// creates a system.action (essentiually function with no in/out - funct ptr) 
    /// sets it to the movement resolution associated to the current camera state if valid
    /// runs the move resloution funct found
    /// </summary>
    public void FixedUpdate()
    {
        if (!m_CanMove)
            return;

        System.Action MoveResolution;
        if (!m_movementResoultion.TryGetValue(m_vision.CurrentState, out MoveResolution))
            throw new System.NotImplementedException("Camera state not recognized by Player motion ");

        MoveResolution();
    }


    public void Update()
    {
        if (!m_CanMove)
            return;

        if (Configurations.IsThisPressed(ActionID.OpenInventory))
            ToggleInventoryDisplay();

        if (Configurations.IsThisPressed(ActionID.Action))
            NewMenuManager.DisplayMenuScreen(MenuScreens.Combat);
    }

    protected bool m_displayInventory;

    protected void ToggleInventoryDisplay()
    {
        m_displayInventory = !m_displayInventory;
        SwapUI();
    }
    protected void SwapUI()
    {
        MenuScreens desiredMenu = (m_displayInventory) ? MenuScreens.SwimmingInventory : MenuScreens.NormalHUD;
        NewMenuManager.DisplayMenuScreen(desiredMenu);
    }

    protected void WarthogMovement() 
    {
        //hopefully will rotate the frog to be looking facedown towards object
        transform.LookAt(m_vision.LookAtWorldTransform, Vector3.up);

        XZDirectional();
    }

    protected void FirstPersonMovement()
    {
        //hopefully will rotate the frog to be looking facedown towards object
        transform.LookAt(m_vision.LookAtWorldTransform, Vector3.up);

        XZDirectional();
    }


    protected void XZDirectional()
    {
        //Forward movement
        Vector3 desiredMovement = transform.forward * Time.deltaTime * movementSpeedRef * ((ALInput.GetKey(ALInput.Forward) ? 1 : 0) - (ALInput.GetKey(ALInput.Backward) ? 0.2f : 0)) ;

        //Left Right
        desiredMovement += transform.right * Time.deltaTime * movementSpeedRef * ((ALInput.GetKey(ALInput.GoRight) ? 0.2f : 0) - (ALInput.GetKey(ALInput.GoLeft) ? 0.2f : 0));

        //ascend descend.
        desiredMovement += Vector3.up * Time.deltaTime * movementSpeedRef * ((ALInput.GetKey(ALInput.Ascend) ? 0.5f : 0) - (ALInput.GetKey(ALInput.Descend) ? 0.5f : 0));

        //apply movement vector

        Vector3 desiredMovement2 = Configurations.AxisDirections(ActionID.MovementOrInventoryNavigation) * Time.deltaTime * movementSpeedRef;

        /*float fb = desiredMovement2.z;
        if (desiredMovement2.z < 0)
            fb *= 0.2f;
        Vector3 desiredMovement = transform.forward * Time.deltaTime * movementSpeedRef * fb;*/


        //Debug.Log(desiredMovement2.x + ", " + desiredMovement2.y + ", " +desiredMovement2.z);
        /*if (desiredMovement2!=Vector3.zero)
            transform.position += desiredMovement2;*/
        transform.position += desiredMovement;
    }



    void ResolveSwimRotation()
    {
        Vector3 desiredDirection = new Vector3
        (
            /*ALInput.GetAxis(ALInput.AxisCode.MouseY)*/Configurations.AxisDirections(ActionID.CameraRotation).y,
            /*ALInput.GetAxis(ALInput.AxisCode.MouseX)*/Configurations.AxisDirections(ActionID.CameraRotation).x,
            0
        ) * turningSpeedRef * Time.deltaTime;

        if (desiredDirection.sqrMagnitude > 0.000001)
            transform.Rotate(desiredDirection, Space.Self);
    }


}
