using System.Collections.Generic;

/// <summary>
/// interface for lists of things in which only one thing can be the selected/active
/// </summary>
/// <typeparam name="T"></typeparam>
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