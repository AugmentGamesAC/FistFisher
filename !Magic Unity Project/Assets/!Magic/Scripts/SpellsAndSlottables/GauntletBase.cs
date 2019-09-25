using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the base code for the gauntlet itself.
//it needs to swap modes, initiate casting, allow for cancelling, pickup/drop/etc objects, and contain spell slots
//may or may not track health/mana
public class GauntletBase : MonoBehaviour
{
    // Start is called before the first frame update
    public GauntletSlots[] m_gauntletSlotsScripts = new GauntletSlots[3];
    public List<int> m_spellsInSlots;

    public GameObject[] m_gauntletSlotsObjects = new GameObject[3];
    bool m_gauntletState; //toggle between pickup and cast states

    //currently unused interfaces
    public PlayerData m_magicUser;
    public GauntletAiming m_aimingDirection;

    //sets up the slot objects attached
    void Start()
    {
        ToggleGauntletMode();
                     
        for (int i = 0; i < m_gauntletSlotsObjects.Length; i++)
        {
            m_gauntletSlotsScripts[i] = m_gauntletSlotsObjects[i].GetComponent<GauntletSlots>();
        }

    }

    // Update is called once per frame
    //right now, it's just chacking for inputs used for debugging
    void Update()
    {
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

    //Updates mana and mana bar. 
    void UpdateMana()
    {
        if(m_magicUser!=null)
        {
            float manaPercentage = m_magicUser.m_mana / m_magicUser.m_manaMax;
        }
    }

    //Updates aura and aura bar. 
    void UpdateAura()
    {
        //update Aura bar here.
    }

    //goes through spell slots to check which ones are valid, and creates a spell based on them. not sure how this works yet
    void CastSpell()
    {

    }


    public void UpdateSpellSlotList()
    {
        //public GauntletSlots[] m_gauntletSlotsScripts = new GauntletSlots[3];

        m_spellsInSlots.Clear();

        foreach (GauntletSlots slot in m_gauntletSlotsScripts)
        {
            m_spellsInSlots.Add(slot.m_spellID);
        }

        //SetSpellSelectionIDs(m_spellsInSlots);


    }













}

