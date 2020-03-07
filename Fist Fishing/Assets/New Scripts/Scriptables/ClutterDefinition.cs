using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Clutter Object", menuName = "Clutter/Clutter Definition")]
public class ClutterDefinition : ScriptableObject, ISpawnable
{
    #region ModelReferences
    [SerializeField]
    protected Mesh m_BaseModelReference;
    [SerializeField]
    protected GameObject m_BasicClutter;

    private GameObject m_thisObject = null;
    /// <summary>
    /// Always throws exception use ProbablitySpawn<T,V> instead
    /// </summary>
    public float WeightedChance => throw new System.NotImplementedException();
    /// <summary>
    /// Always throws exception use ProbablitySpawn<T,V> instead
    /// </summary>
    public MeshCollider MeshOverRide => throw new System.NotImplementedException();
    #endregion
    public GameObject Instatiate(MeshCollider m)
    {
        if (m == null)
            return null;


        m_thisObject = ObjectPoolManager.Get(m_BasicClutter);

        Vector3 pos = BiomeInstance.FindValidPosition(m);
        m_thisObject.gameObject.transform.position = BiomeInstance.GetSeafloorPosition(pos);
        if (m_thisObject.gameObject.transform.position == Vector3.zero)
        {
            pos.y = m.bounds.max.y;
            m_thisObject.gameObject.transform.position = BiomeInstance.GetSeafloorPosition(pos);
        }

        return m_thisObject;
    }

    public bool Despawn()
    {
        if (m_thisObject == default)
            return false;
        m_thisObject.SetActive(false);
        return m_thisObject.activeSelf;
    }
}
