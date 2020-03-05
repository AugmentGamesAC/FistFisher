using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableDefinition : ScriptableObject, ISpawnable
{
    #region ModelReferences
    [SerializeField]
    protected Mesh m_BaseModelRefence;
    [SerializeField]
    protected GameObject m_BasicCollectable;

    private GameObject m_thisObject;
    #endregion
    public GameObject Instatiate(MeshCollider m)
    {
        GameObject CollectRoot = ObjectPoolManager.Get(m_BasicCollectable);

        if (m == null || CollectRoot == null)
            return null;

        Transform pos = m.gameObject.transform;
        pos.position = BiomeInstance.FindValidPosition(m);
        pos.position = BiomeInstance.GetSeafloorPosition(pos.position);

        GameObject m_thisObject = Instantiate(CollectRoot, pos);
        return m_thisObject;
    }
}
