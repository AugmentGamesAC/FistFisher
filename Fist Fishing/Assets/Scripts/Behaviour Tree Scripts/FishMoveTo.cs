using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMoveTo : FishTask
{

    protected Vector3 m_direction;


    public override NodeResult Execute()
    {
        ReadInfo();

        if (Vector3.Distance(m_me.transform.position, m_target.transform.position) < m_accuracy)
            return NodeResult.SUCCESS;

        DetermineNextDirection();
        MoveToward();

        return NodeResult.RUNNING;
    }




    protected void DetermineNextDirection()
    {
        m_direction = m_target.transform.position - m_me.transform.position;
        // not run into stuff is top priority
        RaycastHit hit;
        if (!Physics.Raycast(m_me.transform.position, m_me.transform.forward * m_speed * 2, out hit))
            return;

        m_direction = Vector3.Reflect(m_me.transform.forward, hit.normal);
    }

    protected void MoveToward()
    {
        Quaternion turnDirection = Quaternion.FromToRotation(Vector3.forward, m_direction);
        m_me.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, turnDirection, Time.deltaTime * m_turnSpeed);
        // then move

        m_me.transform.position = m_me.transform.position + m_me.transform.forward * m_speed * Time.deltaTime;
    }

}