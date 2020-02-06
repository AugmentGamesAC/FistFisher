 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHealthBar : MonoBehaviour
{
    private HealthModule m_healthModule;

    public Canvas HUD;

    public float m_deactivateDelay = 3.0f;

    private float m_timer = 0.0f;

    //if health percentage == 1, isActive = false after a float m_delay.
    //Update is called once per frame.

    private void Awake()
    {
        m_healthModule = GetComponent<HealthModule>();
        
    }
    void Update()
    {
        if (HUD == null)
            return;
        if (m_healthModule == null)
            return;

        HUD.gameObject.transform.LookAt(Camera.main.transform);

        if (ResolveLowHealth())
            return;

        m_timer -= Time.deltaTime;

        if (m_timer < 0)
            HUD.gameObject.SetActive(false);
    }

    protected bool ResolveLowHealth()
    {
        if (m_healthModule.HealthPercentage == 1.0f)
            return false;

        if (m_timer != m_deactivateDelay)
        {
            m_timer = m_deactivateDelay;
            HUD.gameObject.SetActive(true);
        }
        return true;
    }
}
