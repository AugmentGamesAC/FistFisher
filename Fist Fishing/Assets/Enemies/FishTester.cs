using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTester : MonoBehaviour
{
    [SerializeField]
    FishDefintion fishDefintion;
    [SerializeField]
    MeshCollider targetMesh;
    // Start is called before the first frame update
    void Start()
    {
        fishDefintion.Spawn(targetMesh);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
