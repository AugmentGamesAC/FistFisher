using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverGeometryData : MonoBehaviour
{
    public Transform coverPosition;
    public bool occupied = false;

    // Start is called before the first frame update
    void Start()
    {
        AIWorldInterface.coverLocations.Add(this);
    }
}
