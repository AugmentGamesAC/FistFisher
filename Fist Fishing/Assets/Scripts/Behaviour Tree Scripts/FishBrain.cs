﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// FishBrain 
/// base for fish AI
/// </summary>
public class FishBrain : BehaviorTree
{
    /// <summary>
    /// Fish Classification is the limit of the mental categories that a fish can percieve
    /// in being able to set these values 
    /// </summary>
    [System.Flags]
    public enum FishClassification
    {
        Passive             = 0x000001,
        Aggressive          = 0x000002,
        Fearful             = 0x000004,    
        Player              = 0x000008,


        FishMask = FishBrain.FishClassification.Aggressive | FishBrain.FishClassification.Fearful | FishBrain.FishClassification.Passive,

        BaitSensitive1      = 0x000100,
        BaitSensitive2      = 0x000200,
        BaitSensitive3      = 0x000400,
        BaitSensitive4      = 0x000800,
        IsBait              = BaitSensitive1 | BaitSensitive2 | BaitSensitive3 | BaitSensitive4,
        BaitRepel1          = 0x001000,
        BaitRepel2          = 0x002000,
        BaitRepel3          = 0x004000,
        BaitRepel4          = 0x008000,
        IsRepellent         = BaitRepel1 | BaitRepel2 | BaitRepel3 | BaitRepel4,

        RepellentStrength1  = 0x000010,
        RepellentStrength2  = 0x000020,
        RepellentStrength3  = 0x000040,
        RepellentStrength4  = 0x000080,
       

        FavoredPlant1 = 0x010000
    }

    static public string TargetName  = "defaultGoal";
    static public string DirectionName = "direction";
    static public string SpeedName = "speed" ;
    static public string TurnSpeedName = "turningSpeed";
    static public string AccuracyName = "accuracy";
    static public string MoodName = "mood";
    static public string BiteCooldownName = "biteCool";
    public float Accuracy = 1.0f;

    //this is a dummygame object that will be repositioned when NewFishPoint is used
    public GameObject defaultGoal;
    // Start is called before the first frame update
    protected BasicFish body;

    public override void Awake()
    {
        parent = gameObject;
        CallStack = new Stack<Node>();
        Blackboard = new Hashtable();
    }



    protected Dictionary<GameObject, FishReaction> m_fishMood = default;
    public void ApplyImpulse(GameObject triggeringObject, float intensity, FishClassification FishType)
    {
        if (m_fishMood == default)
            m_fishMood = (Dictionary <GameObject, FishReaction>) GetValue(MoodName);

        FishReaction reaction;
        if (m_fishMood.TryGetValue(triggeringObject, out reaction))
        {
            reaction.m_intensity += intensity;
            return;
        }

        m_fishMood[triggeringObject] = new FishReaction() {m_fishRecognition = FishType,m_intensity = intensity };
    }


    /// <summary>
    /// This is the start of a very simple fish brain that picks a point to wander to then swims to it
    /// </summary>
    void Start()
    {
        defaultGoal = new GameObject();
        body = GetComponent<BasicFish>(); 
        SetValue(TargetName, defaultGoal);
        SetValue(SpeedName, body.Speed);
        SetValue(TurnSpeedName, body.TurnSpeed);
        SetValue(AccuracyName, Accuracy);
        SetValue(MoodName, new Dictionary<GameObject, FishReaction>());
        SetValue(DirectionName, defaultGoal.transform.position - body.transform.position);
        SetValue(BiteCooldownName, 0.0f);

        //chose the root behavoir for this tree.
        //does each child
        Sequence newRoot = gameObject.AddComponent<Sequence>();
        newRoot.Init(this, new List<Node>(){
            gameObject.AddComponent<FishWander>(),
            gameObject.AddComponent<FishMoveTo>(),
            gameObject.AddComponent<FishAttack>()
        });
        root = newRoot;

    }
}
