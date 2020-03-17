using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CreateAsset : MonoBehaviour
{
    [SerializeField]
    protected string m_locationOfAsset = "";
    [SerializeField]
    protected string m_nameOfAsset = "";

    [SerializeField]
    protected GameObject m_FBXObject;

    private void Start()
    {
        SpawnFromStringResources();
    }


    [ContextMenu("Spawn Set Object From Object")]
    public void SpawnFromObject()
    {
        GameObject o = Instantiate(m_FBXObject, transform.position, transform.rotation, transform);
        //o.transform.parent = null;
    }

    //note for me for future: Resource bundle stuff didn't work here as it actually needs to be loading a bundle - which is created from objects in acene
    /*[ContextMenu("Spawn Set Object From String (not resources)")]
    public void SpawnFromString()
    {
        string s = m_locationOfAsset + m_nameOfAsset;
        AssetBundle a = AssetBundle.LoadFromFile(s);
        Debug.Log(s);
        Debug.Log(a);
        Instantiate(a);
    }*/

    [ContextMenu("Spawn Set Object From String (resources)")]
    public void SpawnFromStringResources()
    {
        GameObject o = Instantiate(Resources.Load<GameObject>(m_nameOfAsset));
        //o.transform.parent = null;

    }
}
