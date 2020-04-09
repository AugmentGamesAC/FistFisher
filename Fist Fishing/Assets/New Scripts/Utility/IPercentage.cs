/// <summary>
/// interface for all things percentagy
/// </summary>
public interface IPercentage
{
    float Current { get; }
    StatTracker Max { get; }
    float Percent { get; }

    void IncrementCurrent(float increment);
    void SetCurrent(float current);
    void SetMax(StatTracker max);
    void SetMax(float max);
}