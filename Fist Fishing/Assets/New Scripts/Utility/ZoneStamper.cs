using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BiomeInstance))]
public class ZoneStamper : MonoBehaviour
{
    public string NewBiomeName;
    protected BiomeInstance m_biomeInstance;
    public BiomeInstance Biome
    {
        get
        {
            if (m_biomeInstance == default)
                m_biomeInstance = GetComponent<BiomeInstance>();
            return m_biomeInstance;
        }
    }

    [ContextMenu("CreateNewBiomeDefinition")]
    public void CloneBiomeDefiniton()
    {
#if UNITY_EDITOR

        m_biomeInstance.Definiton = Biome.Definiton.CloneSelf(NewBiomeName);
        string testing = UnityEditor.AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/Biomes/" + NewBiomeName + ".asset");
        UnityEditor.AssetDatabase.CreateAsset
        (
            m_biomeInstance.Definiton,
            testing
        );
        UnityEditor.AssetDatabase.SaveAssets();
#endif
    }

    [ContextMenu("StampZone")]
    public void DuplicateBiome()
    {
#if UNITY_EDITOR
        var myclone = GameObject.Instantiate(this);
        myclone.name = Biome.Definiton.Name;
        GameObject.DestroyImmediate(myclone.GetComponent<ZoneStamper>());
        
#endif
    }

    [ContextMenu("StampAndNewBiome")]
    public void CreateBiomeObject()
    {
#if UNITY_EDITOR
        CloneBiomeDefiniton();
        DuplicateBiome();
#endif
    }
}