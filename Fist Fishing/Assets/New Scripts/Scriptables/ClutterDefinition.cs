using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClutterDefinition : ScriptableObject, ISpawnable
{
    #region ModelReferences
    [SerializeField]
    protected Mesh m_BaseModelRefence;
    [SerializeField]
    protected GameObject m_BasicClutter;
    #endregion
    public GameObject Instatiate(MeshCollider m)
    {

        GameObject ClutterRoot = ObjectPoolManager.Get(m_BasicClutter);

        if (m == null || ClutterRoot == null)
            return null;

        Transform pos = m.gameObject.transform;
        pos.position = BiomeInstance.FindValidPosition(m);
        pos.position = BiomeInstance.GetSeafloorPosition(pos.position);

        GameObject o = Instantiate(ClutterRoot, pos);
        return o;
    }
}
