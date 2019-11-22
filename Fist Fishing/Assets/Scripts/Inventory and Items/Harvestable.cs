using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HarvestableType
{
    NotSet, 
    DeadFish, 
    Coral1,
    Coral2,
}
public class Harvestable : MonoBehaviour
{
    public HarvestableSpawner m_spawner;
    public TargetController m_targetController;


    //[SerializeField]
    //protected HarvestableSpawner m_spawner;
    //public HarvestableSpawner Spawner { get { return m_spawner; } }

    [SerializeField]
    protected HarvestableType m_harvestableType;
    public HarvestableType HarvestableType { get { return m_harvestableType; } }


    // Start is called before the first frame update
    void Start()
    {
        if (HarvestableType == HarvestableType.NotSet)
            Destroy(gameObject);

    }


    void LateUpdate()
    {

    }

}
