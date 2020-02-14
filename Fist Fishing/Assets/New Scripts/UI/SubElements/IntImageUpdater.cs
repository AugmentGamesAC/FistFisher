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
        if (SpriteList.Count == 0)
            return;

        m_UIElement.enabled = (value > -1 && value < SpriteList.Count);
        if (m_UIElement.enabled)
            m_UIElement.sprite = SpriteList[value];
    }
}
