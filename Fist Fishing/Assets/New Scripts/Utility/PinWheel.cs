﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class for pinwheels to expand upon interface
/// </summary>
/// <typeparam name="T"></typeparam>
public class PinWheel<T> : IPinWheel<T>
{
    [SerializeField]
    protected Dictionary<int, T> m_slots = new Dictionary<int, T>();
    public Dictionary<int, T> Slots { get { return m_slots; } }

    [SerializeField]
    protected int m_selectedSlot;
    public int SelectedSlot { get { return m_selectedSlot; } }

    /// <summary>
    /// Creates a pinwheel with an Enumerable(List, etc.)
    /// </summary>
    /// <param name="startingNumber"></param>
    /// <param name="objects"></param>
    public PinWheel(int startingNumber, IEnumerable<T> objects)
    {
        if (objects == default)
            return;
        foreach (T curObject in objects)
            m_slots.Add(startingNumber++, curObject);

        m_selectedSlot = startingNumber;
    }

    /// <summary>
    /// Returns currently selected
    /// </summary>
    /// <returns></returns>
    public T GetSelectedOption()
    {
        T option;

        if (!m_slots.TryGetValue(m_selectedSlot, out option))
            return default;

        return option;
    }

    /// <summary>
    /// Set the member index "SelectedSlot"
    /// returns the value for "index" key.
    /// </summary>
    public T SetSelectedOption(int index)
    {
        T option;

        if (!m_slots.TryGetValue(index, out option))
            return default;

        m_selectedSlot = index;

        return option;
    }

    /// <summary>
    /// Sets value to null. (does not reduce container size)
    /// Returns true if successful.
    /// </summary>
    public bool RemoveSelectedOption(int index)
    {
        return m_slots.Remove(index);
    }

    /// <summary>
    /// Creates a new <T> Option 
    /// </summary>
    public bool SetNewOption(int index, T newOption, bool overwrite)
    {
        if (!m_slots.ContainsKey(index) || overwrite)
        {
            m_slots[index] = newOption;
            return true;
        }

        return false;
    }

    /// <summary>
    /// 2 Axis selection with keyboard. WASD
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static int TwoDToSelectionKeyboard(float x, float y)
    {
 
        if (x < 0)
            return (y > 0) ? 8 : ((y < 0) ? 6 : 7);
        else if (x > 0)
            return (y > 0) ? 2 : ((y < 0) ? 4 : 3);
        else
            return (y > 0) ? 1 : ((y < 0) ? 5 : 0);
    }

}
