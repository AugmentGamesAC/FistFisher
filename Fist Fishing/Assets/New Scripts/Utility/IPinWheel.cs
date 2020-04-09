using System.Collections.Generic;
/// <summary>
/// interface for pinwheels (circular multi-option selectable things)
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IPinWheel<T>
{
    int SelectedSlot { get; }
    Dictionary<int, T> Slots { get; }
    T GetSelectedOption();
    bool RemoveSelectedOption(int index);
    bool SetNewOption(int index, T newOption, bool overwrite);
    T SetSelectedOption(int index);
}