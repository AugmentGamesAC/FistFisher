using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Camera manage is to have a list of behaviors
/// we are using input controls to switch states
/// </summary>
[System.Serializable]
public class CameraManager : MonoBehaviour, ISerializationCallbackReceiver
{
    protected enum CameraState
    {
        Abzu,
        Locked,
        Warthog,
        FirstPerson
    }

    [SerializeField]
    protected GameObject FollowObject;
    [SerializeField]
    protected GameObject CameraObject;
    [SerializeField]
    protected CameraState _currentState;
    [SerializeField]
    protected CameraBehavoir currentBehavoir;

    [SerializeField]
    protected CameraBehavoir m_abzu = new AbzuCameraBehaviour(new OrbitPoint(0f, -90f, 10f, 180f, -180f, 180f, -180f), new OrbitPoint(0f, 110f, 10f, 180f, -180f, 360f, -360f));
    [SerializeField]
    protected CameraBehavoir m_locked = new LockedCameraBehaviour(new OrbitPoint(0f, -90f, 10f, 180f, -180f, 180f, -180f), new OrbitPoint(0f, 110f, 10f, 180f, -180f, 360f, -360f));
    [SerializeField]
    protected CameraBehavoir m_warthog = new WarthogCameraBehaviour(new OrbitPoint(0f, -90f, 10f, 180f, -180f, 180f, -180f), new OrbitPoint(0f, 110f, 10f, 180f, -180f, 360f, -360f));
    [SerializeField]
    protected CameraBehavoir m_firstPerson = new FirstPersonCameraBehaviour(new OrbitPoint(0f, -90f, 10f, 180f, -180f, 180f, -180f), new OrbitPoint(0f, 110f, 10f, 180f, -180f, 360f, -360f));


    Dictionary<CameraState, CameraBehavoir> StateHolder; 

    public void OnBeforeSerialize()
    {
        InitStateHolderIfNeeded();

        SwitchState(_currentState);
    }


    protected void InitStateHolderIfNeeded()
    {
        if (StateHolder != null)
            return;

        //Setup Dictionary while setting their values
        StateHolder = new Dictionary<CameraState, CameraBehavoir>()
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
        if (StateHolder == default)
            return;

        _currentState = newState;
        if (!StateHolder.TryGetValue(_currentState, out currentBehavoir))
            throw new System.InvalidOperationException("state not found in StateHolder ");
    }


    public void Awake()
    {
        InitStateHolderIfNeeded();
        SwitchState(CameraState.Abzu);
    }

    public void Update()
    {
        if (ALInput.GetKeyDown(ALInput.Abzu))
            SwitchState(CameraState.Abzu);
        else if (ALInput.GetKeyDown(ALInput.Locked))
            SwitchState(CameraState.Locked);
        else if (ALInput.GetKeyDown(ALInput.Warthog))
            SwitchState(CameraState.Warthog);
        else if (ALInput.GetKeyDown(ALInput.FirstPerson))
            SwitchState(CameraState.FirstPerson);

        Vector3 LookInputVec = ALInput.GetDirection(ALInput.DirectionCode.LookInput);//place holder

        currentBehavoir.ResolveInput
            (LookInputVec.x,
            LookInputVec.y,
            LookInputVec.x,
           LookInputVec.y);
    }

}
