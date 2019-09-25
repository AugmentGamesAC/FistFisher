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


    public bool Accept(ISlotable slottingObject)
    {
        if (!CanAccept(slottingObject.m_slotType))
            return false;

        if (slottingObject == null)
            return false;


        if (m_slotted != null) //if ther's already a prism, ejet it first
        {

            SlottableSpellPrism slotThinger1 = (SlottableSpellPrism)m_slotted;
            slotThinger1.DetachFromSlot();
            SpellPrismRemovedFromSlot();

        }

        m_slotted = slottingObject;

        //Debug.LogError("Slotted: "+ m_slotted.GetType());
        SlottableSpellPrism slotThinger = (SlottableSpellPrism)m_slotted;
        if (slotThinger != null)
        {
            slotThinger.transform.position = transform.position + m_slottedObjectOffset;
            slotThinger.transform.parent = transform;

            SpellPrismAddedToSlot();

        }

        return true;



    }

    public bool CanAccept(SlotTypes slotableType)
    {
        return slotableType.HasFlag(SlotTypes.SpellCrystal);
    }


    public void ToggleHighlighting()
    {
        m_slotHighlighted = !m_slotHighlighted;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Gauntlet= transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {


        if (m_slotHighlighted)
        {
            gameObject.GetComponent<Renderer>().material = m_slotHighlight;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = m_slotDefault;
        }

    }



    public void SpellPrismRemovedFromSlot()
    {
        //SpellPrismRemoved.Invoke();
        m_spellID = 0;
        m_Gauntlet.GetComponent<GauntletBase>().UpdateSpellSlotList();
    }

    public void SpellPrismAddedToSlot()
    {
        //SpellPrismAdded.Invoke();
        m_Gauntlet.GetComponent<GauntletBase>().UpdateSpellSlotList();
    }








}
