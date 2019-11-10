using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public FollowCameraBehaviour FollowCameraBehaviour;

    void Awake()
    {
        //Create a basic behaviour for our camera if it doesn't have one.
        if (FollowCameraBehaviour == null)
            FollowCameraBehaviour = new FollowCameraBehaviour();
    }

    void Update ()
    {
		
	}

    void LateUpdate()
    {
        if(m_Player == null)
        {
            return;
        }

        if(m_CurrentBehaviour != null)
        {
            //update camera on the behaviour does all the logic for following the player.
            m_CurrentBehaviour.UpdateCamera();

            //Get the degrees for each axis and set to control rotation.
            ControlRotation = m_CurrentBehaviour.GetControlRotation();
        }
    }

    public void SetPlayer(GameObject player)
    {
        //Store the player's position to a lookPosition for camera to look towards.
        m_Player = player;

        if(m_Player != null)
        {
            LookPos = m_Player.transform.position;
        }

        //behaviour needs to know about the player for updateCamera Logic.
        FollowCameraBehaviour.Init(this, m_Player);

        //Set our Camera behaviour type. Example: First, third, cinematic...
        SetCameraBehaviour(FollowCameraBehaviour);
    }

    //tells behaviour to update rotation based on inputs.
    public void UpdateRotation(float yawAmount, float pitchAmount)
    {
        if (m_CurrentBehaviour != null)
        {
            m_CurrentBehaviour.UpdateRotation(yawAmount, pitchAmount);
        }
    }

    //Tells Behaviour which direction to look towards.
    public void SetFacingDirection(Vector3 direction)
    {
        if (m_CurrentBehaviour != null)
        {
            m_CurrentBehaviour.SetFacingDirection(direction);
        }
    }

    public Vector3 ControlRotation { get; private set; }
    public Vector3 LookPos { get; set; }

    public Vector3 PivotRotation { get; set; }

    void SetCameraBehaviour(CameraBehaviour behaviour)
    {
        if (m_CurrentBehaviour == behaviour)
        {
            return;
        }

        if (m_CurrentBehaviour != null)
        {
            m_CurrentBehaviour.Deactivate();
        }

        m_CurrentBehaviour = behaviour;

        if (m_CurrentBehaviour != null)
        {
            m_CurrentBehaviour.Activate();
        }
    }

    CameraBehaviour m_CurrentBehaviour;
    GameObject m_Player;
}
