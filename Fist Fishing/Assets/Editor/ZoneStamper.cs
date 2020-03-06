using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(BiomeInstance))]
public class ZoneStamper : MonoBehaviour
{
    public string NewBiomeName;
    protected BiomeInstance m_biomeInstance;
    
    [ContextMenu("CreateNewBiomeDefinition")]
    public void CloneBiomeDefiniton()
    {
        m_biomeInstance.Definiton = m_biomeInstance.Definiton.CloneSelf(NewBiomeName);
        AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/Biomes/" + NewBiomeName);
        AssetDatabase.CreateAsset(m_biomeInstance.Definiton, "Assets/NewScripableObject.asset");
        AssetDatabase.SaveAssets();
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
