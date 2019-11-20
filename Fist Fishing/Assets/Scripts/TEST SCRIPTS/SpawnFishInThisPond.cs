using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFishInThisPond : MonoBehaviour
{

    [SerializeField]
    public PondManager m_pond;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) //force-spawn fish. remove this.
        {
            foreach (var g in m_pond.Spawners)
            {
                g.Key.SpawnFish();
            }
        }
    }
}
