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
