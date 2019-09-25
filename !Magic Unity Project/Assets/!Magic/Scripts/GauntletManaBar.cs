using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GauntletManaBar : MonoBehaviour
{

    public GameObject m_manaBar;
    public GameObject m_manaBarLoss;

    private float m_initialManaPercentage;
    private float m_endManaPercentage;
    private float m_percentModified;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*PlayerData m_magicUser = gameObject.transform.parent.GetComponent<GauntletBase>().m_magicUser;
        if (m_magicUser != null)
        {
            if(!m_magicUser.m_predictingManaUsage)
            {
                OnConfirmManaChange();
            }

        }*/
    }



    public void OnManaChange()
    {
        m_initialManaPercentage = m_manaBar.transform.localScale.y;

        PlayerData m_magicUser = gameObject.transform.parent.GetComponent<GauntletBase>().m_magicUser;
        if (m_magicUser != null)
        {
            m_endManaPercentage = m_magicUser.m_mana / m_magicUser.m_manaMax;

            m_percentModified = m_initialManaPercentage - m_endManaPercentage;

        }

        m_manaBar.transform.localScale -= new Vector3(0.0f, m_percentModified, 0.0f);
        m_manaBar.transform.localPosition += new Vector3(0.0f, (m_percentModified / 100.0f)*1.5f, 0.0f);

    }

    public void OnConfirmedManaChange()
    {
        m_initialManaPercentage = m_manaBarLoss.transform.localScale.y;

        PlayerData m_magicUser = gameObject.transform.parent.GetComponent<GauntletBase>().m_magicUser;
        if (m_magicUser != null)
        {
            //m_endManaPercentage = m_magicUser.m_actualMana / m_magicUser.m_manaMax;

            //m_percentModified = m_initialManaPercentage - m_endManaPercentage;
            m_manaBarLoss.transform.localScale -= new Vector3(m_manaBarLoss.transform.localScale.x, m_manaBar.transform.localScale.y, m_manaBarLoss.transform.localScale.z);
            m_manaBarLoss.transform.localPosition = new Vector3(m_manaBarLoss.transform.localScale.x, m_manaBar.transform.localScale.y, m_manaBarLoss.transform.localScale.z);

        }

        //m_manaBarLoss.transform.localScale -= new Vector3(0.0f, m_percentModified, 0.0f);
        //m_manaBarLoss.transform.localPosition += new Vector3(0.0f, (m_percentModified / 100.0f) * 1.5f, 0.0f);

    }

    //once the spell has been confirmed to be cast, set the loss bar to
   /* public void OnConfirmManaChange()
    {
        m_manaBarLoss.transform.localScale -= new Vector3(m_manaBarLoss.transform.localScale.x, m_manaBar.transform.localScale.y, m_manaBarLoss.transform.localScale.z);
        m_manaBarLoss.transform.localPosition = new Vector3(m_manaBarLoss.transform.localScale.x, m_manaBar.transform.localScale.y, m_manaBarLoss.transform.localScale.z);
        
    }*/

}
