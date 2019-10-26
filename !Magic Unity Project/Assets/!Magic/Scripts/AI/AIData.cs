using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIData : MonoBehaviour
{
    public enum Behaviour
    {
        Follow,
        Idle,
        Patrol,
        Attack
    };

    public Behaviour m_currentBehaviour;
    public Behaviour m_lastBehaviour;

    public delegate void State();
    public State state;

    public NavMeshAgent m_agent;
    public Transform followObject;//All enemies get a Ref to Player transform.

    //follow script vars for now.
    public float sightRange;
    public float attackRange;
    public float patrolDelay;

    void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();

        //Default starting behaviour, could be idle or patrol.
        m_currentBehaviour = Behaviour.Patrol;
        m_lastBehaviour = Behaviour.Patrol;

        sightRange = 10.0f;
        patrolDelay = 3.0f;
        attackRange = 3.1f;
    }
}
