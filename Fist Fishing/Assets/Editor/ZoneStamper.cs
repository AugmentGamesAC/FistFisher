using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BiomeInstance))]
public class ZoneStamper : MonoBehaviour
{
    public string NewBiomeName;
    protected BiomeInstance m_biomeInstance;
    
    [ContextMenu("DuplicateAndNameBiome")]
    public void CloneBiome()
    {

    }

    [ContextMenu("DuplicateAndShareBiome")]
    public void CloneBiome()
    {

    }
}
