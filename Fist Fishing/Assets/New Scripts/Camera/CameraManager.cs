using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Camera manager is to have a list of behaviors
/// we are using input controls to switch states
/// </summary>
[System.Serializable]
public class CameraManager : MonoBehaviour, ISerializationCallbackReceiver
{
    /// <summary>
    /// used to determine both camera behaviour and player motion behaviour
    /// </summary>
    public enum CameraState
    {
        Abzu,
        Locked,
        Warthog,
        FirstPerson,
    }

    //For testing only
    public bool m_controllerToggle = false;

    [SerializeField]
    protected GameObject FollowObject;
    [SerializeField]
    protected GameObject CameraObject;
    [SerializeField]
    protected CameraState _currentState;
    public CameraState CurrentState { get { return _currentState; } }

    [SerializeField]
    protected CameraBehavior currentBehavior;

    [SerializeField]
    protected CameraBehavior m_abzu = new AbzuCameraBehaviour(new OrbitPoint(0f, -90f, 10f, 180f, -180f, 180f, -180f), new OrbitPoint(0f, 110f, 10f, 180f, -180f, 360f, -360f));
    [SerializeField]
    protected CameraBehavior m_locked = new LockedCameraBehaviour(new OrbitPoint(0f, -90f, 10f, 180f, -180f, 180f, -180f), new OrbitPoint(0f, 110f, 10f, 180f, -180f, 360f, -360f));
    [SerializeField]
    protected CameraBehavior m_warthog = new WarthogCameraBehaviour(new OrbitPoint(0f, -90f, 10f, 180f, -180f, 180f, -180f), new OrbitPoint(0f, 110f, 10f, 180f, -180f, 360f, -360f));
    [SerializeField]
    protected CameraBehavior m_firstPerson = new FirstPersonCameraBehaviour(new OrbitPoint(0f, -90f, 10f, 180f, -180f, 180f, -180f), new OrbitPoint(0f, 110f, 10f, 180f, -180f, 360f, -360f));


    Dictionary<CameraState, CameraBehavior> StateHolder; 

    public void OnBeforeSerialize()
    {
        SwitchState(_currentState);
    }

    public Vector3 LookAtWorldTransform { get { return FollowObject.transform.position +  currentBehavior.GetCameraLookatPos; } }
    public Vector3 CameraPos { get { return FollowObject.transform.position + currentBehavior.GetCameraPos; } }


    protected void InitStateHolderIfNeeded()
    {
        if (StateHolder != null)
            return;

        //Setup Dictionary while setting their values
        StateHolder = new Dictionary<CameraState, CameraBehavior>()
        {
            {CameraState.Abzu, m_abzu.SetCamBehavObjects(FollowObject, CameraObject) },
            {CameraState.Locked, m_locked.SetCamBehavObjects(FollowObject, CameraObject) },
            {CameraState.Warthog, m_warthog.SetCamBehavObjects(FollowObject, CameraObject) },
            {CameraState.FirstPerson, m_firstPerson.SetCamBehavObjects(FollowObject, CameraObject) }
        };
    }


    public void OnAfterDeserialize() { }

    protected void SwitchState(CameraState newState)
    {
        InitStateHolderIfNeeded();

        _currentState = newState;
        if (!StateHolder.TryGetValue(_currentState, out currentBehavior))
            throw new System.InvalidOperationException("state not found in StateHolder ");
    }


    public void Awake()
    {
        InitStateHolderIfNeeded();
        SwitchState(CameraState.Warthog);
    }

    public void Update()
    {
        if (NewMenuManager.PausedActiveState)
            return;

            if (Input.GetKeyDown(KeyCode.Backspace))
            m_controllerToggle = !m_controllerToggle;

        if (ALInput.GetKeyDown(ALInput.Toggle))
        {
            if (CurrentState == CameraState.FirstPerson)
                SwitchState(CameraState.Warthog);
            else
                SwitchState(CameraState.FirstPerson);
        }
        Vector2 lookInputVec = new Vector2(ALInput.GetAxis(ALInput.AxisType.LookHorizontal), ALInput.GetAxis(ALInput.AxisType.LookVertical));
        
        currentBehavior.ResolveInput
            (lookInputVec.x,
            lookInputVec.y,
            lookInputVec.x,
           lookInputVec.y);
    }

}
