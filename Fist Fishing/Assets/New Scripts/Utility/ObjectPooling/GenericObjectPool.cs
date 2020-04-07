using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericObjectPool : MonoBehaviour
{
    protected Queue<PoolingScript> m_Unused;
    public float LastUsed = 0.0f; 

    public int ObjectCount { get; private set; }
    public int Unused { get => m_Unused.Count; }

    public GameObject GetObject(GameObject preFab)
    {
        if (m_Unused.Count > 0)
            return m_Unused.Dequeue().gameObject;

        GameObject returnVal = GameObject.Instantiate(preFab);
        return HandleNew<GameObject>(returnVal);
    }
    public T GetObject<T>(GameObject preFab) where T: Component
    {
        if (m_Unused.Count > 0)
            return m_Unused.Dequeue().gameObject.GetComponent<T>();

        T returnVal;
        returnVal = GameObject.Instantiate(preFab).GetComponent<T>();
        return HandleNew<T>(returnVal.gameObject);
    }
    public T GetObject<T>(GameObject preFab, Vector3 position, Quaternion rotation) where T : Component
    {
        T returnVal;
        if (m_Unused.Count > 0)
        {
            returnVal = m_Unused.Dequeue().gameObject.GetComponent<T>();
            returnVal.gameObject.transform.SetPositionAndRotation(position, rotation);
            return returnVal;
        }
        returnVal = GameObject.Instantiate(preFab).GetComponent<T>();

        return HandleNew<T>(returnVal.gameObject);
    }

    public T HandleNew<T>(GameObject instatiatedObject)
    {
        ObjectCount++;
        if (instatiatedObject.GetComponent<PoolingScript>() == default)
            instatiatedObject.AddComponent<PoolingScript>().PoolInit(this);
        return instatiatedObject.GetComponent<T>();
    }

    public T GetObject<T>(ISpawnable spawnable, MeshCollider m) where T : Component
    {
        T returnVal;
        if (m_Unused.Count > 0)
            return m_Unused.Dequeue().gameObject.GetComponent<T>();

        returnVal = spawnable.Instantiate(m).GetComponent<T>();
        return HandleNew<T>(returnVal.gameObject);
    }
    public T GetObject<T>(ISpawnable spawnable, MeshCollider m, Vector3 position, Quaternion rotation) where T : Component
    {
        T returnVal;
        if (m_Unused.Count > 0)
        {
            returnVal = m_Unused.Dequeue().gameObject.GetComponent<T>();
            returnVal.gameObject.transform.SetPositionAndRotation(position, rotation);
            return returnVal;
        }

        returnVal = spawnable.Instantiate(m, position, rotation).GetComponent<T>();
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
}
