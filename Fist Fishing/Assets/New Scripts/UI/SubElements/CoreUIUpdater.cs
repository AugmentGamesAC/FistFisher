using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Core functionality for changing a UIElement based on a UITracker<TDataType>.
/// 
/// </summary>
/// <typeparam name="TTracker">where TTracker :UITracker<TDataType> so TDataType and TTracker<> will match  and wil be set to m_tracker to use </typeparam>
/// <typeparam name="TUIField">Any UI component, custom or otherwise, will be set to m_UIElement to use</typeparam>
/// <typeparam name="TDataType"> the actual data object that this updater observes/used in it's UpdateState function</typeparam>
[System.Serializable]
public abstract class CoreUIUpdater<TTracker, TUIField, TDataType> : MonoBehaviour where TTracker :UITracker<TDataType> 
{
    [SerializeField]
    protected TTracker m_tracker;
    public TTracker Tracker => m_tracker;
    [SerializeField]
    protected TUIField m_UIElement;

    // Start is called before the first frame update
    public void Awake()
    {
        m_UIElement = GetComponent<TUIField>();
        if (m_tracker != default)
            m_tracker.OnStateChange += UpdateState;
    }



    public void UpdateTracker(TTracker newTracker)
    {
        if (m_tracker != default)
            m_tracker.OnStateChange -= UpdateState;
        m_tracker = newTracker;
        if (m_tracker == default)
            return;
        m_tracker.OnStateChange += UpdateState;
        ForceUpdate(m_tracker);
    }
    protected abstract void UpdateState(TDataType value);

    public void ForceUpdate(TDataType value)
    {
        UpdateState(value);
    }
}

