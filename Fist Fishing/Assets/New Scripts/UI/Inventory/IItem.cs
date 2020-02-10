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
}
