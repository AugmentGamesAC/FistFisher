using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//this is for figuring out where exactly the user is pointing at 
//basically, attach to the thing that will point forwards, and anything that needs to get the forwards point for raycasts and such.
public interface IAimingDirection
{
    Vector3 StartingPoint { get; }
    Vector3 Direction { get; }
    Transform LatchForSpells { get; }
}
