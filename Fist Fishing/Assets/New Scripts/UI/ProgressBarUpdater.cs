using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUpdater : MonoBehaviour
{
    
    [SerializeField]
    protected int m_minFillAmount;
    [SerializeField]
    protected int m_maxFillAmount;
    protected Image m_fillImage;

    [SerializeField]
    protected FloatTracker m_tracker;
   
    // Start is called before the first frame update
    void Start()
    {
        //Get image with fill amount
        m_fillImage = transform.GetChild(0).GetComponent<Image>();
        //On a change occuring use UpdateState
        m_tracker.OnStateChange += UpdateState;
    }

    protected void UpdateState(float fillValue)
    {
        //Use offsets set in editor to dynamically change bar fill size
        float fillAmount = (fillValue - m_minFillAmount) / (m_maxFillAmount - m_minFillAmount);
        //Set the fill amount based on what is changed in the tracker
        m_fillImage.fillAmount = fillAmount;
    }

}
