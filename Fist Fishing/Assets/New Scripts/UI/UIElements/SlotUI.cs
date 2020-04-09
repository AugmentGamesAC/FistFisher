using UnityEngine;

/// <summary>
/// UI updater for inventory slots
/// </summary>
[RequireComponent(typeof(ASlotRender))]
public class SlotUI : CoreUIElement<ISlotData>
{
    [SerializeField]
    protected FloatTextProUpdater CountDisplay;
    [SerializeField]
    protected ImageUpdater Image;

    /// <summary>
    /// Gets the selectedSlotInformation 
    /// </summary>
    /// <param name="newData"></param>
    public override void UpdateUI(ISlotData newData)
    {
        if (!ShouldUpdateUI(newData,x=>newData.Item != null))
            return;

        CountDisplay.ForceUpdate(newData.Count);
        Image.ForceUpdate(newData.Item.IconDisplay);
    }
}
