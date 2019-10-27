using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIData))]
public class AIDefaultBehaviourTree : MonoBehaviour
{
    private AIData data;
    private AIFollow followScript;
    private Idle idleScript;
    private Look lookScript;
    private AIFlyTo flyScript;

    // Start is called before the first frame update
    void Start()
    {
        //check if behaviour tree has all default behaviours.
        if (GetComponent<AIFollow>() == null)
            gameObject.AddComponent<AIFollow>();
        if (GetComponent<AIFlyTo>() == null) //for now. make something check if flying or not later
            gameObject.AddComponent<AIFlyTo>();

        if (GetComponent<Idle>() == null)
            gameObject.AddComponent<Idle>();

        if (GetComponent<Look>() == null)
            gameObject.AddComponent<Look>();

        //set our scripts.
        data = GetComponent<AIData>();
        followScript = GetComponent<AIFollow>();
        idleScript = GetComponent<Idle>();
        lookScript = GetComponent<Look>();
        flyScript = GetComponent<AIFlyTo>(); //for now

        //set our current state dependant on the Ai's current behaviour.
        switch (data.m_currentBehaviour)
        {
            case AIData.Behaviour.Follow:
                data.state = followScript.OnBehaviourStart;
                break;

            case AIData.Behaviour.FlyTo:
                data.state = flyScript.OnBehaviourStart;
                break;

            case AIData.Behaviour.Idle:
                data.state = idleScript.OnBehaviourStart;
                break;

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
        //change current behaviour in behaviour scripts and let the scripts update the states.
        //if current behaviour is changed from last frame.
        if(data.m_currentBehaviour != data.m_lastBehaviour)
        {
            //set the state according to its new behaviour.
            switch (data.m_currentBehaviour)
            {
                case AIData.Behaviour.Follow:
                    data.state = followScript.OnBehaviourStart;
                    break;

                case AIData.Behaviour.Idle:
                    data.state = idleScript.OnBehaviourStart;
                    break;

                case AIData.Behaviour.FlyTo:
                    data.state = flyScript.OnBehaviourStart;
                    break;

                //case AIData.Behaviour.Look:
                //    data.state = lookScript.OnBehaviourStart;
                //    break;

                default:
                    break;
            }

            //stop the last behaviour.
            switch (data.m_lastBehaviour)
            {
                case AIData.Behaviour.Follow:
                    followScript.OnBehaviourEnd();
                    break;

                case AIData.Behaviour.FlyTo:
                    flyScript.OnBehaviourEnd();
                    break;

                case AIData.Behaviour.Idle:
                    idleScript.OnBehaviourEnd();
                    break;

                //case AIData.Behaviour.Look:
                //    lookScript.OnBehaviourEnd();
                //    break;

                default:
                    break;
            }
        //update last to current behaviour to not run the previous block.
        data.m_lastBehaviour = data.m_currentBehaviour;
        }

        //run the functionality for this frame.
        data.state();
    }
}
