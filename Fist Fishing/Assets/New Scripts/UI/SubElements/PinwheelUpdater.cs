using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// updater for pinwheels
/// </summary>
/// <typeparam name="T"></typeparam>
public class PinwheelUpdater<T> : CoreUIUpdater<PinwheelTracker<T>, CoreUIElement<T>, IPinWheel<T>>
{
    protected override void UpdateState(IPinWheel<T> value)
    {
        m_UIElement.UpdateUI(value.GetSelectedOption());
    }
}
