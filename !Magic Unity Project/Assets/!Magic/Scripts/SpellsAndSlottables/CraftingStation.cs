
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CraftingStation : MonoBehaviour, ISlot
{
    public GameObject m_SpellCrystalPrefab;
    public int m_Spell_Id;


    public BasicSlot[] _slots;
    public GameObject DeadRef;

    public GauntletAiming m_AimingModule;

    public ISlot[] m_SpellDescriptorSlots;
    public ISlot m_CreationSlot;
    #region SlotManageMent
    private ISlot m_LastTargetedSlot;
    public ISlotable m_slotted
    {
        get { return m_LastTargetedSlot.m_slotted; }
        set { m_LastTargetedSlot.m_slotted = value; }
    }

    public bool Accept(ISlotable slottingObject)
    {
        if (!CanAccept(slottingObject))
            return false;

        m_slotted = slottingObject;
        return true;
    }

    public bool CanAccept(ISlotable slotable)
    {
        bool couldAccept = ((slotable.m_slotType & SlotTypes.IsDescriptionCrystal) > 0);

        if (!couldAccept)
            return false;

        ResolveSlottingIndex(slotable);
        return true;
    }

    public void ToggleHighlighting()
    {
        if (m_LastTargetedSlot == null)
            return;
        m_LastTargetedSlot.ToggleHighlighting();
    }

    private void ResolveSlottingIndex(ISlotable slottable)
    {
        if ((m_SpellDescriptorSlots == null))
            return;

        Vector3 thisLocation = gameObject.transform.position;
        GameObject closestSlot = null;
        float closestDist = float.MaxValue;


        foreach (ISlot hit in m_SpellDescriptorSlots)
        {
          
            //checks to see if it's closest
            float dist = Vector3.SqrMagnitude(thisLocation - hit.gameObject.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestSlot = hit.gameObject;
            }
        }

        if (closestSlot != null)
            return;

    }
    #endregion SlotManageMent

    public void SpawnCrystal()
    {
        SpellDescription newSpell = IsValidSpell();
        if (newSpell == null)
            return;

        CraftAndPlace(newSpell);
    }

    private void CraftAndPlace(SpellDescription newSpellDis)
    {
        GameObject newSpellPrism = Instantiate(m_SpellCrystalPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        SlottableSpellPrism spellPrismScript = newSpellPrism.GetComponent<SlottableSpellPrism>();

        m_Spell_Id = Valve.VR.InteractionSystem.Player.instance.gameObject.GetComponentInChildren<Weave>().m_AllGestureIDs.Last<int>();
        spellPrismScript.SetSpellID(m_Spell_Id);//change ID to whatever you get from airsig
        spellPrismScript.m_spell = new Spell(newSpellDis,null);
        spellPrismScript.m_spell.m_AimPoint = m_AimingModule;
        spellPrismScript.m_spell.m_spellOwner = Valve.VR.InteractionSystem.Player.instance.gameObject.GetComponent<IMagicUser>();
        spellPrismScript.m_spell.m_SpellManager = Valve.VR.InteractionSystem.Player.instance.gameObject.GetComponent<SpellManager>();

        //TODO: rewrite spawn location
        ///m_CreationSlot.Accept(spellPrismScript);
        spellPrismScript.transform.position = DeadRef.transform.position;
        Material mat = newSpellPrism.GetComponent<Renderer>().material;
        Color col = new Color();
        Color col2 = new Color();
        if (spellPrismScript.m_spell.m_elementType == Spell.Elements.Fire)
            col = new Color(1f, 0.75f, 0f);
        if (spellPrismScript.m_spell.m_elementType == Spell.Elements.Ice)
            col = new Color(0.4f, 0.4f, 1.0f);
        if (spellPrismScript.m_spell.m_elementType == Spell.Elements.Lightning)
            col = new Color(1f, 1f, 0.2f);

        if (spellPrismScript.m_spell.m_Start.m_shape == SpellDescription.Shape.Cone)
            col2 = new Color(1f, 0.1f, 1f);
        if (spellPrismScript.m_spell.m_Start.m_shape == SpellDescription.Shape.Cube)
            col2 = new Color(1f, 0.2f, 0.2f);
        if (spellPrismScript.m_spell.m_Start.m_shape == SpellDescription.Shape.Sphere)
            col2 = new Color(0.2f, 1f, 0.2f);
        mat.color = col2;
        mat.SetColor("_EmissionColor", col);
        newSpellPrism.GetComponent<Renderer>().material = mat;
    }

    private SpellDescription IsValidSpell()
    {
        SpellDescription spellDescription = new SpellDescription(0, 0, 0, 0); //sneaky hack makes them emmpty flags

        //For now simple combination logic, flags are ored together, otherwise last entry wins
        foreach(ISlot slotdata in _slots)
        {
            if ((slotdata == null)||(slotdata.m_slotted == null))
                continue;
            DescriptionCrystal dC = slotdata.m_slotted.gameObject.GetComponentInParent<DescriptionCrystal>();
            if (dC == null)
                continue;

            spellDescription.m_aiming |= dC.m_Aiming;
            spellDescription.m_effect = (dC.m_Effect > 0) ? dC.m_Effect : spellDescription.m_effect;
            spellDescription.m_shape = (dC.m_Shape > 0) ? dC.m_Shape : spellDescription.m_shape;
            spellDescription.m_usage = (dC.m_Usage > 0) ? dC.m_Usage : spellDescription.m_usage;
        }
        //if any of the properties has no value its not a valid spell
        if  (
                (spellDescription.m_aiming == 0) ||
                (spellDescription.m_effect == 0) ||
                (spellDescription.m_shape == 0) ||
                (spellDescription.m_usage == 0) 
            )
            return null;
        // valid spell go nuts
        return spellDescription;
    }

    public void Eject()
    {
        throw new System.NotImplementedException();
    }

    public void ToggleHighlighting(bool toggle)
    {
        throw new System.NotImplementedException();
    }
}



