using System.Collections.Generic;

public interface ISingleSelectionList<T> : System.Collections.Generic.IEnumerable<T>
{
    T this[int value] { get; }
    T SelectedItem { get; }
    int Selection { get; }
    int Count { get; }
    void SetSelection(int direction);
    void IncrementSelection(int direction);
    void AddItem(T item);
    void AddItems(IEnumerable<T> items);
    void Remove(T item);
}