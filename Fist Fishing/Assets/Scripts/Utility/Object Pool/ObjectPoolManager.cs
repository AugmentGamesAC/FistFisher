using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPoolManager : MonoBehaviour
{
    static List<ObjectPool> m_Pools = new List<ObjectPool>();

    public bool CleanupStaleObjects = true;
    public int SecondsStaleBeforeCleanup = 500;
    float m_SecondsPoolRefreshRate;


    private IEnumerator CleanPools()
    {
        while (true)
        {
            List<ObjectPool> poolsToDelete = new List<ObjectPool>();
            foreach(ObjectPool pool in m_Pools)
            {
                if(pool.LastUsed + SecondsStaleBeforeCleanup < Time.timeSinceLevelLoad)
                {
                    pool.RemoveDisableObjects();
                    if (pool.ObjectCount < 1)
                    {
                        poolsToDelete.Add(pool);
                    }
                }
            }
            poolsToDelete.ForEach(x => m_Pools.Remove(x));
            yield return new WaitForSeconds(m_SecondsPoolRefreshRate);
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        m_SecondsPoolRefreshRate = 15;
        if (CleanupStaleObjects)
        {
            StartCoroutine(CleanPools());
        }
    }

    private static ObjectPool CreateNewPool(GameObject prefab)
    {
        ObjectPool pool = new ObjectPool();
        pool.PoolType = typeof(GameObject);
        pool.ObjectName = prefab.name;
        m_Pools.Add(pool);

        return pool;
    }

    public static ObjectPool GetPool(string name, Type type)
    {
        var matchingPools = m_Pools.Where(x => x.ObjectName == name && x.PoolType == type);

        if (matchingPools.Any())
        {
            return matchingPools.First();
        }
        return null;
    }

    public static GameObject Get(GameObject prefab)
    {
        ObjectPool objPool = GetPool(prefab.name, typeof(GameObject));
        if(objPool==null)
        {
            objPool = CreateNewPool(prefab);
        }

        GameObject newobj = objPool.GetObject(prefab);
        newobj.SetActive(true);
        return newobj;
    }

    public static GameObject Get(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        GameObject newObject = Get(prefab);
        newObject.transform.position = pos;
        newObject.transform.rotation = rot;

        return newObject;
    }

    public static GameObject ObjectCreator(GameObject prefab)
    {
        GameObject newObj = (GameObject)Instantiate(prefab);
        return newObj;
    }
































    // Update is called once per frame
    /*void Update()
    {
        
    }*/
}
