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

        if (m_healthModule.HealthPercentage == 1.0f)
        {
            m_timer += Time.deltaTime;
            if (m_deactivateDelay < m_timer)
            {
                HUD.gameObject.SetActive(false);
                m_timer = 0.0f;
            }
        }
        else
        {
            m_timer = 0.0f;
            HUD.gameObject.SetActive(true);
        }

        this.gameObject.transform.LookAt(Camera.main.transform);
    }
}
