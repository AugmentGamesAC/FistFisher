using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIData))]
public class TurretBehaviourTree : MonoBehaviour
{
    private AIData m_data;
    private AITurretIdle m_turretIdleScript;
    private AITurretAttack m_turretAttackScript;

    // Start is called before the first frame update
    void Start()
    {
        //check if behaviour tree has all default behaviours and add them.
        if (GetComponent<AIData>() == null)
            gameObject.AddComponent<AIData>();
        if (GetComponent<AITurretIdle>() == null)
            gameObject.AddComponent<AITurretIdle>();
        if (GetComponent<AITurretAttack>() == null)
            gameObject.AddComponent<AITurretAttack>();

        //set our scripts.
        m_data = GetComponent<AIData>();
        m_turretIdleScript = GetComponent<AITurretIdle>();
        m_turretAttackScript = GetComponent<AITurretAttack>();

        //set our current state dependant on the Ai's current behaviour.
        switch (m_data.m_currentBehaviour)
        {
            case AIData.Behaviour.Idle:
                m_data.state = m_turretIdleScript.OnBehaviourStart;
                break;

            case AIData.Behaviour.Attack:
                m_data.state = m_turretAttackScript.OnBehaviourStart;
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
                case AIData.Behaviour.Idle:
                    m_data.state = m_turretIdleScript.OnBehaviourStart;
                    break;

                case AIData.Behaviour.Attack:
                    m_data.state = m_turretAttackScript.OnBehaviourStart;
                    break;

                default:
                    break;
            }

            //stop the last behaviour.
            switch (m_data.m_lastBehaviour)
            {

                case AIData.Behaviour.Idle:
                    m_turretIdleScript.OnBehaviourEnd();
                    break;

                case AIData.Behaviour.Attack:
                    m_turretAttackScript.OnBehaviourEnd();
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
