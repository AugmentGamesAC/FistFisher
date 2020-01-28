using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This is a class that contains the definition for each "action" (bait selection, atack selection, etc)
/// 
/// 
/// 		internalId (enum or whatever), 
/// 		HumanReadableId some string,
/// 		detailed discription (yet another string), 
/// 		ActionType (2axis,1axis,button,page), 
/// 		ContextGroup as field (Boat Travel,inventoryshop,swiminventoy,Battle,Swimming,Menu Navigation)
///
/// </summary>
public class ActionDefinition : MonoBehaviour
{
    public enum ActionID
    {
        BoatMovement,
        PlayerMovement,
        PlayerRotation,
        CameraRotation,
        DivingBellRaiseLower,
        MountToggle, //merge mount toggle and gather?
        Gather,
        PageSelect, //which menu to focus on (and change shop tab)
        HoldPick,
        Action, //sell, buy, eat, attack
        Flee,
        Item, //bait
        Targeting,
        AttackSwap,
        BaitSwap,

        OpenShop,
        OpenInventory,
        OpenMainMenu,


        InventoryAndMenuNavigation, // how does this tie in with ContextGroup.MenuNavigation?
    }

    [System.Flags]
    public enum ContextGroup
    {
        BoatTravel = 0x0001,
        InventoryShop = 0x0002,
        SwimInventory = 0x0004,
        Battle = 0x0010,
        Swimming = 0x0020,
        MenuNavigation = 0x0040,
    }

    [SerializeField]
    protected ActionID m_internalID;
    public ActionID InternalID => m_internalID;

    public string m_humanReadableID;

    public string m_detailedDescription;

    [SerializeField]
    protected ContextGroup m_contextGroups;
    public ContextGroup ContextGroups => m_contextGroups;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
