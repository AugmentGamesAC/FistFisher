using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FishGatheringQuest : GatheringQuest<FishDefintion>
{
    public FishGatheringQuest(QuestDefinition Def, FishDefintion fish) : base(Def, fish) { }
    public FishGatheringQuest(FishDefintion fish) : base(fish) { }

    public FishGatheringQuest() : base() { }
}
