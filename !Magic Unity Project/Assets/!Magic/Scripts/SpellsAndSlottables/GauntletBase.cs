using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the base code for the gauntlet itself.
public class GauntletBase : MonoBehaviour
{
    // Start is called before the first frame update
    public GauntletSlots[] m_gauntletSlotsScripts = new GauntletSlots[3];
    public List<int> m_spellsInSlots;

    public GameObject[] m_gauntletSlotsObjects = new GameObject[3];
    bool m_gauntletState; //toggle between pickup and cast states. Currently unused due to change in design.

    public PlayerData m_magicUser;
    public GauntletAiming m_aimingDirection;

    public Spell m_QueuedSpell;

    //sets up the slot objects attached
    void Start()
    {
        ToggleGauntletMode(); 
        
        //assigns 
        for (int i = 0; i < m_gauntletSlotsObjects.Length; i++)
        {
            m_gauntletSlotsScripts[i] = m_gauntletSlotsObjects[i].GetComponent<GauntletSlots>();
        }

    }

    // Update is called once per frame
    //right now, it's just updating hp/mp in playerdata
    void Update()
    {
        //update hp/mp
        UpdateAura();
        UpdateMana();

        /*if (Input.GetKeyDown(KeyCode.K))
        {
            ToggleGauntletMode();
        }*/


        if (m_gauntletState)
        {
            //do whatever gauntlet does in holding mode
        }
        else
        {
            //do whatever gauntlet does in casting mode
        }
    }

    //on some button press, it'll set the gauntlet to casting mode, or cancelling it to be in pickup mode
    //right now, it just swaps the base colour of the model to indicate mode. 
    //this will change once I figure out what we're doing to mode changing
    void ToggleGauntletMode()
    {
        m_gauntletState = !m_gauntletState;
    }

    public void UpdateAllSlots()
    {
        foreach(GauntletSlots slot in m_gauntletSlotsScripts)
        {
            slot.SpellPrismAddedToSlot();
        }
    }

    //Updates mana and mana bar. 
    void UpdateMana()
    {
        //update mana bar here
        /*if(m_magicUser!=null)
        {
            float manaPercentage = m_magicUser.m_mana / m_magicUser.m_manaMax;
        }*/
    }

    //Updates aura and aura bar. 
    void UpdateAura()
    {
        //update Aura bar here.
    }

    //goes through spell slots to check which ones are valid, and creates a spell based on them. not sure how this works yet
    public void AimSpell(int spellID)
    {
        foreach (GauntletSlots slot in m_gauntletSlotsScripts)
        {
            print(string.Format("Checking if spell id {0} matches slot id {1}", spellID, slot.m_spellID));
            if (slot != null && slot.m_spellID == spellID)
            {
                print("Found Match!");
                Spell slottedSpell = slot.GetSpell();
                if(slottedSpell!=null)
                {
                    m_QueuedSpell = slottedSpell;
                    slottedSpell.BeginAiming();
                    print(string.Format("Aiming spell id {0}, bypassing aiming", spellID));
                }
                else
                {
                    print("No slotted spell!!");
                }
            }
        }
    }

    public void CastSpell()
    {
        if (m_QueuedSpell == null)
            return;
        if (!(m_QueuedSpell.Interact(InteractionType.StartCasting)))
        {
            m_QueuedSpell.Interact(InteractionType.BypassAiming);
        }
        m_QueuedSpell = null;
    }

    public void CancelAiming()
    {
        if (m_QueuedSpell == null)
            return;
        if (!m_QueuedSpell.m_isAiming)
            return;

        m_QueuedSpell.Cancel();
        m_QueuedSpell = null;
    }


    //this is where it updates the list of spells in the gauntlet, and passes it to Weave
    public void UpdateSpellSlotList()
    {
        print(string.Format("Updating Spell Slots"));

        m_spellsInSlots.Clear();

        foreach (GauntletSlots slot in m_gauntletSlotsScripts)
        {
            if (slot != null && slot.m_spellID != int.MaxValue)
            { m_spellsInSlots.Add(slot.m_spellID); print(string.Format("Adding spell id {0} to gauntlet slots", slot.m_spellID)); }
            else if (slot == null)
            {
                print(string.Format("SLOT IS NULL"));
            }
        }
        
        Weave weaveScript = m_magicUser.gameObject.GetComponentInChildren<Weave>();

        if(weaveScript != null)
            weaveScript.SetSpellSelectionIDs(m_spellsInSlots);


    }













}

