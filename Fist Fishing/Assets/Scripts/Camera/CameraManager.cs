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

       
    Dictionary<CameraState, CameraBehavoir> StateHolder = new Dictionary<CameraState, CameraBehavoir>()
    {
        {CameraState.Abzu, new AbzuCameraBehaviour(new OrbitPoint(0f, -90f, 10f, 180f, -180f, 180f, -180f), new OrbitPoint(0f, 110f, 10f, 180f, -180f, 360f, -360f)) },
        {CameraState.Locked, new LockedCameraBehaviour(new OrbitPoint(0f, -90f, 10f, 180f, -180f, 180f, -180f), new OrbitPoint(0f, 110f, 10f, 180f, -180f, 360f, -360f)) },
        {CameraState.Warthog, new WarthogCameraBehaviour(new OrbitPoint(0f, -90f, 10f, 180f, -180f, 180f, -180f), new OrbitPoint(0f, 110f, 10f, 180f, -180f, 360f, -360f)) },
        {CameraState.FirstPerson, new FirstPersonCameraBehaviour(new OrbitPoint(0f, -90f, 10f, 180f, -180f, 180f, -180f), new OrbitPoint(0f, 110f, 10f, 180f, -180f, 360f, -360f)) }
    };


    public void OnBeforeSerialize()
    {
        SwitchState(_currentState);
    }

    public void OnAfterDeserialize() { }




    protected void SwitchState(CameraState newState)
    {
        _currentState = newState;
        if (!StateHolder.TryGetValue(_currentState, out currentBehavoir))
            throw new System.InvalidOperationException("state not found in StateHolder ");
    }


    public void Awake()
    {
        SwitchState(CameraState.Abzu);

        foreach (KeyValuePair< CameraState, CameraBehavoir > stateHolderVals in StateHolder)
        {
            stateHolderVals.Value.SetCamBehavObjects(FollowObject, CameraObject);
        }
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
