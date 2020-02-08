public interface ISingleSelectionList<T>
{
    T SelectedItem { get; }
    int Selection { get; }
    int Count { get; }
    void SetSelection(int direction);
    void IncrementSelection(int direction);
    void AddItem(T item);
    void Remove(T item);
}