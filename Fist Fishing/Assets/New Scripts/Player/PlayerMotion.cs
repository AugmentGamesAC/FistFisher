using System.Collections;
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
[System.Serializable,RequireComponent(typeof(Rigidbody))]
public class PlayerMotion : MonoBehaviour
{
    CameraManager m_vision;

    public bool m_CanMove;
    public bool m_CanMoveForward;

    protected statclassPlaceholder turningSpeedRef = new statclassPlaceholder();
    protected statclassPlaceholder movementSpeedRef = new statclassPlaceholder();

    protected Dictionary<CameraManager.CameraState, System.Action> m_movementResoultion;
    protected Rigidbody m_rigidbody;

    [SerializeField]
    protected 



    /// <summary>
    /// gets the camera manager on the main camera, then sets up a dictionary of all the possible camera states paired to movement resolution functions
    /// </summary>
    public void Start()
    {
        m_vision = Camera.main.GetComponent<CameraManager>();
        m_movementResoultion = new Dictionary<CameraManager.CameraState, System.Action>()
        {
           {CameraManager.CameraState.Abzu, AbzuMovement },
           {CameraManager.CameraState.FirstPerson, FirstPersonMovement },
           {CameraManager.CameraState.Locked, LockedMovement },
           {CameraManager.CameraState.Warthog, AbzuMovement },
        };
        m_rigidbody = GetComponent<Rigidbody>();

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
            throw new System.NotImplementedException("Camera state not reconized by Player motion ");

        MoveResolution();
    }

    #region common Actions
    protected void CommonActions()
    {

    }

    protected void SwapUI()
    {

    }
    #endregion


    protected void AbzuMovement()
    {
        if (!ALInput.GetKey(ALInput.ManualCamera))
            ResolveSwimRotation();

        if (ALInput.GetKey(ALInput.Forward))
            m_rigidbody.MovePosition(transform.position + transform.up * Time.deltaTime * movementSpeedRef);
    }
    protected void LockedMovement()
    {
        if (!ALInput.GetKey(ALInput.ManualCamera))
            ResolveSwimRotation();

        if (ALInput.GetKey(ALInput.Forward))
            m_rigidbody.MovePosition(transform.position + transform.up * Time.deltaTime * movementSpeedRef);
    }
    protected void WarthogMovement()
    {
        //hopefully will rotate the frog to be looking facedown towards object
        transform.LookAt(m_vision.LookAtWorldTransform, Vector3.forward);

        m_rigidbody.MovePosition(transform.position + transform.up * Time.deltaTime * movementSpeedRef *
            ALInput.GetAxis(ALInput.AxisCode.Vertical));
    }

    protected void XZDirectional()
    {

    }


    protected void FirstPersonMovement()
    {
        //hopefully will rotate the frog to be looking facedown towards object
        transform.LookAt(m_vision.LookAtWorldTransform, Vector3.forward);

        if (ALInput.GetKey(ALInput.Forward))
            m_rigidbody.MovePosition(transform.position + transform.forward * Time.deltaTime * movementSpeedRef);
    }

    void ResolveSwimRotation()
    {
        Vector3 desiredDirection = new Vector3
        (
            ALInput.GetAxis(ALInput.AxisCode.MouseY),
            0, // no touch Y
            ALInput.GetAxis(ALInput.AxisCode.MouseX)
        ) * turningSpeedRef * Time.deltaTime;

        if (desiredDirection.sqrMagnitude > 0.000001)
            transform.Rotate(desiredDirection, Space.Self);
    }


}
