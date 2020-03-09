using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatPrompt : Prompt
{
    protected float m_detectionRadius;
    private void Start()
    {
        m_detectionRadius = PlayerInstance.Instance.PlayerMotion.SphereCastRadius;

        m_collider = gameObject.AddComponent<SphereCollider>();

        SphereCollider collider = (m_collider as SphereCollider);
        if (collider == default)
            return;

        collider.isTrigger = true;

        if (m_detectionRadius <= 0)
            return;
        collider.radius = m_detectionRadius;
    }
}
