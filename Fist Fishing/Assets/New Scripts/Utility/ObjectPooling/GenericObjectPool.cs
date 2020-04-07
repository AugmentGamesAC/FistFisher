using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GenericObjectPool : MonoBehaviour
{
    public Type PoolType;
    public string ObjectName = "";

    protected Queue<PoolingScript> m_Unused;
    public float LastUsed = 0.0f; 

    public int ObjectCount { get; private set; }
    public int Unused { get => m_Unused.Count; }

    protected GameObject FromPool()
    {
        m_Unused.Peek().gameObject.SetActive(true);
        return m_Unused.Dequeue().gameObject;
    }

    public T HandleNew<T>(GameObject instatiatedObject) where T: Component 
    {
        ObjectCount++;
        if (instatiatedObject.GetComponent<PoolingScript>() == default)
            instatiatedObject.AddComponent<PoolingScript>().PoolInit(this);
        return instatiatedObject.GetComponent<T>();
    }
    public GameObject HandleNew(GameObject instatiatedObject)
    {
        ObjectCount++;
        if (instatiatedObject.GetComponent<PoolingScript>() == default)
            instatiatedObject.AddComponent<PoolingScript>().PoolInit(this);
        return instatiatedObject;
    }


    public GameObject GetObject(GameObject preFab)
    {
        if (m_Unused.Count > 0)
            return FromPool();

        GameObject returnVal = Instantiate(preFab);
        return HandleNew(returnVal);
    }

    public GameObject GetObject(GameObject preFab, Vector3 position, Quaternion rotation) 
    {
        GameObject returnVal;
        if (m_Unused.Count > 0)
        {
            returnVal = FromPool();
            returnVal.transform.SetPositionAndRotation(position, rotation);
            return returnVal;
        }
        returnVal = Instantiate(preFab);
        return HandleNew(returnVal);
    }


    public T GetObject<T>(GameObject preFab) where T: Component
    {
        if (m_Unused.Count > 0)
            return FromPool().GetComponent<T>();

        T returnVal;
        returnVal = GameObject.Instantiate(preFab).GetComponent<T>();
        return HandleNew<T>(returnVal.gameObject);
    }

    public T GetObject<T>(GameObject preFab, Vector3 position, Quaternion rotation) where T : Component
    {
        T returnVal;
        if (m_Unused.Count > 0)
        {
            returnVal = FromPool().GetComponent<T>();
            returnVal.gameObject.transform.SetPositionAndRotation(position, rotation);
            return returnVal;
        }
        returnVal = GameObject.Instantiate(preFab).GetComponent<T>();

        return HandleNew<T>(returnVal.gameObject);
    }

    public T GetSpawn<T>(ISpawnable spawnable) where T : Component
    {
        T returnVal;
        if (m_Unused.Count > 0)
            return FromPool().GetComponent<T>();

        returnVal = spawnable.Instantiate().GetComponent<T>();
        return HandleNew<T>(returnVal.gameObject);
    }

    public T GetSpawn<T>(ISpawnable spawnable, Vector3 position, Quaternion rotation) where T : Component
    {
        T returnVal;
        if (m_Unused.Count > 0)
        {
            returnVal = FromPool().GetComponent<T>();
            returnVal.gameObject.transform.SetPositionAndRotation(position, rotation);
            return returnVal;
        }

        returnVal = spawnable.Instantiate(position, rotation).GetComponent<T>();
        return HandleNew<T>(returnVal.gameObject);
    }

    public void Deactivated(PoolingScript deactivated)
    {
        m_Unused.Enqueue(deactivated);
    }

    public void Destroyed(PoolingScript destroyed)
    {
        ObjectCount--;
    }

    public void RemoveDisableObjects() //quarantine
    {
        while (m_Unused.Count > 0)
            if (m_Unused.Peek() == default)
                m_Unused.Dequeue();
            else
                Destroy(m_Unused.Dequeue().gameObject);
            
    }
}
