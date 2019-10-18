using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : ABehaviour
{
    public override void OnBehaviourStart()
    {
        Init();

        m_updateTimer = 0;
        m_data.state = OnBehaviourUpdate;
    }

    public override void OnBehaviourUpdate()
    {
        //if distance between AI and follow object is smaller than the max dist that we want,
        if (Vector3.SqrMagnitude(m_data.followObject.position - m_data.m_AItransform.position) < m_data.maxDistToPlayer * m_data.maxDistToPlayer)
        {
            //don't move;
           

        }
        //else if(/*out of sight for certain amount of time.*/)
        //{
        //    //go into patrol behaviour
        //}
        //else if(/*player is within attack range*/)
        //{
        //    //attack behaviour
        //}
    }
    public override void OnBehaviourEnd()
    {

    }
}
