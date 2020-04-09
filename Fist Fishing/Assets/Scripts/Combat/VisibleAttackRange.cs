using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// when combat was going to be in-world, this was going to be a way to show attack ranges for the player
/// </summary>
public class VisibleAttackRange : MonoBehaviour
{
    public Shoulder m_currentShoulder;

    public GameObject m_player;

    public bool Toggle = false;

    private void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (m_player == null)
            return;
        m_currentShoulder = m_player.GetComponentInChildren<CombatModule>().m_currentShoulder;
    }
    private void OnDrawGizmos()
    {
        if (m_currentShoulder == null || !Toggle)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(m_currentShoulder.gameObject.transform.position, m_currentShoulder.m_currentCombatZone.Range);
    }
}
