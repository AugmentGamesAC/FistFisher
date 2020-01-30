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
    [SerializeField]
    protected Image m_maskImage;
    [SerializeField]
    protected FloatTracker m_tracker;
    // Start is called before the first frame update
    void Start()
    {
        m_tracker.OnStateChange += UpdateState;
    }

    protected void UpdateState(float fillValue)
    {
        float fillAmount = (fillValue - m_minFillAmount) / (m_maxFillAmount - m_minFillAmount);
        m_maskImage.fillAmount = fillAmount;
    }

}
