using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "New Prob Spawn Collectable Object", menuName = "Collectables/Collectable ProbabilitySpawn")]
public class ProbabilitySpawnCollectable : ProbabilitySpawn<CollectableDefinition, ProbabilitySpawnCollectable> { }