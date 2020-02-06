using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class TextUpdater : MonoBehaviour
{
    [SerializeField]
    protected TextTracker m_tracker;
    protected Text m_text;

    // Start is called before the first frame update
    void Start()
    {
        m_text = GetComponent<Text>();
        m_tracker.OnStateChange += UpdateState;
    }
    public void UpdateTracker(TextTracker newTracker)
    {
        m_tracker.OnStateChange -= UpdateState;
        m_tracker = newTracker;
        m_tracker.OnStateChange += UpdateState;
    }

    protected void UpdateState(string value)
    {

        m_text.text = string.Format(value);
    }
}
