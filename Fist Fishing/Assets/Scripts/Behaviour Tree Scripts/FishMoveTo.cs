using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMoveTo : Task
{
    protected float m_speed;
    protected float m_turnSpeed;
    protected float m_accuracy;
    protected GameObject m_target;
    protected GameObject m_me;
    protected Vector3 m_direction;


    public override NodeResult Execute()
    {
        ReadInfo();


        if (Vector3.Distance(m_me.transform.position, m_target.transform.position) < m_accuracy)
        {
            return NodeResult.SUCCESS;
        }

        DetermineNextDirection();
        MoveToward();

        return NodeResult.RUNNING;
    }

    protected void ReadInfo()
    {
        m_me = m_tree.parent;
        m_target = (GameObject)m_tree.GetValue(FishBrain.TargetName);
        m_speed = (float)m_tree.GetValue(FishBrain.SpeedName); // should, like targetname, pass the variable names in.
        m_turnSpeed = (float)m_tree.GetValue(FishBrain.TurnSpeedName);
        m_accuracy = (float)m_tree.GetValue(FishBrain.AccuracyName);
    }


    protected void DetermineNextDirection()
    {
        m_direction = m_target.transform.position - m_me.transform.position;
        // not run into stuff is top priority
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, transform.forward * m_speed * 2, out hit))
            return;

        m_direction = Vector3.Reflect(transform.forward, hit.normal);
    }

    protected void MoveToward()
    {
        Quaternion turnDirection = Quaternion.FromToRotation(Vector3.forward, m_direction);
        transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, turnDirection, Time.deltaTime * m_turnSpeed);
        // then move

        transform.position = transform.position + transform.forward * m_speed * Time.deltaTime;
    }

}