using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class SamMultiGenericClass<T, U> 
//    where T : ICollection
//    where U : FloatTextUpdater
//{

//}

public class MonoPool : MonoBehaviour
{
    public int capacity;
    public IPool<GameObject> pool { get; private set; }
    public GameObject prototype;

    public enum PoolType { Queue, List}
    public PoolType currentPoolType = PoolType.Queue;
    public bool expandable = true;

    // Start is called before the first frame update
    void Awake()
    {
        switch (currentPoolType)
            {
            case PoolType.Queue:
                pool = new QueuePool<GameObject>(() => Create(prototype), capacity);
                break;
            case PoolType.List:
                pool = new ListPool<GameObject>(() => Create(prototype), capacity, g=>g.activeInHierarchy, expandable);
                break;
            default:
                return;
        }
    }

    /// <summary>
    /// Calls instantiate or creates something else if it's not a gameObject.
    /// </summary>
    /// <param name="somethingToCreate"></param>
    /// <returns></returns>
    GameObject Create(GameObject somethingToCreate)
    {
        if (!typeof(GameObject).Equals(typeof(UnityEngine.GameObject)))
        {
            //place generic way to create a T object
            //GameObject newObj = new GameObject();            
            Debug.Log(string.Format("{0} is not a GameObject!" , somethingToCreate));
        }

        UnityEngine.GameObject obj = (UnityEngine.GameObject)(object)somethingToCreate;

        return (GameObject)(object)Instantiate(obj);
    }


}
