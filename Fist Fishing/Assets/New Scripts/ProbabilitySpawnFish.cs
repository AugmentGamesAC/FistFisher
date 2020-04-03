using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "New Prob Spawn Fish Object", menuName = "Fish/Fish ProbabilitySpawn")]
public class ProbabilitySpawnFish : ProbabilitySpawn<FishDefintion, ProbabilitySpawnFish> { }
