
/// <summary>
///interface for slots and their data storage and handling
/// </summary>
public interface ISlotData
{
    int Count { get; }
    int Index { get; }
    IItem Item { get; }
    SlotManager Manager { get; }
    void SetSlotManager(SlotManager newManger);
    void SetIndex(int newIndex);
    int AddItem(IItem item, int count);
    int CheckAddItem(IItem item, int count);
    void RemoveItem();
}