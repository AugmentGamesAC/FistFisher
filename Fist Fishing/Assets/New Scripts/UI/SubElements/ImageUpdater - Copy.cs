using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class IntImageUpdater : CoreUIUpdater<IntTracker, Image, int>
{
    [SerializeField]
    protected List<Sprite> SpriteList = new List<Sprite>();

    protected override void UpdateState(int value)
    {
        if (m_UIElement == default)
            return;

        m_UIElement.enabled = (value < 0 || value > SpriteList.Count -1);
        if (m_UIElement.enabled)
            m_UIElement.sprite = SpriteList[value];
    }
}
