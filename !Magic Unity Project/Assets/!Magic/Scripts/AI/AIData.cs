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
        Look
    };
    public Behaviour m_currentBehaviour;
    public Behaviour m_lastBehaviour;

    public delegate void State();
    public State state;

    public NavMeshAgent m_agent;
    public Transform followObject;//All enemies get a Ref to Player transform.

    //follow script vars for now.
    public float minDistToPlayer;
    public float maxDistToPlayer;

    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        //Default starting behaviour, could be idle or patrol.
        m_currentBehaviour = Behaviour.Idle;
        m_lastBehaviour = Behaviour.Idle;
    }
}
