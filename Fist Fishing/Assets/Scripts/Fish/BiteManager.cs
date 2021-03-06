﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// determines what is bitable and how much damage bites do
/// allows hostile fish to attack in world
/// replaced with new combat system
/// </summary>
public class BiteManager : MonoBehaviour
{
    [SerializeField]
    protected float m_biteDamage = 20;
    public float BiteDamage { get { return m_biteDamage; } }

    [SerializeField]
    protected float m_timeBetweenBites = 1.5f;
    public float TimeBeteenBites { get { return m_timeBetweenBites; } }

    [SerializeField]
    protected int m_Bitables = 0;

    protected HashSet<Collider> m_inMouth = new HashSet<Collider>();
    public HashSet<Collider> InMouth { get { return m_inMouth; } }

    private void OnTriggerEnter(Collider other)
    {
        if (m_inMouth.Contains(other))
            return;
        m_inMouth.Add(other);
        m_Bitables++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_inMouth.Contains(other))
            m_inMouth.Remove(other);
        m_Bitables--;
    }

}
