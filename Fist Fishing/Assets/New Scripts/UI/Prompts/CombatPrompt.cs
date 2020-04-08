using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// specific prompt indicating combat possibility
/// </summary>
[System.Serializable]
public class CombatPrompt : Prompt
{
    protected float m_detectionRadius;

    public override void Init(Sprite sprite, string desc, int priority = 1)
    {
        m_detectionRadius = PlayerInstance.Instance.PlayerMotion.SphereCastRadius;

        if (m_detectionRadius <= 0)
            throw new System.InvalidOperationException( "Radius must be > 0");

        gameObject.AddComponent<SphereCollider>().radius = m_detectionRadius;

        base.Init(sprite, desc, priority);
    }
}
