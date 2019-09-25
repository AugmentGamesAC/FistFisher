using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    //sine wave stats
    public float m_Magnitude = 0.3f;
    public float m_FloatOffset = 0.0f;
    public float m_Frequency = 0.4f;

    private Vector3 m_StartPos;

    // Start is called before the first frame update
    void Start()
    {
        m_StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x,
            m_StartPos.y + m_FloatOffset + Mathf.Sin(Time.time * m_Frequency) * m_Magnitude,
            transform.position.z);
    }
}