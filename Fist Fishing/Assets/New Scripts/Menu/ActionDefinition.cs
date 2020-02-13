using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// all of the inputs, generally context sensitive but sharing keys/inputs
/// Need to playtest to ensure these are not too condensed
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
    /// out of combat, toggles between 3rd person or 1st person camera modes (f3-f4) 
    /// button
    /// </summary>
    FleeOrCameraModeSwap,
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


    [SerializeField]
    protected ActionID m_internalID;
    public ActionID InternalID => m_internalID;


    [SerializeField]
    protected string m_humanReadableID;
    public string HumandReadableID => m_humanReadableID;

    [SerializeField]
    protected string m_detailedDescription;
    public string DetailedDescription => m_detailedDescription;

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
