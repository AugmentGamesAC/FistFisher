using UnityEngine;

[System.Serializable]
public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField]
    public LockedCameraBehaviour m_followCameraBehaviour;
    public TargetingCameraBehaviour m_targetingCameraBehaviour;

    public TargetController m_targetController;

    void Awake()
    {
        //Create a basic behaviour for our camera if it doesn't have one.
        if (m_followCameraBehaviour == null)
            m_followCameraBehaviour = new LockedCameraBehaviour();

        if (m_targetingCameraBehaviour == null)
            m_targetingCameraBehaviour = new TargetingCameraBehaviour();

    }

    //this overrides every camera change in any other class even in lateUpdate.
    void LateUpdate()
    {
        if (m_player == null)
        {
            return;
        }

        if (m_currentBehaviour != null)
        {
            //if targeting is active, UpdateTargetingCamera.
            if (m_targetController.m_targetingIsActive)
                //updateTargeting is the same as updateCamera but doesn't take input from player and only from fish pos direction.
                //change this to a behaviour instead of hard coded function toggles.
                SetCameraBehaviour(m_targetingCameraBehaviour); 
            else
                //update camera on the behaviour does all the logic for following the player.
                SetCameraBehaviour(m_followCameraBehaviour);

            m_currentBehaviour.UpdateCamera();
            //Get the degrees for each axis and set to control rotation.
            ControlRotation = m_currentBehaviour.GetControlRotation();
        }
    }

    public void SetPlayer(GameObject player)
    {
        //Store the player's position to a lookPosition for camera to look towards.
        m_player = player;

        if (m_player != null)
        {
            LookPos = m_player.transform.position;
        }

        //behaviour needs to know about the player for updateCamera Logic.
        m_followCameraBehaviour.Init(this, m_player);
        m_targetingCameraBehaviour.Init(this, m_player);

        //Set our Camera behaviour type. Example: First, third, cinematic...
        SetCameraBehaviour(m_followCameraBehaviour);

        if (m_targetController == null)
            m_targetController = m_player.GetComponent<TargetController>();

        m_targetingCameraBehaviour.SetTargetingController(m_targetController);
    }

    //tells behaviour to update rotation based on inputs.
    public void UpdateRotation(float yawAmount, float pitchAmount)
    {
        if (m_currentBehaviour != null)
        {
            m_currentBehaviour.UpdateRotation(yawAmount, pitchAmount);
        }
    }

    //Tells Behaviour which direction to look towards.
    public void SetFacingDirection(Vector3 direction)
    {
        if (m_currentBehaviour != null)
        {
            m_currentBehaviour.SetFacingDirection(direction);
        }
    }

    public Vector3 ControlRotation { get; private set; }
    public Vector3 LookPos { get; set; }

    public Vector3 PivotRotation;

    void SetCameraBehaviour(CameraBehaviour behaviour)
    {
        if (m_currentBehaviour == behaviour)
        {
            return;
        }

        if (m_currentBehaviour != null)
        {
            m_currentBehaviour.Deactivate();
        }

        m_currentBehaviour = behaviour;

        if (m_currentBehaviour != null)
        {
            m_currentBehaviour.Activate();
        }
    }

    CameraBehaviour m_currentBehaviour;
    GameObject m_player;
}
