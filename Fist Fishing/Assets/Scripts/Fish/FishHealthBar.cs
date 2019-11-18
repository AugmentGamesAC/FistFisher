using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHealthBar : MonoBehaviour
{
    public GameObject m_player;

    //if health percentage == 1, isActive = false after a float m_delay.

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.LookAt(m_player.transform);
    }
}
