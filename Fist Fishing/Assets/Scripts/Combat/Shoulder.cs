﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// when combat was going to be in-world, this was going to be a way to show attack ranges for the player
/// </summary>
public class Shoulder : MonoBehaviour
{
    public float m_smallZoneRange = 5.0f;
    public float m_smallZoneModifier = 1.0f;

    public float m_mediumZoneRange = 10.0f;
    public float m_mediumZoneModifier = 0.8f;

    public float m_largeZoneRange = 15.0f;
    public float m_largeZoneModifier = 0.5f;

    public CombatZone m_smallZone;
    public CombatZone m_mediumZone;
    public CombatZone m_largeZone;

    public CombatZone m_currentCombatZone;

    private void Start()
    {
        m_smallZone = new CombatZone();
        m_mediumZone = new CombatZone();
        m_largeZone = new CombatZone();

        m_smallZone.Range = m_smallZoneRange;
        m_smallZone.DamageModifier = m_smallZoneModifier;

        m_mediumZone.Range = m_mediumZoneRange;
        m_mediumZone.DamageModifier = m_mediumZoneModifier;

        m_largeZone.Range = m_largeZoneRange;
        m_largeZone.DamageModifier = m_largeZoneModifier;
    }

    //tests every frame against targeted fish and returns which zone it's in.
    public CombatZone GetCombatZone(GameObject targetedFish)
    {
        Vector3 fishPos = targetedFish.transform.position;

        float distFromFish = Vector3.Distance(transform.position, fishPos);

        //if not in punching range, can't punch.
        if (distFromFish > m_largeZone.Range)
            return null;

        //prioritize small zones.
        if (distFromFish < m_smallZone.Range)
        {
            m_currentCombatZone = m_smallZone;
            return m_currentCombatZone;
        }
        else if (distFromFish < m_mediumZone.Range)
        {
            m_currentCombatZone = m_mediumZone;
            return m_currentCombatZone;
        }
        else if (distFromFish <= m_largeZone.Range)
        {
            m_currentCombatZone = m_largeZone;
            return m_currentCombatZone;
        }

        return null;
    }
}
