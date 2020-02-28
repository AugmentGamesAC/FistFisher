using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    int StackSize { get; }
    int ID { get; }
    int WorthInCurrency { get; }
    ItemType Type { get; }
    string Description { get; }
    Sprite IconDisplay { get; }
    string Name { get; }

    bool CanMerge(IItem newItem);

    /// <summary>
    /// Handles resolution of the drop.
    /// </summary>
    /// <param name="slot"></param>
    /// <returns>true when drop logic handled otherwise false.</returns>
    bool ResolveDropCase(ISlotData newSlot, ISlotData oldSlot);
}
