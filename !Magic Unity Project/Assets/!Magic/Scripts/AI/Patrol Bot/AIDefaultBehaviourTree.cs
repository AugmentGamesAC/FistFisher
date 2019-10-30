using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIData))]
public class AIDefaultBehaviourTree : MonoBehaviour
{
    private AIData m_data;
    private AIFollow m_followScript;
    private Idle m_idleScript;
    private AIPatrol m_patrolScript;
    private AIAttack m_attackScript;

    // Start is called before the first frame update
    void Start()
    {
        //check if behaviour tree has all default behaviours and add them.
        if (GetComponent<AIData>() == null)
            gameObject.AddComponent<AIData>();
        if (GetComponent<AIFollow>() == null)
            gameObject.AddComponent<AIFollow>();
        if (GetComponent<Idle>() == null)
            gameObject.AddComponent<Idle>();
        if (GetComponent<AIPatrol>() == null)
            gameObject.AddComponent<AIPatrol>();
        if (GetComponent<AIAttack>() == null)
            gameObject.AddComponent<AIAttack>();

        //set our scripts.
        m_data = GetComponent<AIData>();
        m_followScript = GetComponent<AIFollow>();
        m_idleScript = GetComponent<Idle>();
        m_patrolScript = GetComponent<AIPatrol>();
        m_attackScript = GetComponent<AIAttack>();

        //set our current state dependant on the Ai's current behaviour.
        switch (m_data.m_currentBehaviour)
        {
            case AIData.Behaviour.Follow:
                m_data.state = m_followScript.OnBehaviourStart;
                break;

            case AIData.Behaviour.Idle:
                m_data.state = m_idleScript.OnBehaviourStart;
                break;

            case AIData.Behaviour.Patrol:
                m_data.state = m_patrolScript.OnBehaviourStart;
                break;

            case AIData.Behaviour.Attack:
                m_data.state = m_attackScript.OnBehaviourStart;
                break;

            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //change current behaviour in behaviour scripts and let the scripts update the states.
        //if current behaviour is changed from last frame.
        if (m_data.m_currentBehaviour != m_data.m_lastBehaviour)
        {
            //set the state according to its new behaviour.
            switch (m_data.m_currentBehaviour)
            {
                case AIData.Behaviour.Follow:
                    m_data.state = m_followScript.OnBehaviourStart;
                    break;

                case AIData.Behaviour.Idle:
                    m_data.state = m_idleScript.OnBehaviourStart;
                    break;

                case AIData.Behaviour.Patrol:
                    m_data.state = m_patrolScript.OnBehaviourStart;
                    break;

                case AIData.Behaviour.Attack:
                    m_data.state = m_attackScript.OnBehaviourStart;
                    break;

                default:
                    break;
            }

            //stop the last behaviour.
            switch (m_data.m_lastBehaviour)
            {
                case AIData.Behaviour.Follow:
                    m_followScript.OnBehaviourEnd();
                    break;

                case AIData.Behaviour.Idle:
                    m_idleScript.OnBehaviourEnd();
                    break;

                case AIData.Behaviour.Patrol:
                    m_patrolScript.OnBehaviourEnd();
                    break;

                case AIData.Behaviour.Attack:
                    m_attackScript.OnBehaviourEnd();
                    break;

                default:
                    break;
            }
            //update last to current behaviour to not run the previous block.
            m_data.m_lastBehaviour = m_data.m_currentBehaviour;
        }

        //run the currentBehaviour's function for this frame.
        m_data.state();
    }
}
