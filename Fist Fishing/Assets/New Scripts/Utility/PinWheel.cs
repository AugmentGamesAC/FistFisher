using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PinwheelInfo<T>
//--
//Dictionary<int, T> PossibleOptions
//intSelectedOption;
//--
//T GetSelectedOption()
//T ChooseSelectedOption(int)
//bool SetSelectedOption(int, T, bool override);
////override will allow use to set the option, if it is false the return will 
//return false if the int is already assigned to an object
//--
/// </summary>
/// <typeparam name="T"></typeparam>
public class PinWheel<T>
{
    Dictionary<int, T> Slots = new Dictionary<int, T>();
    int SelectedSlot;

    public T GetSelectedOption()
    {
        return Slots[SelectedSlot];
    }

    public T ChooseSelectedOption(int index)
    {
        T option;

        if (!Slots.TryGetValue(index, out option))
            return default;

        SelectedSlot = index;

        return option;
    }

    public bool RemoveSelectedOption(int index)
    {
        return Slots.Remove(index);
    }

    public bool SetSelectedOption(int index, T newOption, bool overwrite)
    {
        if(Slots[index] == default || overwrite)
        {
            Slots[index] = newOption;
            return true;
        }

        return false;
    }

}
