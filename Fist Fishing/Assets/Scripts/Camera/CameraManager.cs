using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Camera manage is to have a list of behaviors
/// we are using input controls to switch states
/// </summary>
[System.Serializable]
public class CameraManager : MonoBehaviour
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

    protected CameraState _currentState;
    [SerializeField]
    protected CameraBehavoir currentBehavoir;

    Dictionary<CameraState, CameraBehavoir> StateHolder = new Dictionary<CameraState, CameraBehavoir>()
    {
        {CameraState.Abzu, new AbzuCameraBehaviour() },
        {CameraState.Locked, new LockedCameraBehaviour() },
        {CameraState.Warthog, new WarthogCameraBehaviour() },
        {CameraState.FirstPerson, new FirstPersonCameraBehaviour() }
    };
    protected void SwitchState(CameraState newState)
    {
        _currentState = newState;
        if (!StateHolder.TryGetValue(_currentState, out currentBehavoir))
            throw new System.InvalidOperationException("state not found in StateHolder ");
    }

    public void Start()
    {
        SwitchState(CameraState.Abzu);

        foreach (KeyValuePair< CameraState, CameraBehavoir > stateHolderVals in StateHolder)
        {
            stateHolderVals.Value.SetOrbitObjects(FollowObject, CameraObject);
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
