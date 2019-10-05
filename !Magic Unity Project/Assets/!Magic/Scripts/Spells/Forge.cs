using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Forge : BasicSlotManager
{
    protected int GestureId;
    /// <summary>
    /// prefab to be used to generate new chip
    /// </summary>
    protected GameObject NotMagicChip;
    protected BasicSlotManager OutputLocation;

    public void GenerateChip() { throw new System.NotImplementedException(); }

    private NotMagicDescription AttemptSpellFormation() { throw new System.NotImplementedException(); }
    private void GenerateChip(NotMagicDescription validatedSpell) { throw new System.NotImplementedException();  }
}
