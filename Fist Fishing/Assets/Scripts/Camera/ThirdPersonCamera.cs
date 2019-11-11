using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public FollowCameraBehaviour m_followCameraBehaviour;

    void Awake()
    {
        //Create a basic behaviour for our camera if it doesn't have one.
        if (m_followCameraBehaviour == null)
            m_followCameraBehaviour = new FollowCameraBehaviour();
    }

    void Update ()
    {
		
	}

    void LateUpdate()
    {
        if(m_player == null)
        {
            return;
        }

        if(m_currentBehaviour != null)
        {
            //update camera on the behaviour does all the logic for following the player.
            m_currentBehaviour.UpdateCamera();

            //Get the degrees for each axis and set to control rotation.
            ControlRotation = m_currentBehaviour.GetControlRotation();
        }
    }

    public void SetPlayer(GameObject player)
    {
        //Store the player's position to a lookPosition for camera to look towards.
        m_player = player;

        if(m_player != null)
        {
            LookPos = m_player.transform.position;
        }

        //behaviour needs to know about the player for updateCamera Logic.
        m_followCameraBehaviour.Init(this, m_player);

        //Set our Camera behaviour type. Example: First, third, cinematic...
        SetCameraBehaviour(m_followCameraBehaviour);
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

    public Vector3 PivotRotation { get; set; }

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
