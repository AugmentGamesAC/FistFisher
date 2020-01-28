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
    protected string m_textChange;
    protected Text m_text;
    
    // Start is called before the first frame update
    void Start()
    {
        m_text = GetComponent<Text>();
        m_tracker.OnStateChange += UpdateState;
    }

  protected void UpdateState(float value)
    {

        m_text.text = string.Format(m_textChange, value);
    }
}
