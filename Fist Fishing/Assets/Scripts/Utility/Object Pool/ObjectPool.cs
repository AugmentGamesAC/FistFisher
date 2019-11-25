using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class ObjectPool
{
    public Type PoolType;
    public string ObjectName = "";
    public float LastUsed = 0.0f;

    public int ObjectCount
    {
        get{ return m_PoolGameObjects.Count; }
    }

    public GameObject GetObject(GameObject prefab)
    {
        GameObject retrievedObject = null;
        var disabledObjects = m_PoolGameObjects.Where(x => x.activeSelf == false);

        if (disabledObjects.Any())
        {
            retrievedObject = disabledObjects.First();
        }
        else
        {
            retrievedObject = CreateNewObject(prefab);
        }

        retrievedObject.gameObject.SetActive(true);
        LastUsed = Time.timeSinceLevelLoad;

        return retrievedObject;
    }

    public GameObject CreateNewObject(GameObject prefab)
    {
        GameObject returnObj = ObjectPoolManager.ObjectCreator(prefab);
        m_PoolGameObjects.Add(returnObj);
        return returnObj;
    }

    public void RemoveDisableObjects() //quarantine
    {
        var DisabledObjects = m_PoolGameObjects.Where(poolObject => poolObject.gameObject.activeSelf == false).ToList();
        m_PoolGameObjects = m_PoolGameObjects.Where(poolObject => poolObject.gameObject.activeSelf == true).ToList();
        if(DisabledObjects!=null)
        {
            foreach(GameObject poolObject in DisabledObjects)
            {
                GameObject.Destroy(poolObject);
            }
        }
        LastUsed = Time.timeSinceLevelLoad;
    }


    List<GameObject> m_PoolGameObjects = new List<GameObject>();


}
