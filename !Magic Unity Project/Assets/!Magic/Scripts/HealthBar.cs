using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image m_HealthImage;
    public Image m_LostHealthImage;
    public Image m_ManaImage;

    private float m_InitialHealthPercentage;
    private float m_EndHealthPercentage;
    private float m_PercentModified;

    private bool m_IsHealthLerpCalculated = false;
    private bool m_IsManaLerpCalculated = false;

    public float m_HealthBarInterpolator = 0.0f;
    public float m_LostHealthBarInterpolator = 0.0f;

    public float m_HealthLerpSpeed;
    public float m_LostHealthLerpSpeed;

    private Valve.VR.InteractionSystem.Player m_Player;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Valve.VR.InteractionSystem.Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_HealthImage.fillAmount == m_HealthBarInterpolator)
        {
            m_IsHealthLerpCalculated = false;
        }
        else
        {
            LerpHealthBar();
        }
            LerpLostHealthBar();

        if(m_Player)
            transform.LookAt(m_Player.hmdTransform.position);

    }

    public void OnHealthChange()
    {
        //update components for lerping.
        m_InitialHealthPercentage = m_HealthImage.fillAmount;
        m_EndHealthPercentage = GetComponentInParent<Enemy>().m_HealthPercentage;
        m_PercentModified = m_InitialHealthPercentage - m_EndHealthPercentage;

        //reset interpolators
        m_HealthBarInterpolator = 0.0f;
        m_LostHealthBarInterpolator = 0.0f;

        m_IsHealthLerpCalculated = true;
    }

    public void OnManaChange()
    {
        m_ManaImage.fillAmount = GetComponentInParent<Enemy>().m_ManaPercentage;

        m_IsManaLerpCalculated = true;
    }

    //lerp health bar to the modified amount. 
    private void LerpHealthBar()
    {
        //Only run code if 
        if (m_IsHealthLerpCalculated == true)
        {
            m_HealthImage.fillAmount = Mathf.Lerp(m_InitialHealthPercentage, m_EndHealthPercentage, m_HealthBarInterpolator);
            m_HealthBarInterpolator += 0.5f * Time.deltaTime * m_HealthLerpSpeed;
        }
        //clamp interpolator
        if (m_HealthBarInterpolator >= 1.0f)
        {
            m_HealthBarInterpolator = 1.0f;
        }
    }

    private void LerpLostHealthBar()
    {
        //make lost health bar follow health bar but slower.
        m_LostHealthImage.fillAmount = Mathf.Lerp(m_LostHealthImage.fillAmount, m_HealthImage.fillAmount, m_LostHealthBarInterpolator);
        m_LostHealthBarInterpolator += 0.5f * Time.deltaTime * m_LostHealthLerpSpeed;

        //clamp interpolator
        if (m_LostHealthBarInterpolator >= 1.0f)
        {
            m_LostHealthBarInterpolator = 1.0f;
        }
    }
}
