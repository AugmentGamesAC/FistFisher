public interface IPercentage
{
    float Current { get; }
    float Max { get; }
    float Percent { get; }

    void IncrementCurrent(float increment);
    void SetCurrent(float current);
    void SetMax(float max);
}