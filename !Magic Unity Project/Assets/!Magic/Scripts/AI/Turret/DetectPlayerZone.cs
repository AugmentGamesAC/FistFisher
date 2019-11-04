using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerZone : MonoBehaviour
{
    GameObject m_turret;
    // Start is called before the first frame update
    void Start()
    {
        if (m_turret == null)
            m_turret = transform.parent.gameObject;
    }

    public void OnTriggerStay(Collider other)
    {
        GameObject otherObj = other.gameObject;
        if (otherObj.tag == "Player Target")
        {
            m_turret.GetComponent<ABehaviour>().m_playerInZone = true;
        }
    }
}
