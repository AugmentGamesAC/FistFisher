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
    #endregion
    public GameObject Instatiate(MeshCollider m)
    {
        if (m == null)
            return null;


        m_thisObject = ObjectPoolManager.Get(m_BasicClutter);

        m_thisObject.gameObject.transform.position = BiomeInstance.GetSeafloorPosition(BiomeInstance.FindValidPosition(m));
        //Transform transform = m.gameObject.transform;
        //m_thisObject = Instantiate(m_BasicClutter, BiomeInstance.GetSeafloorPosition(BiomeInstance.FindValidPosition(m)), transform.rotation, transform);

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
