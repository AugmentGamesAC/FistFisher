using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float m_Damage = 33f;
    private float m_Life = 0.5f;

    private void FixedUpdate()
    {
        m_Life -= Time.deltaTime;
        if (m_Life <= 0f)
            GameObject.Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        IMagicUser otherMagicUser = (IMagicUser)other.gameObject.GetComponent(typeof(IMagicUser));

        if (otherMagicUser == null)
            return;

        otherMagicUser.TakeDamage(m_Damage);
    }
}
