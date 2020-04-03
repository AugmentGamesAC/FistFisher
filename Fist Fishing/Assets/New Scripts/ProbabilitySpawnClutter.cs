using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "New Prob Spawn Clutter Object", menuName = "Clutter/Clutter ProbabilitySpawn")]
public class ProbabilitySpawnClutter : ProbabilitySpawn<ClutterDefinition, ProbabilitySpawnClutter> { }