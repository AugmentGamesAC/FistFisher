using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SingleSelectionListTracker<T> : UITracker<ISingleSelectionList<T>>, ISingleSelectionList<T>
{
    public SingleSelectionListTracker()
    {
        m_value = new SingleSelectionList<T>();
    }

    public T SelectedItem => m_value.SelectedItem;

    public int Selection => m_value.Selection;

    public int Count => m_value.Count;

    public T this[int value] => m_value[value];

    public void AddItem(T item)
    {
        m_value.AddItem(item);
        UpdateState();
    }

    public void AddItems(IEnumerable<T> items)
    {
        m_value.AddItems(items);
        UpdateState();
    }


    public void IncrementSelection(int direction)
    {
        m_value.IncrementSelection(direction);
        UpdateState();
    }

    public void SetSelection(int selection)
    {
        m_value.SetSelection(selection);
        UpdateState();
    }

    public void Remove(T item)
    {
        m_value.Remove(item);
        UpdateState();
    }

    protected override ISingleSelectionList<T> ImplicitOverRide(UITracker<ISingleSelectionList<T>> reference)
    {
        return this;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return m_value.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return m_value.GetEnumerator();
    }
}
