using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
/// <summary>
/// this is the Guantlet's slot relatedCode
/// Guantlet behavoir in PlayerSpellSystem
/// </summary>
public class SpellList : BasicSlotManager
{
    public int GetSpellIndex(int spellRef)
    {
        int result = -1;
        if (m_Slots.Count == 0)
            return result;
        foreach(ASlot slot in m_Slots)
        {
            result++;
            SpellChip spellChip = slot.Slotted.GetComponent<SpellChip>();
            if (spellChip == null)
                continue;
            if (spellChip.GestureId == spellRef)
                return result;
        }
        return -1;
    } 
    public Spell ReadSpell(int spellIndex)
    {
        if ((spellIndex < 0) || (spellIndex > m_Slots.Count))
            return default(Spell);
        return m_Slots[spellIndex].Slotted.GetComponent<SpellChip>().SpellData;
    }

    public delegate void SpellListUpdated(IEnumerable<int> newSpellList);
    public event SpellListUpdated OnSpellListUpdated;


    /// <summary>
    /// This is used to notify when the spell list has possibly changed, it trigger the event based on the 
    /// slot, no futher details looked at at this point
    /// </summary>
    /// <param name="changedSlot">The slot that called this function</param>
    public override void SlotUpdate(ASlot changedSlot)
    {
        //if event list isn't empty invoke the event sending the list of spells now avaiblable
        OnSpellListUpdated?.Invoke
        (
            //Linq statement applied to the list of slots, only dealing with slots WHERE chips are in them
            m_Slots.Where(slot => ((slot.Slotted != null) && ((slot.Slotted.GetComponent<SpellChip>() != null))))
                //and returning the list of gesture ids
                .Select(slot => slot.Slotted.GetComponent<SpellChip>().GestureId)
        );
    }
}
