using UnityEngine;


public class SlotUI : CoreUIElement<SlotData>
{
    [SerializeField]
    protected FloatTextUpdater CountDisplay;
    [SerializeField]
    protected ImageUpdater Image;
    [SerializeField]

    /// <summary>
    /// Gets selected fish from combat manager.
    /// </summary>
    /// <param name="newData"></param>
    public override void UpdateUI(SlotData newData)
    {
        if (!ShouldUpdateUI(newData))
            return;

        CountDisplay.ForceUpdate(newData.Count);
        Image.ForceUpdate(newData.Item.IconDisplay);
    }
}
