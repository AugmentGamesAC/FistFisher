public interface ISlotData
{
    int Count { get; }
    int Index { get; }
    IItem Item { get; }
    SlotManager Manager { get; }

    int AddItem(IItem item, int count);
    int CheckAddItem(IItem item, int count);
    void RemoveItem();
}