using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Forge : BasicSlotManager
{
    [SerializeField]
    protected int GestureId;
    /// <summary>
    /// prefab to be used to generate new chip
    /// </summary>
    [SerializeField]
    protected GameObject SpellChip;
    [SerializeField]
    protected BasicSlot OutputLocation;

    /// <summary>
    /// Creates new slot at the OutputLocation;
    /// </summary>
    public void GenerateChip() {
        SpellDescription newSpell = AttemptSpellFormation();
        if (newSpell == null)
            return;

        GenerateChip(newSpell);
    }

    private SpellDescription AttemptSpellFormation() { throw new System.NotImplementedException(); }
    /// <summary>
    /// Creates a new chip and Has the BasicSlotManager 
    /// </summary>
    /// <param name="validatedSpell">spell that is validated by AttempSpellFormation</param>
    private void GenerateChip(SpellDescription validatedSpell) {
        throw new System.NotImplementedException();
    }
}
