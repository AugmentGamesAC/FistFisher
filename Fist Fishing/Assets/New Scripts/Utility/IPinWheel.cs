using System.Collections.Generic;

public interface IPinWheel<T>
{
    int SelectedSlot { get; }
    Dictionary<int, T> Slots { get; }
    T GetSelectedOption();
    bool RemoveSelectedOption(int index);
    bool SetNewOption(int index, T newOption, bool overwrite);
    T SetSelectedOption(int index);
}