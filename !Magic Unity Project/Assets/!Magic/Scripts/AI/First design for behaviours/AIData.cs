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
    public Behaviour currentBehaviour;
    public Behaviour lastBehaviour;

    public delegate void State();
    public State state;

    public NavMeshAgent agent;
    public Transform AItransform;//this Enemy's position.
    public Transform followObject;//All enemies get a Ref to Player transform.

    //follow script vars for now.
    public float minMoveCushion;
    public float maxMoveCushion;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        AItransform = transform;
        //Default starting behaviour, could be idle or patrol.
        currentBehaviour = Behaviour.Follow;
        lastBehaviour = Behaviour.Follow;
    }
}
