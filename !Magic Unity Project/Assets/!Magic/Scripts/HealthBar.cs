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

    private float m_LostHealthBarInterpolator = 0.0f;

    public float m_LostHealthLerpSpeed;

    private Valve.VR.InteractionSystem.Player m_Player;

    // Start is called before the first frame update
    void Start()
    {
        //find the Lookat target with tags.
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Valve.VR.InteractionSystem.Player>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(DelayedLerp());
        //Canvas always looks towards player.
        if (m_Player)
            transform.LookAt(m_Player.hmdTransform.position);
    }

    public void OnHealthChange()
    {
        //update components for lerping.
        //Set Local health percentage before setting the new image fill amount.
        m_InitialHealthPercentage = m_HealthImage.fillAmount;
        //Damage health visually.
        m_HealthImage.fillAmount = GetComponentInParent<ASpellUser>().ShieldPercentage;
        //reset interpolator
        m_LostHealthBarInterpolator = 0.0f;
    }

    public void OnManaChange()
    {
        m_ManaImage.fillAmount = GetComponentInParent<ASpellUser>().ManaPercentage;
    }

    //Make Orange healthbar catch up to the current health
    private void LerpLostHealthBar()
    {
        //make lost health bar follow health bar but slower.
        m_LostHealthBarInterpolator += 0.5f * Time.deltaTime * m_LostHealthLerpSpeed;
        m_LostHealthImage.fillAmount = Mathf.Lerp(m_LostHealthImage.fillAmount, m_HealthImage.fillAmount, m_LostHealthBarInterpolator);
    }

    private IEnumerator DelayedLerp()
    {
        yield return new WaitForSeconds(4.0f);
        //Lerp every frame.
        LerpLostHealthBar();
    }
}
