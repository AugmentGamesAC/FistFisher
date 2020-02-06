using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds all the information needed for a player's move.
/// </summary>
public class CombatMoveInfo
{
    public CombatMoveInfo(float damage, float slow, float noise, float moveDistance, float oxygenConsumption)
    {
        m_damage = damage;
        m_slow = slow;
        m_noise = noise;
        m_moveDistance = moveDistance;
        m_oxygenConsumption = oxygenConsumption;
    }

    public CombatMoveInfo() { }

    public float m_damage;
    public float m_slow;
    public float m_noise;
    public float m_moveDistance;
    public float m_oxygenConsumption;

    /// <summary>
    /// probably RangeZone enum instead of float.
    /// </summary>
    public float m_sweetSpot;
}
