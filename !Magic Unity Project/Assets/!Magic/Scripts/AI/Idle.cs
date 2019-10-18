using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : ABehaviour
{
    LineRenderer m_line;

    public override void OnBehaviourStart()
    {
        Init();

        m_line = gameObject.GetComponent<LineRenderer>();

        m_updateTimer = 0;
        m_data.state = OnBehaviourUpdate;
    }

    public override void OnBehaviourUpdate()
    {
        m_updateTimer += Time.deltaTime;

        Vector3 direction = m_data.followObject.position - transform.position;

        Ray ray = new Ray(transform.position, direction);
        RaycastHit Hit;

        m_line.enabled = true;
        m_line.SetPosition(0, ray.origin);

        if (Physics.Raycast(ray, out Hit, 3000))
        {
            //if target is player and distance between the two 
            if (Hit.collider.tag == "Player Target" && 
                (Hit.distance < m_data.maxDistToPlayer))
            {
                m_line.SetPosition(1, Hit.transform.position);

                //Set to Follow for now.
                m_data.m_currentBehaviour = AIData.Behaviour.Follow;
            }
        }


        //if distance between AI and follow object is smaller than the max dist that we want,
        if (Vector3.SqrMagnitude(m_data.followObject.position - m_data.transform.position) < m_data.maxDistToPlayer * m_data.maxDistToPlayer)
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
