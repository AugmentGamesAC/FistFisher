using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// ProbabilitySpawn specific to Collectbles
/// have to make the specific class for unity to accept it
/// had to be migrated to its own file because scriptable objects didn't like multiple classes in a single file
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "New Prob Spawn Collectable Object", menuName = "Collectables/Collectable ProbabilitySpawn")]
public class ProbabilitySpawnCollectable : ProbabilitySpawn<CollectableDefinition, ProbabilitySpawnCollectable> { }