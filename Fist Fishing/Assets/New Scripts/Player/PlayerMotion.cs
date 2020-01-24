using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class statclassPlaceholder
{
    public float Value = 30;
    public static implicit operator float(statclassPlaceholder reference)
    {
        return reference.Value;
    }
}
/// <summary>
/// Camera manage is to have a list of behaviors
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

    protected void ThrowBait()
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
        if (!ALInput.GetKey(ALInput.ManualCamera))
            ResolveSwimRotation();

        if (ALInput.GetKey(ALInput.Forward))
            m_rigidbody.MovePosition(transform.position + transform.up * Time.deltaTime * movementSpeedRef);
    }
    protected void FirstPersonMovement()
    {
        if (!ALInput.GetKey(ALInput.ManualCamera))
            ResolveSwimRotation();

        if (ALInput.GetKey(ALInput.Forward))
            m_rigidbody.MovePosition(transform.position + transform.up * Time.deltaTime * movementSpeedRef);
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
