using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTester : MonoBehaviour
{
    [SerializeField]
    FishDefintion fishDefintion;
    [SerializeField]
    MeshCollider targetMesh;



    [SerializeField]
    float radius = 5.0f;

    [SerializeField]
    float secondradius = Mathf.Infinity;



    // Start is called before the first frame update
    void Start()
    {
        //fishDefintion.Spawn(targetMesh);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Debug.Log(!BiomeInstance.SpherecastToEnsureItHasRoom(gameObject.transform.position, radius, out hit));
        //Physics.SphereCast(gameObject.transform.position, radius, Vector3.down, out hit, secondradius, ~LayerMask.GetMask("Player", "Ignore Raycast", "Water", "BoatMapOnly"));
        Debug.Log(hit.point);

    }
}
