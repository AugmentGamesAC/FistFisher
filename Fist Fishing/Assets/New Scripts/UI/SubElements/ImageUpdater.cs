using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageUpdater : MonoBehaviour
{

    [SerializeField]
    protected ImageTracker m_tracker;
    
    protected Image m_image;
    
    // Start is called before the first frame update
    void Start()
    {
        m_image = GetComponentInChildren<UnityEngine.UI.Image>();
        m_tracker.OnStateChange += UpdateState;
    }

    public void UpdateTracker(ImageTracker newTracker)
    {
        m_tracker.OnStateChange -= UpdateState;
        m_tracker = newTracker;
        m_tracker.OnStateChange += UpdateState;
    }

    protected void UpdateState(Sprite value)
    {
        if (m_image == default)
            return;
        
        m_image.enabled = (value != default);
        if (m_image.enabled)
            m_image.sprite = value;
    }
}
