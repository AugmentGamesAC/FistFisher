using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController m_characterController;

    public Camera m_camera;

    public float m_walkSpeed = 100.0f;
    public float m_SwimSpeed = 40.0f;

    public float m_gravity = -9.81f;
    public float m_terminalVelocity = 50.0f;

    public bool m_IsSwimming = false;
    public bool m_IsGrounded = false;

    Vector3 m_velocity = Vector3.zero;

    public Transform m_groundCheck;
    public float m_groundDistance = 0.4f;
    public LayerMask m_groundMask;

    // Update is called once per frame
    void Update()
    {
        m_IsGrounded = Physics.CheckSphere(m_groundCheck.position, m_groundDistance, m_groundMask);

        if (m_IsSwimming)
            Swim();
        else
            Walk();
    }

    private void Swim()
    {
        m_IsGrounded = false;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + m_camera.transform.forward * z;

        m_characterController.Move(move * Time.deltaTime * m_SwimSpeed);
    }

    private void Walk()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //if your on the ground, don't increase Velocity;
        if (m_IsGrounded && m_velocity.y < 0.0f)
            m_velocity.y = -2.0f;

        //Key apply movement.
        Vector3 move = transform.right * x + transform.forward * z;

        //apply movement to controller.
        m_characterController.Move(move * Time.deltaTime * m_walkSpeed);

        m_velocity.y += m_gravity * Time.deltaTime;

        m_characterController.Move(m_velocity * Time.deltaTime);
    }


}
