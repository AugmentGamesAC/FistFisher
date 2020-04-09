using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// ProbabilitySpawn specific to Fish
/// have to make the specific class for unity to accept it
/// had to be migrated to its own file because scriptable objects didn't like multiple classes in a single file
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "New Prob Spawn Fish Object", menuName = "Fish/Fish ProbabilitySpawn")]
public class ProbabilitySpawnFish : ProbabilitySpawn<FishDefintion, ProbabilitySpawnFish> { }
