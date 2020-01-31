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
    /// <summary>
    /// the type of input
    /// </summary>
    public enum ActionType
    {
        /// <summary>
        /// pinwheel
        /// joysticks and such
        /// </summary>
        TwoAxis,
        /// <summary>
        /// along a line (L->R, U->D)
        /// </summary>
        OneAxis,
        /// <summary>
        /// single button down input
        /// </summary>
        Button,
        /// <summary>
        /// 2axis with additional 1axis for cycling
        /// </summary>
        Page,
    }

    /// <summary>
    /// all of the inputs, generally context sensitive but sharing keys/inputs
    /// </summary>
    public enum ActionID
    {
        /// <summary>
        /// rotates camera around player
        /// 2 axis
        /// </summary>
        CameraRotation, 
        /// <summary>
        /// while swimming or steering boat. 
        /// or while selecting inventory slots
        /// 2Axis
        /// </summary>
        MovementOrInventoryNavigation,
        /// <summary>
        /// standard action key (button), context sensitive.
        /// Harvest - swim, not in range of boat
        /// Mount - swimming, in range
        /// Dismount - boat
        /// Attack - battle
        /// Select highlighted/hovered option - Menu Nav
        /// Sell/Buy/Eat - inv/shop
        /// </summary>
        Action, 
        /// <summary>
        /// Use Bait - combat or swimming
        /// Back - menu navigation
        /// </summary>
        SecondaryAction,
        /// <summary>
        /// raises/lowers diving bell if applicable
        /// 1 axis
        /// </summary>
        DivingBellRaiseLower,
        /// <summary>
        /// opens inventory if in water,
        /// opens inventory and shop if mounted on boat
        /// </summary>
        OpenInventory,
        /// <summary>
        /// Esc button
        /// shows main menu/pause - which contains ways to open options/quit/etc
        /// </summary>
        MainMenu,
        /// <summary>
        /// Picks up the selected item in inventory or shop for inventory manipulation
        /// </summary>
        HoldPickup,
        /// <summary>
        /// PAGE
        /// cycles through tabs or menu navigation
        /// 2 axis to rotate a pinwheel
        /// 1 axis for LR cycling through
        /// </summary>
        MenuAndShopMenuNavigationPageSelect,
        /// <summary>
        /// in combat, flees
        /// button
        /// </summary>
        Flee, 
        /// <summary>
        /// cycles through targets in list
        /// 1 axis
        /// </summary>
        Targeting,
        /// <summary>
        /// opens a pinwheel to select a attack to use
        /// 2axis
        /// </summary>
        AttackSwap, 
        /// <summary>
        /// opens a pinwheel to select a bait to use
        /// 2axis
        /// </summary>
        BaitSwap, 
    }

    /// <summary>
    /// the context for keys
    /// EX: Action will do different things while swimming or in shop
    /// </summary>
    [System.Flags]
    public enum ContextGroup
    {
        BoatTravel = 0x0001,
        InventoryShop = 0x0002,
        Battle = 0x0004,
        MenuNavigation = 0x0008,
        Swimming = 0x0010,
    }

    [SerializeField]
    protected ActionID m_internalID;
    public ActionID InternalID => m_internalID;

    public string m_humanReadableID;

    public string m_detailedDescription;

    [SerializeField]
    protected ContextGroup m_contextGroups;
    public ContextGroup ContextGroups => m_contextGroups;

    [SerializeField]
    protected ActionType m_actionType;
    public ActionType InputActionType => m_actionType;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
