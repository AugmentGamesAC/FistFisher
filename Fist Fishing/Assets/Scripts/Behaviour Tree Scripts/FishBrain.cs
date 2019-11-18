using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// FishBrain 
/// </summary>
public class FishBrain : BehaviorTree
{
    static public string TargetName  = "defaultGoa";
    static public string SpeedName = "speed" ;
    static public string TurnSpeedName = "turningSpeed";
    static public string AccuracyName = "accuracy";
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



    void Start()
    {
        defaultGoal = new GameObject();
        body = GetComponent<BasicFish>(); //TODO resolve creation order better
        SetValue(TargetName, defaultGoal);
        SetValue(SpeedName, body.Speed);
        SetValue(TurnSpeedName, body.TurnSpeed);
        SetValue(AccuracyName, Accuracy);

        //chose the root behavoir for this tree.
        //does each child
        Sequence newRoot = gameObject.AddComponent<Sequence>();
        newRoot.Init(this, new List<Node>(){
            gameObject.AddComponent<FishWander>(),
            gameObject.AddComponent<FishMoveTo>()
        });
        root = newRoot;

    }
}
