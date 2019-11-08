using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : Task {
    public string TargetName;
    public string SpeedName;
    public string TurnSpeedName;
    public string AccuracyName;
    // Use this for initialization
    float Speed;
    float TurnSpeed;
    float Accuracy;
    public override NodeResult Execute()
    {
        GameObject go = tree.parent;
        GameObject target = (GameObject)tree.GetValue(TargetName);
        Speed = (float)tree.GetValue(SpeedName); // should, like targetname, pass the variable names in.
        TurnSpeed = (float)tree.GetValue(TurnSpeedName);
        Accuracy = (float)tree.GetValue(AccuracyName);
        if (Vector3.Distance(go.transform.position,target.transform.position) < Accuracy)
        {
            return NodeResult.SUCCESS;
        }
        Vector3 direction = target.transform.position - go.transform.position;
        go.transform.rotation = Quaternion.Slerp(go.transform.rotation, Quaternion.LookRotation(direction), TurnSpeed * Time.deltaTime);
        if (Vector3.Distance(go.transform.position, target.transform.position) < Speed * Time.deltaTime)
        {
            go.transform.position = target.transform.position;
        }
        else
        {
            go.transform.Translate(0, 0, Speed * Time.deltaTime);
        }
        return NodeResult.RUNNING;
    }

}
