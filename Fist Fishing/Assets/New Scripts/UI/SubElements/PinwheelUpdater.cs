using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinwheelUpdater<T> : CoreUIUpdater<PinwheelTracker<T>, CoreUIElement<T>, IPinWheel<T>>
{
    protected override void UpdateState(IPinWheel<T> value)
    {
        m_UIElement.UpdateUI(value.GetSelectedOption());
    }
}
