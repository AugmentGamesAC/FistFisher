using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : Task
{
    public string TargetName;
    public string SpeedName;
    public string TurnSpeedName;
    public string AccuracyName;
    // Use this for initialization
    protected float m_speed;
    protected float TurnSpeed;
    protected float Accuracy;

    protected Vector3 m_GoalPostion;  


    public override NodeResult Execute()
    {
        GameObject go = m_tree.parent;
        GameObject target = (GameObject)m_tree.GetValue(TargetName);
        m_speed = (float)m_tree.GetValue(SpeedName); // should, like targetname, pass the variable names in.
        TurnSpeed = (float)m_tree.GetValue(TurnSpeedName);
        Accuracy = (float)m_tree.GetValue(AccuracyName);

        if (Vector3.Distance(go.transform.position, target.transform.position) < Accuracy)
        {
            return NodeResult.SUCCESS;
        }

        Vector3 direction = target.transform.position - go.transform.position;
        go.transform.rotation = Quaternion.Slerp(go.transform.rotation, Quaternion.LookRotation(direction), TurnSpeed * Time.deltaTime);
        if (Vector3.Distance(go.transform.position, target.transform.position) < m_speed * Time.deltaTime)
        {
            go.transform.position = target.transform.position;
        }
        else
        {
            go.transform.Translate(0, 0, m_speed * Time.deltaTime);
        }
        return NodeResult.RUNNING;
    }

}
