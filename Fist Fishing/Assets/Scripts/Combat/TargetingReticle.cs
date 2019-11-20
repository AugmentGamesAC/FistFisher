using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingReticle : MonoBehaviour
{
    private Camera m_camera;

    public float m_rotateSpeed = 3.0f;

    private Vector3 m_zRotate = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        m_camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - m_camera.transform.position);

        m_zRotate.z += m_rotateSpeed;

        transform.Rotate(m_zRotate);
    }
}
