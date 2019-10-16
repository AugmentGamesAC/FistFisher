using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABehaviour : MonoBehaviour
{
    protected AIData data;
    protected float pathUpdateTimer;
    protected float pathUpdateDelay;
    protected float minDistanceToPoint;

    public abstract void OnBehaviourStart();
    public abstract void OnBehaviourUpdate();
    public abstract void OnBehaviourEnd();
}
