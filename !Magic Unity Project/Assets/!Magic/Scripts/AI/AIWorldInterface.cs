using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWorldInterface : MonoBehaviour
{
    public static List<CoverGeometryData> coverLocations;

    void Awake()
    {
        coverLocations = new List<CoverGeometryData>();
    }
}
