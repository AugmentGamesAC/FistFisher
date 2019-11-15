using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController m_characterController;

    public GameObject m_player;
    public GameObject m_playerBody;
    public Vector3 m_boatMountPosition;
    public Vector3 m_boatDismountPosition;

    private ThirdPersonCamera m_camera;

    public float m_walkSpeed = 10.0f;
    public float m_sprintSpeed = 15.0f;
    public float m_fastSwimSpeed = 8.0f;
    public float m_swimSpeed = 5.0f;

    public float m_gravity = -9.81f;
    public float m_terminalVelocity = 50.0f;
    public float mountCooldown = 0.0f;

    public bool m_isSwimming = false;
    public bool m_isGrounded = false;
    public bool m_isMounted = false;
    public bool m_canMount = false;

    Vector3 m_velocity = Vector3.zero;

    public Transform m_groundCheck;
    public float m_groundDistance = 0.4f;
    public LayerMask m_groundMask;

    private void Start()
    {
        m_camera = Camera.main.GetComponent<ThirdPersonCamera>();

        Cursor.lockState = CursorLockMode.Locked;

        if (m_player == null)
            m_player = gameObject;

        m_camera.SetPlayer(m_player);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateIsGrounded();

        UpdateCamera();

        if (!m_isMounted)
        {
            if (m_isSwimming)
                Swim();
            else
            {
                ApplyGravity();
                if (IsSprinting())
                    Sprint();
                else
                    Walk();
            }
        }

        mountCooldown -= Time.deltaTime;

        if (mountCooldown < 0.0f)
            UpdateBoatMountStatus();
    }

    private void Mount()
    {
        //teleport to boat seat.
        transform.position = m_boatMountPosition;

        //player is now mounted and shouldn't be able to move until dismount.
        m_isMounted = true;

        m_canMount = false;
    }

    private void Dismount()
    {
        //go to diving position
        transform.position = m_boatDismountPosition;

        //no longer mounted on the boat.
        m_isMounted = false;
    }

    private void UpdateBoatMountStatus()
    {
        //if pressing mount button and allowed to mount.
        if (m_canMount && Input.GetButton("Mount") && !m_isMounted)
        {
            Mount();

            mountCooldown = 2.0f;
        }
        //if i am mounted and pressing the mount button.
        else if (!m_canMount && Input.GetButton("Mount") && m_isMounted)
        {
            Dismount();

            mountCooldown = 2.0f;
        }
    }

    private void Swim()
    {
        m_isGrounded = false;

        //Setup move direction based on input and follow camera forward direction if swimming.
        Vector3 move = transform.right * GetMoveInput().x + m_camera.transform.forward * GetMoveInput().z;

        m_characterController.Move(move * Time.deltaTime * m_swimSpeed);
    }

    private void Walk()
    {
        //if your on the ground, don't increase Velocity;
        if (m_isGrounded && m_velocity.y < 0.0f)
            m_velocity.y = -3.0f;

        //Key apply movement.
        Vector3 move = transform.right * GetMoveInput().x + transform.forward * GetMoveInput().z;

        //apply movement to controller.
        m_characterController.Move(move * Time.deltaTime * m_walkSpeed);
    }

    //Same logic as walk but higher Speed and less turning speed on camera.
    void Sprint()
    {
        //if your on the ground, don't increase Velocity;
        if (m_isGrounded && m_velocity.y < 0.0f)
            m_velocity.y = -3.0f;

        //Key apply movement.
        Vector3 move = transform.right * GetMoveInput().x + transform.forward * GetMoveInput().z;

        //apply movement to controller.
        m_characterController.Move(move * Time.deltaTime * m_sprintSpeed);
    }

    private bool UpdateIsGrounded()
    {
        m_isGrounded = Physics.CheckSphere(m_groundCheck.position, m_groundDistance, m_groundMask);
        return m_isGrounded;
    }

    private void UpdateCamera()
    {
        m_camera.UpdateRotation(GetLookInput().x, GetLookInput().y);

        //get only forward and ignore Y. 
        //This is so we can move flat on the ground without rotating character model on xRot.
        Vector3 targetDirection = m_camera.transform.forward;
        targetDirection.y = 0;

        m_player.transform.rotation = Quaternion.LookRotation(targetDirection);
    }

    Vector3 GetMoveInput()
    {
        return ALInput.GetDirection(ALInput.DirectionCode.MoveInput);
    }

    public Vector3 GetLookInput()
    {
        return ALInput.GetDirection(ALInput.DirectionCode.LookInput);
    }

    void ApplyGravity()
    {
        m_velocity.y += m_gravity * Time.deltaTime;
        m_characterController.Move(m_velocity * Time.deltaTime);
    }

    public bool IsJumping()
    {
        return ALInput.GetKey(ALInput.Jump);
        //return Input.GetButton("Jump");
    }

    public bool IsPunching()
    {
        return false;
    }

    public bool IsSprinting()
    {
        return ALInput.GetKey(ALInput.Sprint);
        return Input.GetButton("Sprint");
    }
}
