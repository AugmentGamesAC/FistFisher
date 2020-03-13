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
        m_biomeInstance.Definiton = Biome.Definiton.CloneSelf(NewBiomeName);
        string testing = UnityEditor.AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/Biomes/" + NewBiomeName + ".asset");
        UnityEditor.AssetDatabase.CreateAsset
        (
            m_biomeInstance.Definiton,
            testing
        );
        UnityEditor.AssetDatabase.SaveAssets();
    }

    [ContextMenu("StampZone")]
    public void DuplicateBiome()
    {
        GameObject.Destroy(GameObject.Instantiate(this).GetComponent<ZoneStamper>().GetComponent<ZoneStamper>());
    }

    [ContextMenu("NewBiomeandStamp")]
    public void CreateBiomeObject()
    {
        CloneBiomeDefiniton();
        DuplicateBiome();
    }


}