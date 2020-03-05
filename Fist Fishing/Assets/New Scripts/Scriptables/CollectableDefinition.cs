using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Collectables Object", menuName = "Collectables")]
public class CollectableDefinition : ScriptableObject, ISpawnable
{
    #region ModelReferences
    [SerializeField]
    protected Mesh m_BaseModelRefence;
    [SerializeField]
    protected GameObject m_BasicCollectable;

    private GameObject m_thisObject = null;
    #endregion
    public GameObject Instatiate(MeshCollider m)
    {
        if (m == null)
            return null;

        Transform transform = m.gameObject.transform;
        m_thisObject = Instantiate(m_BasicCollectable, BiomeInstance.GetSeafloorPosition(BiomeInstance.FindValidPosition(m)), transform.rotation, transform);

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
