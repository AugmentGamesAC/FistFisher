using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFollow : ABehaviour
{
    void Start()
    {
        data = GetComponent<AIData>();
        //delay to only update the follow point after the delay amount.
        pathUpdateDelay = 1.0f;
        minDistanceToPoint = 2f;
    }

    public override void OnBehaviourStart()
    {
        if (Vector3.SqrMagnitude(data.followObject.position - data.AItransform.position) > data.maxMoveCushion * data.maxMoveCushion)
        {
            data.agent.SetDestination(data.followObject.position);
            pathUpdateTimer = 0;
            data.state = OnBehaviourUpdate;
        }
        else
        {
            data.state = IdleFunction;
            //replace with data.IdleScript.OnBehaviourStart();
        }
    }

    public override void OnBehaviourEnd()
    {
        //empty for now.
    }

    public override void OnBehaviourUpdate()
    {
        pathUpdateTimer += Time.deltaTime;

        //if too close to me, go to idle, or in the future, go to attack and change min distanceToPoint to the AI's attack range.
        if (Vector3.SqrMagnitude(data.followObject.position - data.AItransform.position) < minDistanceToPoint * minDistanceToPoint)
        {
            data.state = IdleFunction;
            //data.currentBehaviour = AIData.Behaviour.Follow;
            //replace with data.IdleScript.OnBehaviourStart();
        }
        else if (pathUpdateTimer > pathUpdateDelay)
        {
            data.agent.SetDestination(data.followObject.position);
            pathUpdateTimer = 0;
        }
    }

    public void IdleFunction()
    {
        //if distance between AI and follow object is smaller than the max dist that we want,
        if (Vector3.SqrMagnitude(data.followObject.position - data.AItransform.position) > data.maxMoveCushion * data.maxMoveCushion)
        {
            data.agent.SetDestination(data.followObject.position);
            //this should be in idle script.
            data.state = OnBehaviourStart;
        }
    }
}
