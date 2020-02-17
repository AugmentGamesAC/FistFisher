using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisapearingMenu : Menu
{
    [SerializeField]
    protected float m_timer;
    [SerializeField]
    protected float m_displayDuration;

    public void WasKeyed ()
    {
        if (!gameObject.activeSelf)
            Show(true);
        m_timer = m_displayDuration;   
    }

    public void FixedUpdate()
    {
        if (m_timer <= 0)
            return;
        m_timer -= Time.deltaTime;
        if (m_timer <= 0)
            Show(false);
    }
}
