using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float m_mouseSensitivity = 120.0f;

    public Transform m_playerBody;

    private float m_xRotation = 0.0f;
    private float m_yRotation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Get Inputs from mouse.
        float mouseX = Input.GetAxis("Mouse X") * m_mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * m_mouseSensitivity * Time.deltaTime;

        //Set a xRotation variable to Clamp 
        m_xRotation -= mouseY;
        m_xRotation = Mathf.Clamp(m_xRotation, -90, 90);

        //set our rotations.
        transform.localRotation = Quaternion.Euler(m_xRotation, 0.0f, 0.0f);
        m_playerBody.Rotate(Vector3.up * mouseX);
    }
}
