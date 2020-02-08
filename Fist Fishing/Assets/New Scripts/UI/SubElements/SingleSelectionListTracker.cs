﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SingleSelectionListTracker<T> : UITracker<ISingleSelectionList<T>>, ISingleSelectionList<T>
{
    public T SelectedItem => m_value.SelectedItem;

    public int Selection => m_value.Selection;

    public int Count => m_value.Count;

    public void AddItem(T item)
    {
        m_value.AddItem(item);
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
}
