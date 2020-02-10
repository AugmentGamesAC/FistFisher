
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class PinwheelTracker<T> : UITracker<IPinWheel<T>>, IPinWheel<T>
{
    public PinwheelTracker(int startingNumber, IEnumerable<T> objects)
    {
        m_value = new PinWheel<T>(startingNumber, objects);
    }

    public int SelectedSlot => m_value.SelectedSlot;

    public Dictionary<int, T> Slots => m_value.Slots;

    public T GetSelectedOption()
    {
        return m_value.GetSelectedOption();
    }

    public bool RemoveSelectedOption(int index)
    {
        bool returnVal = m_value.RemoveSelectedOption(index);
        if (returnVal)
            UpdateState();
        return returnVal;
    }

    public bool SetNewOption(int index, T newOption, bool overwrite)
    {
        bool returnVal = m_value.SetNewOption(index,newOption,overwrite);
        if (returnVal)
            UpdateState();
        return returnVal;
    }

    public T SetSelectedOption(int index)
    {
        T returnVal = m_value.SetSelectedOption(index);
        if (returnVal != default)
            UpdateState();
        return returnVal;
    }

    protected override IPinWheel<T> ImplicitOverRide(UITracker<IPinWheel<T>> reference)
    {
        return this;
    }
}

