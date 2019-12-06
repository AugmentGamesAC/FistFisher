using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController m_characterController;
    public float m_turnSpeed = 25;
    public GameObject m_player;
    public GameObject m_playerBody;
    public GameObject m_boat;
    public Vector3 m_boatMountPosition;
    public Vector3 m_boatDismountPosition;

    private ThirdPersonCamera m_camera;
    public DisplayInventory m_displayInventory;

    public float m_walkSpeed = 10.0f;
    public float m_sprintSpeed = 15.0f;
    public float m_fastSwimSpeed = 8.0f;
    public float m_swimSpeed = 5.0f;

    public float m_gravity = -9.81f;
    public float m_terminalVelocity = 50.0f;

    public float m_mountCooldownMax = 2.0f;
    public float m_mountCooldown = 0.0f;

    public float m_baitThrowCooldownMax = 2.0f;
    public float m_baitThrowCooldown = 0.0f;

    public bool m_isSwimming = false;
    public bool m_isGrounded = false;
    public bool m_isMounted = false;
    public bool m_canMount = false;

    Vector3 m_velocity = Vector3.zero;

    public Transform m_groundCheck;
    public float m_groundDistance = 0.4f;
    public LayerMask m_groundMask;

    public BoatMovement m_boatMovement;

    private void Start()
    {
        m_camera = Camera.main.GetComponent<ThirdPersonCamera>();

        m_displayInventory = GetComponentInChildren<DisplayInventory>();

        Cursor.lockState = CursorLockMode.Locked;
        //m_displayInventory.gameObject.transform.parent.gameObject.SetActive(false);

        if (m_player == null)
            m_player = gameObject;

        m_camera.SetPlayer(m_player);

        m_boat = GameObject.FindGameObjectWithTag("Boat");
        if(m_boat!=null)
            m_boatMovement = m_boat.GetComponent<BoatMovement>();

        m_baitThrowCooldown = m_baitThrowCooldownMax;
    }

    void ResolveMovement()
    {
        if (m_boatMovement != null)
            m_boatMovement.m_allowUpdate = m_isMounted;

        if (m_isMounted)
            return;

            if (m_isSwimming)
            {
                Swim();
                return;
            }
            else
            {
                ApplyGravity();
                Walk();
            }
    }


    // Update is called once per frame
    void Update()
    {
        UpdateIsGrounded();

        UpdateCamera();

        ResolveMovement();

        m_mountCooldown -= Time.deltaTime;
        m_baitThrowCooldown -= Time.deltaTime;
        m_baitThrowCooldown = Mathf.Clamp(m_baitThrowCooldown, 0.0f, m_baitThrowCooldownMax);

        if (m_mountCooldown <= 0.0f)
            UpdateBoatMountStatus();


        if (m_baitThrowCooldown <= 0.0f && !m_isMounted && IsThrowBait())
        {
            GameObject bait = gameObject.GetComponent<Inventory>().GetReferenceToStoredBait();
            if (bait != null)
            {
                if (gameObject.GetComponent<Inventory>().RemoveFromInventory(bait))
                {
                    bait.GetComponent<Bait>().Init();
                    bait.transform.position = gameObject.transform.position + gameObject.transform.forward * 5.0f;

                    m_baitThrowCooldown = m_baitThrowCooldownMax;
                }
            }
        }

        

        /*if (ALInput.GetKeyDown(ALInput.ToggleInventory))
        {
            ToggleMouseLock();
        }*/
    }

    public void ToggleMouseLock()
    {
        bool setToNone = Cursor.lockState == CursorLockMode.Locked;

        m_displayInventory.gameObject.SetActive(setToNone);
        Cursor.lockState = (setToNone) ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void DriveBoat()
    {
        //if your on the ground, don't increase Velocity;
        if (m_isGrounded && m_velocity.y < 0.0f)
            m_velocity.y = -1.0f;

        //Key apply movement.
        Vector3 move = transform.right * GetMoveInput().x + transform.forward * GetMoveInput().z;
        move.y = 0;
        //apply movement to controller.
        m_characterController.Move(move * Time.deltaTime * m_walkSpeed);
    }

    private void Mount()
    {
        //teleport to boat seat.
        transform.position = m_boatMountPosition;

        m_player.GetComponent<Player>().SetNewCheckpoint(transform);

        //player is now mounted and shouldn't be able to move until dismount.
        m_isMounted = true;

        m_canMount = false;


        transform.forward = m_boat.transform.forward;
        transform.SetParent(m_boat.transform);
    }

    private void Dismount()
    {
        transform.SetParent(null);

        //go to diving position
        transform.position = m_boatDismountPosition;

        //no longer mounted on the boat.
        m_isMounted = false;
        m_canMount = true;
    }

    private void UpdateBoatMountStatus()
    {
        //if pressing mount button and allowed to mount.
        if (m_canMount && ALInput.GetKeyDown(ALInput.MountBoat) && !m_isMounted)
        {
            Mount();

            m_mountCooldown = m_mountCooldownMax;
        }
        //if i am mounted and pressing the dismount button.
        else if (!m_canMount && ALInput.GetKeyDown(ALInput.DismountBoat) && m_isMounted)
        {
            Dismount();

            m_mountCooldown = m_mountCooldownMax;
        }
    }

    private void Swim()
    {
        m_isGrounded = false;

        if (!ALInput.GetKey(ALInput.ManualCamera))
            ResolveSwimRotation();

        if (ALInput.GetKey(ALInput.Forward))
            m_characterController.Move(transform.up * Time.deltaTime * m_swimSpeed);
    }

    void ResolveSwimRotation()
    {
        //Vector3 desiredDirection = (
        //    transform.right * ALInput.GetAxis(ALInput.AxisCode.MouseY)
        //    + transform.forward * ALInput.GetAxis(ALInput.AxisCode.MouseY)
        //) * m_turnSpeed * Time.deltaTime;
        Vector3 desiredDirection = new Vector3
        (
            (ALInput.GetKey(ALInput.RotateForward)) ? 1 : 0 +
            ((ALInput.GetKey(ALInput.RotateBackwards)) ? -1 : 0),
            0, // no touch Y
            (ALInput.GetKey(ALInput.RotateLeft)) ? 1 : 0 +
            ((ALInput.GetKey(ALInput.RotateRight)) ? -1 : 0)
        ) * m_turnSpeed * Time.deltaTime;

        if (desiredDirection.sqrMagnitude > 0.000001)
            transform.Rotate(desiredDirection, Space.Self);
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


    //This is used for ascending when in the water.
    //Can be removed if it doesn't fit design
    void Jump()
    {
        //Assign ascend speed.

        Vector3 speed = transform.up * 10;

        //apply movement to controller.
        m_characterController.Move(speed * Time.deltaTime);

    }

    //For descending when in the water.
    void Descend()
    {
        //Assign descend speed.
        Vector3 speed = -transform.up * 10;

        //apply movement to controller.
        m_characterController.Move(speed * Time.deltaTime);
    }

    //Same logic as walk but higher Speed and less turning speed on camera.
    void Sprint()
    {
        //if your on the ground, don't increase Velocity;
        if (m_isGrounded && m_velocity.y < 0.0f)
            m_velocity.y = -3.0f;

        if (m_isGrounded)
        {
            //Key apply movement.
            Vector3 move = transform.right * GetMoveInput().x + transform.forward * GetMoveInput().z;

            //apply movement to controller.
            m_characterController.Move(move * Time.deltaTime * m_sprintSpeed);
        }
        //If in the water sprint is locked to forward and backwards movement
        //based on the cameras forward direction
        else if (m_isSwimming)
        {
            //Key apply movement.
            Vector3 move = m_camera.transform.forward * GetMoveInput().z;

            //apply movement to controller.
            m_characterController.Move(move * Time.deltaTime * m_sprintSpeed);
        }
    }

    private bool UpdateIsGrounded()
    {
        m_isGrounded = Physics.CheckSphere(m_groundCheck.position, m_groundDistance, m_groundMask);
        return m_isGrounded;
    }

    private void UpdateCamera()
    {
        if (!ALInput.GetKey(ALInput.ManualCamera))
            return;

        m_camera.UpdateRotation(GetLookInput().x, GetLookInput().y);

        //get only forward and ignore Y. 
        //This is so we can move flat on the ground without rotating character model on xRot.
        Vector3 targetDirection = m_camera.transform.forward;
        targetDirection.y = 0;

        //m_player.transform.rotation = Quaternion.LookRotation(targetDirection);
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
    public bool IsDescending()
    {
        return ALInput.GetKey(ALInput.Descend);
        //return Input.GetButton("Descend");
    }
    public bool IsPunching()
    {
        return false;
    }

    public bool IsSprinting()
    {
        return ALInput.GetKey(ALInput.Sprint);
        //return Input.GetButton("Sprint");
    }
    public bool IsThrowBait()
    {
        return ALInput.GetKey(ALInput.ThrowBait);
    }
}
