using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUpdater : CoreUIUpdater<FloatTracker,Image,float>
{
    [SerializeField]
    protected int m_minFillAmount;
    [SerializeField]
    protected int m_maxFillAmount;

    public void ConfigureBar(int min, int max)
    {
        m_minFillAmount = min;
        m_maxFillAmount = max;
    }

    protected override void UpdateState(float fillValue)
    {
        //Use offsets set in editor to dynamically change bar fill size
        float fillAmount = (fillValue - m_minFillAmount) / (m_maxFillAmount - m_minFillAmount);
        //Set the fill amount based on what is changed in the tracker
        m_UIElement.fillAmount = fillAmount;
    }
}
