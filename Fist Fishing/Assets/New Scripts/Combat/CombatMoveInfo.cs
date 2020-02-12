using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum MoveType
{
    Regular,
    Superior, 
    Shotgun,
    SuperiorShotgun,
    FrogHop,
    Funeral
}

public enum SweetSpotRange
{
    Close, 
    Mid,
    Long
}

/// <summary>
/// Holds all the information needed for a player's move.
/// </summary>
[CreateAssetMenu(fileName = "New Combat Move Definition", menuName = "Combat/ Combat Moves")]
public class CombatMoveInfo : ScriptableObject
{
    [SerializeField]
    protected MoveType m_currentMoveType;
    public MoveType CurrentMoveType => m_currentMoveType;

    [SerializeField]
    protected FloatTracker m_damage;
    public FloatTracker Damage => m_damage;

    [SerializeField]
    protected FloatTracker m_slow;
    public FloatTracker Slow => m_slow;

    [SerializeField]
    protected FloatTracker m_noise;
    public FloatTracker Noise => m_noise;

    [SerializeField]
    protected FloatTracker m_moveDistance;
    public FloatTracker MoveDistance => m_moveDistance;

    [SerializeField]
    protected FloatTracker m_oxygenConsumption;
    public FloatTracker OxygenConsumption => m_oxygenConsumption;

    [SerializeField]
    protected SweetSpotRange m_sweetSpot;
    public SweetSpotRange SweetSpot => m_sweetSpot;

    [SerializeField]
    protected TextTracker m_name;
    public TextTracker Name => m_name;

    [SerializeField, TextArea(10, 15)]
    protected TextTracker m_description;
    public TextTracker Description => m_description;


    [SerializeField]
    protected ImageTracker m_icon;
    public ImageTracker Icon => m_icon;
}
