using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float m_Damage = -33f;
    private float m_Life = 0.5f;

    float cooldown = 2.0f;

    List<float> m_cooldowns;
    List<GameObject> m_gameObjectsOnCooldown;

    Dictionary<GameObject, float> m_CooldownList;


    private void Start()
    {
        m_CooldownList = new Dictionary<GameObject, float>();
    }

    private void FixedUpdate()
    {
        m_Life -= Time.deltaTime;
        if (m_Life <= 0f)
            GameObject.Destroy(gameObject);

        List<GameObject> removalQueue = new List<GameObject>();
        List<GameObject> keys = new List<GameObject>(m_CooldownList.Keys);

        foreach(GameObject key in keys)
        {
            m_CooldownList[key] -= Time.deltaTime;
        }

        foreach (GameObject entry in m_CooldownList.Keys)
        {
            if (m_CooldownList[entry] <= 0)
            {
                removalQueue.Add(entry);
            }
        }

        foreach(GameObject entry in removalQueue)
        {
            m_CooldownList.Remove(entry);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        ASpellUser otherSpellUser = (ASpellUser)other.gameObject.GetComponent(typeof(ASpellUser));

        if (otherSpellUser == null)
            return;

        GameObject otherEnemy = other.gameObject;

        if (!m_CooldownList.ContainsKey(otherEnemy))
        {
            otherSpellUser.TakeDamage(m_Damage);
            if(otherSpellUser.ShieldCurrent > 0)
                m_CooldownList.Add(otherEnemy, cooldown);
            return;
        }
    }
}
