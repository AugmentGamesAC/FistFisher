using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageUpdater : CoreUIUpdater<ImageTracker, Image, Sprite>
{
    protected override void UpdateState(Sprite value)
    {
        if (m_UIElement == default)
            return;

        m_UIElement.enabled = (value != default);
        if (m_UIElement.enabled)
            m_UIElement.sprite = value;
    }
}
