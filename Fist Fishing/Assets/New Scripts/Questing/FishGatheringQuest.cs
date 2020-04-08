using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// gathering quests specifically for fish
/// </summary>
[System.Serializable]
public class FishGatheringQuest : GatheringQuest<FishDefintion>
{
    public FishGatheringQuest(QuestDefinition Def, FishDefintion fish) : base(Def, fish) { }
    public FishGatheringQuest(FishDefintion fish) : base(fish) { }
    public FishGatheringQuest() : base() { }
}
