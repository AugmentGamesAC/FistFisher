using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// movement handler for when player is mounted to boat
/// </summary>
public class BoatMovement : MonoBehaviour
{
    public Vector3 m_centerOfMass;
    public float m_collisionRange = 2.5f;
    public float m_speed = 1.0f;
    public float m_steerSpeed = 10.0f;

    public float m_movementThreshold = 1.0f;
    public float m_steerThreshold = 1.0f;

    public bool m_allowUpdate = false;

    Transform m_COM;
    public Transform m_boatFront;

    float m_verticalInput;
    float m_horizontalInput;

    float m_movementFactor;
    float m_steerFactor;

    [SerializeField]
    public Transform spawnPoint;


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
        //ALInput.GetDirection(ALInput.DirectionCode.MoveInput).y;

        m_movementFactor = Mathf.Lerp(m_movementFactor, m_verticalInput, Time.deltaTime / m_movementThreshold);
        RaycastHit hit;
        Physics.Raycast(m_boatFront.position, Vector3.forward, out hit, m_collisionRange);
        if (hit.collider == null || hit.collider.isTrigger)
        {
            transform.Translate(0.0f, 0.0f, m_movementFactor * m_speed);
        }
        else
        {
            //Debug.LogError(hit.collider.gameObject.name);
            transform.Translate(0.0f, 0.0f, -m_movementFactor * m_speed);
        }
    }


    public void MountObject(GameObject someObject)
    {


        someObject.transform.SetParent(transform);
        someObject.transform.localPosition = spawnPoint.localPosition;
        someObject.transform.rotation = transform.rotation;
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
