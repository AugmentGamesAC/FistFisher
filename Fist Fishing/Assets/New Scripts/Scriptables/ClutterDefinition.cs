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

    private GameObject m_thisObject;
    #endregion
    public GameObject Instatiate(MeshCollider m)
    {

        GameObject ClutterRoot = ObjectPoolManager.Get(m_BasicClutter);

        if (m == null || ClutterRoot == null)
            return null;

        Transform pos = m.gameObject.transform;
        pos.position = BiomeInstance.FindValidPosition(m);
        pos.position = BiomeInstance.GetSeafloorPosition(pos.position);

        GameObject m_thisObject = Instantiate(ClutterRoot, pos);
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
