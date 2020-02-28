using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeSlotManager : RetangleSlotManager
{
    protected override void Init()
    {
        UpgradeManager upgradeMaker = GetComponent<UpgradeManager>();
        if (upgradeMaker == default)
            throw new System.Exception("UpgradeMaker was not found on this object!!");

        for (int i = 0; i < m_slotCount; i++)
        {
            var obj = Instantiate(m_inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetGridPosition(i);
            obj.GetComponent<RectTransform>().sizeDelta = Vector2.one * m_boxScale;
            obj.GetComponentInChildren<ASlotRender>().SetSlotIndex(i);

            Upgrade upgrade = upgradeMaker.GenerateUpgrade();

            if (upgrade == default)
                return;
            obj.GetComponentInChildren<ASlotRender>().Tracker.AddItem(upgrade, 1);
        }
    }

    public override void HandleSlotDrop(PointerEventData eventData, ISlotData droppedOn)
    {
        var slotref = CommonMountPointer.eventData.pointerDrag.GetComponent<ASlotRender>();
        int newvalue = droppedOn.CheckAddItem(slotref.Tracker.Item, slotref.Tracker.Count);
        if (newvalue == slotref.Tracker.Count)
        {
            OnDrop(eventData);
            return;
        }
        var delta = slotref.Tracker.Count - newvalue;

        if (slotref.SlotMan != this)
            PlayerInstance.Instance.Clams.SetValue(PlayerInstance.Instance.Clams + slotref.Tracker.Item.WorthInCurrency * delta);

        base.HandleSlotDrop(eventData, droppedOn);
    }
}
