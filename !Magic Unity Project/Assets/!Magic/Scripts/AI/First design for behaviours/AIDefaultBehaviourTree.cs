using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AIData))]
public class AIDefaultBehaviourTree : MonoBehaviour
{
    private AIData data;
    private AIFollow followScript;
    private Idle idleScript;
    private Look lookScript;

    // Start is called before the first frame update
    void Start()
    {
        //check if behaviour tree has all default behaviours.
        if (GetComponent<AIFollow>() == null)
            gameObject.AddComponent<AIFollow>();

        if (GetComponent<Idle>() == null)
            gameObject.AddComponent<Idle>();

        if (GetComponent<Look>() == null)
            gameObject.AddComponent<Look>();

        //set our scripts.
        data = GetComponent<AIData>();
        followScript = GetComponent<AIFollow>();
        idleScript = GetComponent<Idle>();
        lookScript = GetComponent<Look>();

        //set our current state dependant on the Ai's current behaviour.
        switch (data.currentBehaviour)
        {
            case AIData.Behaviour.Follow:
                data.state = followScript.OnBehaviourStart;
                break;

            //case AIData.Behaviour.Idle:
            //    data.state = idleScript.OnBehaviourStart;
            //    break;

            //case AIData.Behaviour.Look:
            //    data.state = lookScript.OnBehaviourStart;
            //    break;

            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if current behaviour is changed from last frame.
        if(data.currentBehaviour != data.lastBehaviour)
        {
            //set the state according to its new behaviour.
            switch (data.currentBehaviour)
            {
                case AIData.Behaviour.Follow:
                    data.state = followScript.OnBehaviourStart;
                    break;

                //case AIData.Behaviour.Idle:
                //    data.state = idleScript.OnBehaviourStart;
                //    break;

                //case AIData.Behaviour.Look:
                //    data.state = lookScript.OnBehaviourStart;
                //    break;

                default:
                    break;
            }

            //stop the last behaviour.
            switch (data.lastBehaviour)
            {
                case AIData.Behaviour.Follow:
                    followScript.OnBehaviourEnd();
                    break;

                //case AIData.Behaviour.Idle:
                //    idleScript.OnBehaviourEnd();
                //    break;

                //case AIData.Behaviour.Look:
                //    lookScript.OnBehaviourEnd();
                //    break;

                default:
                    break;
            }
        }

        //run the functionality for this frame.
        data.state();
        //update last behaviour.
        data.lastBehaviour = data.currentBehaviour;
    }
}
