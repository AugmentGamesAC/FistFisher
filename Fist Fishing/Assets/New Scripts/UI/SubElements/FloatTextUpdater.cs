using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class FloatTextUpdater : MonoBehaviour
{

    [SerializeField]
    protected FloatTracker m_tracker;


    [SerializeField]
    protected string m_textInput;
    protected Text m_text;
    
    // Start is called before the first frame update
    void Start()
    {
        m_text = GetComponent<Text>();
        m_tracker.OnStateChange += UpdateState;
    }
    
    public void UpdateTracker (FloatTracker newTracker)
    {
        m_tracker.OnStateChange -= UpdateState;
        m_tracker = newTracker;
        m_tracker.OnStateChange += UpdateState;
    }


  protected void UpdateState(float value)
    {

        m_text.text = string.Format(m_textInput + " {0}", value);
    }
}
