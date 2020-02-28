using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTracker
{
    public virtual float MaxValue { get; }

    public virtual void Change(float changeAmount){}

    public virtual void SetMax(float max)
    {
        //throw new System.Exception("SetMax empty base function called.");
    }
}
