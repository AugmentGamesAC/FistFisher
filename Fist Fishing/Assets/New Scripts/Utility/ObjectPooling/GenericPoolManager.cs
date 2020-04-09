using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class GenericPoolManager : MonoBehaviour
{
    static Dictionary<object, GenericObjectPool> m_Pools = new Dictionary<object, GenericObjectPool>();

    public bool CleanupStaleObjects = true;
    public int SecondsStaleBeforeCleanup = 500;
    float m_SecondsPoolRefreshRate;

    private IEnumerator CleanPools()
    {
        while (true)
        {
            List<GenericObjectPool> poolsToDelete = new List<GenericObjectPool>();
            foreach (GenericObjectPool pool in m_Pools.Values)
            {
                if (pool.LastUsed + SecondsStaleBeforeCleanup < Time.timeSinceLevelLoad)
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

    private static GenericObjectPool CreateNewPool(GameObject prefab)
    {
        GenericObjectPool pool = new GenericObjectPool();
        pool.PoolType = typeof(GameObject);
        pool.ObjectName = prefab.name;
        m_Pools.Add(prefab, pool);

        return pool;
    }

    public static GenericObjectPool GetPool(string name, Type type)
    {
        var matchingPools = m_Pools.Values.Where(x => x.ObjectName == name && x.PoolType == type);

        if (matchingPools.Any())
        {
            return matchingPools.First();
        }
        return null;
    }

    public static GenericObjectPool GetPool(ISpawnable spawnable) { return GetPool((spawnable as Component).gameObject); }
    public static GenericObjectPool GetPool(GameObject prefab)
    {
        GenericObjectPool returnVal = default;
        if (!m_Pools.TryGetValue(prefab, out returnVal))
            returnVal = CreateNewPool(prefab); 
        return returnVal;
    }

    public static GameObject Get(GameObject prefab)
    {
        GenericObjectPool objPool = GetPool(prefab);
        return objPool.GetObject(prefab);
    }
    public static T Get<T>(GameObject prefab) where T : Component
    {
        GenericObjectPool objPool = GetPool(prefab);
        return objPool.GetObject<T>(prefab);
    }

    public static T Get<T>(ISpawnable spawnable, MeshCollider m) where T: Component
    {
        GenericObjectPool objPool = GetPool(spawnable);
        return objPool.GetSpawn<T>(spawnable, m);
    }

    public static GameObject Get(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        GenericObjectPool objPool = GetPool(prefab);
        return objPool.GetObject(prefab,pos,rot);
    }

    public static T Get<T>(GameObject prefab, Vector3 pos, Quaternion rot) where T: Component
    {
        GenericObjectPool objPool = GetPool(prefab);
        return objPool.GetObject<T>(prefab, pos, rot);
    }

    public static T Get<T>(ISpawnable prefab,MeshCollider m, Vector3 pos, Quaternion rot) where T : Component
    {
        GenericObjectPool objPool = GetPool(prefab);
        return objPool.GetSpawn<T>(prefab,m, pos, rot);
    }
}
