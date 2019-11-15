using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHitBox : MonoBehaviour
{
    public KeyValuePair<Collider, float> m_HitBoxModifier { get; private set; }

    public Collider m_collider;
    public float m_percentModifier = 1.0f;

    void Start()
    {
        if (m_collider == null)
            m_collider = GetComponent<Collider>();

        m_HitBoxModifier = new KeyValuePair<Collider, float>(m_collider, m_percentModifier);
    }
}
