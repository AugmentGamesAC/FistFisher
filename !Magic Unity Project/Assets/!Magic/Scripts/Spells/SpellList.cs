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

    public override void SlotUpdate(ASlot changedSlot)
    {
        OnSpellListUpdated?.Invoke
        (
            m_Slots.Where(slot => ((slot.Slotted != null) && ((slot.Slotted.GetComponent<SpellChip>() != null))))
                .Select(slot => slot.Slotted.GetComponent<SpellChip>().GestureId)
        );
    }
}
