using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject m_RespawnPoint;
    public GameObject m_PrefabObj;

    private List<GameObject> m_Spawnlist = new List<GameObject>();
    public int Maximum = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //removes any objects from the list type that have been destroyed. in this case it will remove the one test dummy each time it's destroyed.
        m_Spawnlist.RemoveAll(obj => (obj == null || obj.Equals(null)));
    }

    public void Spawn()
    {
        //Can add spawn sound here
        //Will Spawn on command until reached maximum amount of objects.
        if (m_Spawnlist.Count < Maximum)
        {
            GameObject obj = Instantiate(m_PrefabObj, m_RespawnPoint.transform.position, m_RespawnPoint.transform.rotation);
            m_Spawnlist.Add(obj);
        }
        //else error message or wrong beep sound.
    }
}
