using UnityEngine;


public class SlotUI : CoreUIElement<ISlotData>
{
    [SerializeField]
    protected FloatTextUpdater CountDisplay;
    [SerializeField]
    protected ImageUpdater Image;
    [SerializeField]

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
