using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// ProbabilitySpawn specific to Clutter
/// have to make the specific class for unity to accept it
/// had to be migrated to its own file because scriptable objects didn't like multiple classes in a single file
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "New Prob Spawn Clutter Object", menuName = "Clutter/Clutter ProbabilitySpawn")]
public class ProbabilitySpawnClutter : ProbabilitySpawn<ClutterDefinition, ProbabilitySpawnClutter> { }