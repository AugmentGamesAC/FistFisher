using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for tracking noise.
/// Combat Manager will subscribe to current amount changed. 
/// 
/// </summary>
public class NoiseTracker : StatTracker
{
    public NoiseTracker(float startingAmount)
    {
        m_currentAmount = startingAmount;
    }

    public NoiseTracker() { }
}
