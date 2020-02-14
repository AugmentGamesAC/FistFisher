using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFeeder : MonoBehaviour
{
    public GameObjectMonoPool sourcePool;

    public string message;
    //public GameObject destination;
    public float invokeDelay = 1f;
    public float invokePeriod = .2f;

    public ObjectLauncher launcher;

    private void Start()
    {
        InvokeRepeating("FeedObject", invokeDelay, invokePeriod);
    }

    void FeedObject()
    {
        //destination.SendMessage(message, sourcePool.pool.GetInstance(), SendMessageOptions.DontRequireReceiver);
        launcher.Launch(sourcePool.pool.GetInstance());
    }
}
