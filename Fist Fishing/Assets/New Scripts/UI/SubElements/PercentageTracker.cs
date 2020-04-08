using UnityEngine;

/// <summary>
/// tracker for things with percentages
/// </summary>
[System.Serializable]
public class PercentageTracker : UITracker<IPercentage>, IPercentage
{
    public PercentageTracker(StatTracker max)
    {
        displayPercentage = new Percentage();
        m_value = displayPercentage;
        m_value.SetMax(max);
        m_value.Max.OnChange += UpdateState;
    }

    [SerializeField]
    protected Percentage displayPercentage;
    public float Current => m_value.Current;

    public StatTracker Max => m_value.Max;

    public float Percent => m_value.Percent;

    public void IncrementCurrent(float increment)
    {
        m_value.IncrementCurrent(increment);
        UpdateState();
    }

    public void SetCurrent(float current)
    {
        m_value.SetCurrent(current);
        UpdateState();
    }
    public void SetMax(StatTracker stat)
    {
        m_value.SetMax(stat);
        UpdateState();
    }


    public void SetMax(float max)
    {
        m_value.SetMax(max);
        UpdateState();
    }

    protected override IPercentage ImplicitOverRide(UITracker<IPercentage> reference)
    {
        return this;
    }
}

