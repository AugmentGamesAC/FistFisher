using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : ASpellUser
{
    Player m_playerRef;
    Vector3 m_playerPos;
    float m_playerHealth;
    float m_enemyHealth;

    void Awake()
    {
        m_playerRef = GameObject.FindGameObjectWithTag("Player");
        m_playerPos = m_playerRef.transform.position;
        m_playerHealth = m_playerRef.HealthCurrent.Get();
    }

    public override Transform Aiming => throw new System.NotImplementedException();
}
