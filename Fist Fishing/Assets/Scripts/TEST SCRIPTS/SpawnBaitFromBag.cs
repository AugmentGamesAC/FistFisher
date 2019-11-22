using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBaitFromBag : MonoBehaviour
{
    public GameObject m_baitPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (m_baitPrefab == null)
                return;
            GameObject bait = ObjectPoolManager.Get(m_baitPrefab);
            bait.GetComponent<Bait>().Init();
            bait.transform.position = gameObject.transform.position + gameObject.transform.forward * 5.0f;
        }
    }
}
