using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//slots in the gauntlet
//needs to know what is slotted into it, allows things to be slotted into it,
public class GauntletSlots : MonoBehaviour, ISlot
{
    public GameObject m_Gauntlet; //ref to parent for ease of access

    public Material m_slotDefault;
    public Material m_slotHighlight;

    public ISlotable m_slotted { get; set; }
    public SlotTypes m_slotableType;

    public int m_spellID;

    //get the spell from the slotted spell prism
    public Spell GetSpell()
    {
        SlottableSpellPrism slotThinger1 = (SlottableSpellPrism)m_slotted;
        print(string.Format("slotThinger = {0}", slotThinger1));
        if (slotThinger1==null)
            return null;
        if (slotThinger1.m_spell==null)
            return null;
        return slotThinger1.m_spell;
    }

    public Vector3 m_slottedObjectOffset = Vector3.zero;

    bool m_slotHighlighted = false;

    //public UnityEvent SpellPrismRemoved;
    //public UnityEvent SpellPrismAdded;


    //allows the slottable to be attached to this slot
    public bool Accept(ISlotable slottingObject)
    {
        if (!CanAccept(slottingObject)) //check that it is a valid slottable type
            return false;

        if (slottingObject == null)
            return false;


        if (m_slotted != null) //if ther's already a prism, eject it first
        {



        }

        m_slotted = slottingObject;

        SlottableSpellPrism slotThinger = (SlottableSpellPrism)m_slotted;
        if (slotThinger != null)
        {
            slotThinger.transform.position = transform.position + m_slottedObjectOffset;
            slotThinger.transform.parent = transform;

            SpellPrismAddedToSlot();

        }

        return true;



    }

    //check that it is a valid slottable type
    public bool CanAccept(ISlotable slotable)
    {
        return slotable.m_slotType.HasFlag(SlotTypes.SpellCrystal);
    }

    //invert bool for if slot is highlighted
    public void ToggleHighlighting()
    {
        m_slotHighlighted = !m_slotHighlighted;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Gauntlet= transform.parent.gameObject;
        m_spellID = int.MaxValue;
    }

    // Update is called once per frame
    //updates slot material, set spellID if not already done
    void Update()
    {

        Material mat = gameObject.GetComponent<Renderer>().material;

        if (m_slotHighlighted && mat != m_slotHighlight)
        {
            mat = m_slotHighlight;
        }
        else if (!m_slotHighlighted && mat != m_slotDefault)
        {
            mat = m_slotDefault;
        }

        Valve.VR.SteamVR_Action_Boolean menubtn;
        
    }

    private void UpdateSlot()
    {
        if (m_spellID == int.MaxValue)
        {
            print(string.Format("Attempting to Assign"));
            SlottableSpellPrism slotThinger1 = (SlottableSpellPrism)m_slotted;
            if (slotThinger1 != null)
            {
                if (slotThinger1.m_spellID != m_spellID && slotThinger1.m_spellID != int.MaxValue)
                {
                    print(string.Format("Assigning {0} ", slotThinger1.m_spellID));
                    m_spellID = slotThinger1.m_spellID;
                }
                else
                {
                    print(string.Format("SpellThinger spell value is unset or already exsists in gaunlet!!!!"));
                }
            }
            else
            {
                print(string.Format("SpellThinger is null!!!!"));
            }
        }
    }

    public void SpellPrismRemovedFromSlot()
    {
        //SpellPrismRemoved.Invoke();
        m_spellID = int.MaxValue;
        m_Gauntlet.GetComponent<GauntletBase>().UpdateSpellSlotList();
        UpdateSlot();  
    }

    public void SpellPrismAddedToSlot()
    {
        //SpellPrismAdded.Invoke();
        m_Gauntlet.GetComponent<GauntletBase>().UpdateSpellSlotList();
        UpdateSlot();
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
