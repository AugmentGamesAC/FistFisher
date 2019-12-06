using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    public Vector3 m_centerOfMass;

    public float m_speed = 1.0f;
    public float m_steerSpeed = 10.0f;

    public float m_movementThreshold = 1.0f;
    public float m_steerThreshold = 1.0f;

    public bool m_allowUpdate = false;

    Transform m_COM;
    public Rigidbody m_BoatBody;

    float m_verticalInput;
    float m_horizontalInput;

    float m_movementFactor;
    float m_steerFactor;

    private void Update()
    {
        if (m_allowUpdate)
        {
            //Balance();
            Movement();
            Steer();
        }
    }

    void Balance()
    {
        if (!m_COM)
        {
            m_COM = new GameObject("COM").transform;
            m_COM.SetParent(transform);
        }

        m_COM.position = m_centerOfMass;
        GetComponent<Rigidbody>().centerOfMass = m_COM.position;
    }
    void Movement()
    {
        m_verticalInput = Input.GetAxis("Vertical");
        //Debug.LogWarning(m_verticalInput + "-in1");
        //ALInput.GetDirection(ALInput.DirectionCode.MoveInput).y;
        m_movementFactor = Mathf.Lerp(m_movementFactor, m_verticalInput, Time.deltaTime / m_movementThreshold);
        //Debug.LogError(m_movementFactor + "-mf");
        if (m_BoatBody == null)
            transform.Translate(0.0f, 0.0f, m_movementFactor * m_speed);
        else
        {
            Vector3 dir = gameObject.transform.forward * m_movementFactor * m_speed;
            m_BoatBody.AddForce(dir, ForceMode.VelocityChange);
        }
        
    }

    void Steer()
    {
        //mouse x
        m_horizontalInput = Input.GetAxis("Horizontal");
        //ALInput.GetDirection(ALInput.DirectionCode.LookInput).x;
        m_steerFactor = Mathf.Lerp(m_steerFactor, m_horizontalInput, Time.deltaTime / m_movementThreshold);
        transform.Rotate(0.0f, m_steerFactor * m_steerSpeed, 0.0f);
    }
};
