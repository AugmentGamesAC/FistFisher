using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds all the information needed for a player's move.
/// </summary>
public class CombatMoveInfo
{
    public float Damage;
    public float Slow;
    public float Noise;
    public float MoveDistance;
    public float Oxygen;

    /// <summary>
    /// probably RangeZone enum instead of float.
    /// </summary>
    public float SweetSpot;
}
