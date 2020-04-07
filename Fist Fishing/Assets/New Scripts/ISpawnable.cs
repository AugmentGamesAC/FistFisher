using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface ISpawnable
{
    Type SpawnableType { get; }
    String SpawnableName { get; }
    GameObject Spawn(MeshCollider m);
    GameObject Instantiate();
    GameObject Instantiate(Vector3 position, Quaternion rotation);
    float WeightedChance { get; }
    MeshCollider MeshOverRide { get; }
}
