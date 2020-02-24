using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSlotManager : RetangleSlotManager
{
    UpgradeManager upgradeMaker = new UpgradeManager();

    protected override void Init()
    {
        for (int i = 0; i < m_slotCount; i++)
        {
            var obj = Instantiate(m_inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetGridPosition(i);
            obj.GetComponent<RectTransform>().sizeDelta = Vector2.one * m_boxScale;
            obj.GetComponentInChildren<ASlotRender>().SetSlotIndex(i);

            Upgrade upgrade = upgradeMaker.GenerateUpgrade();

            obj.GetComponentInChildren<ASlotRender>().Tracker.AddItem(upgrade, 1);
        }
    }


}
