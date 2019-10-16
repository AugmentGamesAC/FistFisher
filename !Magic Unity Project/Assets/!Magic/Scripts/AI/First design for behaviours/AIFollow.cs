using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFollow : MonoBehaviour
{
    private AIData data;
    private float pathUpdateTimer;
    //delay to only update the follow point after the delay amount.
    private float pathUpdateDelay = 1.0f;
    private float minDistanceToPoint = 2f;

    // Start is called before the first frame update
    void Start()
    {
        data = GetComponent<AIData>();
    }

    public void OnBehaviourStart()
    {
        if(Vector3.SqrMagnitude(data.followObject.position - data.AItransform.position) > data.maxMoveCushion * data.maxMoveCushion)
        {
            data.agent.SetDestination(data.followObject.position);
            pathUpdateTimer = 0;
            data.state = FollowFunction;
        }
        else
        {
            data.state = IdleFunction;
            //replace with data.IdleScript.OnBehaviourStart();
        }
    }

    public void OnBehaviourEnd()
    {
        //empty for now.
    }

    public void FollowFunction()
    {
        pathUpdateTimer += Time.deltaTime;

        //if too close to me, go to idle, or in the future, go to attack and change min distanceToPoint to the AI's attack range.
        if(Vector3.SqrMagnitude(data.followObject.position - data.AItransform.position) < minDistanceToPoint * minDistanceToPoint)
        {
            data.state = IdleFunction;
            //replace with data.IdleScript.OnBehaviourStart();
        }
        else if(pathUpdateTimer > pathUpdateDelay)
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
            data.state = FollowFunction;
        }
    }
}
