using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnable
{
    GameObject Spawn(MeshCollider m);
    GameObject Instantiate(MeshCollider m);
    GameObject Instantiate(MeshCollider m, Vector3 position, Quaternion rotation);
    float WeightedChance { get; }
    MeshCollider MeshOverRide { get; }
}
