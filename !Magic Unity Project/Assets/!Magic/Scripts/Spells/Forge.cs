using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Forge : BasicSlotManager
{
    [SerializeField]
    protected int m_GestureId;
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

    private SpellDescription AttemptSpellFormation()
    {
        SpellDescription spellDescription = new SpellDescription(0, 0, 0, 0); //sneaky hack makes them emmpty flags

        //For now simple combination logic, flags are ored together, otherwise last entry wins
        foreach (ASlot slotdata in m_Slots)
        {
            if ((slotdata == null) || (slotdata.Slotted == null))
                continue;
            DescriptionCrystal dC = slotdata.Slotted.gameObject.GetComponentInParent<DescriptionCrystal>();
            if (dC == null)
                continue;

            spellDescription.Aiming |= dC.SpellDescription.Aiming;
            spellDescription.Effect = (dC.SpellDescription.Effect > 0) ? dC.SpellDescription.Effect : spellDescription.Effect;
            spellDescription.Shape = (dC.SpellDescription.Shape > 0) ? dC.SpellDescription.Shape : spellDescription.Shape;
            spellDescription.Usage = (dC.SpellDescription.Usage > 0) ? dC.SpellDescription.Usage : spellDescription.Usage;
        }
        //if any of the properties has no value its not a valid spell
        if (
                (spellDescription.Aiming == 0) ||
                (spellDescription.Effect == 0) ||
                (spellDescription.Shape == 0) ||
                (spellDescription.Usage == 0)
            )
            return null;
        // valid spell go nuts
        return spellDescription;
    }
    /// <summary>
    /// Creates a new chip and Has the BasicSlotManager 
    /// </summary>
    /// <param name="validatedSpell">spell that is validated by AttempSpellFormation</param>
    private void GenerateChip(SpellDescription validatedSpell) {
        GameObject newChip = Instantiate(SpellChip, new Vector3(0, 0, 0), Quaternion.identity);
        SpellChip newSpellChip = newChip.GetComponent<SpellChip>();

        if (newSpellChip == null)
            return;

        m_GestureId = Valve.VR.InteractionSystem.Player.instance.gameObject.GetComponentInChildren<Weave>().m_AllGestureIDs.Last<int>();
        newSpellChip.GestureId = m_GestureId;//change ID to whatever you get from airsig
    }
}
