using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Coral Bud Object", menuName = "Coral/Coral Bud Definition")]
public class CoralBudDefinition : ScriptableObject
{
    [SerializeField]
    protected GameObject m_modelOfBud;
    [SerializeField]
    protected Material m_materialOverride;
    [SerializeField]
    protected AItem m_coralItemData;
    public AItem ItemData => m_coralItemData;

    public GameObject Instantiate(Vector3 pos)
    {
        /*Debug.Log(m_modelOfBud);
        Debug.Log(m_materialOverride);
        Debug.Log(m_coralItemData);*/
        if (m_modelOfBud == default)
            return null;
        GameObject o = ObjectPoolManager.Get(m_modelOfBud, pos, Quaternion.identity);
        if (o == default)
            return null;
        if (m_materialOverride != null)
            o.GetComponent<Renderer>().material = m_materialOverride;
        CoralBudNew c = o.AddComponent<CoralBudNew>();
        c.SetDefinition(this);
        return o;
    }
}
