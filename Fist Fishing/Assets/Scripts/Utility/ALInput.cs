using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ALInput : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    private static void hasInstance()
    {
        if (Instance == default)
            throw new System.InvalidOperationException("ALInput not Initilized");
    }

    #region keycodes keycodes
    //Movement Keys
    [SerializeField]
    protected KeyCode m_forward;
    public static KeyCode Forward { get { hasInstance(); return Instance.m_forward; } }
    [SerializeField]
    protected KeyCode m_backward;
    public static KeyCode Backward { get { hasInstance(); return Instance.m_backward; } }
    [SerializeField]
    KeyCode m_goRight;
    public static KeyCode GoRight { get { hasInstance(); return Instance.m_goRight; } }
    [SerializeField]
    KeyCode m_goLeft;
    public static KeyCode GoLeft { get { hasInstance(); return Instance.m_goLeft; } }
  
    [SerializeField]
    KeyCode m_action;
    /// <summary>
    /// Covers Selecting/Starting, Quick Buy, Quick Sell, Attacking, Mounting, Dismounting, and Gathering, Combat: Attack Action
    /// </summary>
    public static KeyCode Action { get { hasInstance(); return Instance.m_action; } }
    [SerializeField]
    KeyCode m_altAction;
    /// <summary>
    /// Covers Canceling Menus, Quick Buy, Quick Sell, Ascending, Combat: Item Action
    /// </summary>
    public static KeyCode AltAction { get { hasInstance(); return Instance.m_altAction; } }
    [SerializeField]
    KeyCode m_cancleKey;
    /// <summary>
    /// Button for exiting menus, decsending, and Combat: using item
    /// </summary>
    public static KeyCode CancleKey { get { hasInstance(); return Instance.m_cancleKey; } }
    [SerializeField]
    KeyCode m_toggle;
    /// <summary>
    /// Toggles screens, camera mode, quips?
    /// </summary>
    public static KeyCode Toggle { get { hasInstance(); return Instance.m_toggle; } }
    [SerializeField]
    KeyCode m_menuKey;
    /// <summary>
    /// Opens Menu
    /// </summary>
    public static KeyCode MenuButton { get { hasInstance(); return Instance.m_menuKey; } }
    [SerializeField]
    KeyCode m_selectMouse;
    /// <summary>
    /// Make selections with the LMB, acts as a second action button for prompts
    /// </summary>
    public static KeyCode MouseAction { get { hasInstance(); return Instance.m_selectMouse; } }
    [SerializeField]
    KeyCode m_switch;
    /// <summary>
    /// Switching targets and opening player inventory
    /// </summary>
    public static KeyCode Switch { get { hasInstance(); return Instance.m_switch; } }
    [SerializeField]
    KeyCode m_cancelMouse;
    public static KeyCode MouseCancel { get { hasInstance(); return Instance.m_cancelMouse; } }


    [SerializeField]
    KeyCode m_sprint;
    public static KeyCode Sprint { get { hasInstance(); return Instance.m_sprint; } }

    [SerializeField]
    KeyCode m_manualCamera;
    public static KeyCode ManualCamera { get { hasInstance(); return Instance.m_manualCamera; } }


    [SerializeField]
    KeyCode m_rotateForward;
    public static KeyCode RotateForward { get { hasInstance(); return Instance.m_rotateForward; } }
    [SerializeField]
    KeyCode m_rotateBackwards;
    public static KeyCode RotateBackwards { get { hasInstance(); return Instance.m_rotateBackwards; } }
    [SerializeField]
    KeyCode m_rotateRight;
    public static KeyCode RotateRight { get { hasInstance(); return Instance.m_rotateRight; } }
    [SerializeField]
    KeyCode m_rotateLeft;
    public static KeyCode RotateLeft { get { hasInstance(); return Instance.m_rotateLeft; } }



    
    //[SerializeField]
    //KeyCode m_keyTarget;
    //public static KeyCode KeyTarget { get { hasInstance(); return Instance.m_keyTarget; } }
    //[SerializeField]
    //KeyCode m_forgetTarget;
    //public static KeyCode ForgetTarget { get { hasInstance(); return Instance.m_forgetTarget; } }
    //[SerializeField]
    //KeyCode m_punch;
    //public static KeyCode Punch { get { hasInstance(); return Instance.m_punch; } }


    //[SerializeField]
    //KeyCode m_mountBoat;
    //public static KeyCode MountBoat { get { hasInstance(); return Instance.m_mountBoat; } }
    //[SerializeField]
    //KeyCode m_dismountBoat;
    //public static KeyCode DismountBoat { get { hasInstance(); return Instance.m_dismountBoat; } }

    //[SerializeField]
    //KeyCode m_harvest;
    //public static KeyCode Harvest { get { hasInstance(); return Instance.m_harvest; } }

    //[SerializeField]
    //KeyCode m_throwBait;
    //public static KeyCode ThrowBait { get { hasInstance(); return Instance.m_throwBait; } }

    //[SerializeField]
    //KeyCode m_craftBait;
    //public static KeyCode CraftBait { get { hasInstance(); return Instance.m_craftBait; } }

    //[SerializeField]
    //KeyCode m_toggleInventory;
    //public static KeyCode ToggleInventory { get { hasInstance(); return Instance.m_toggleInventory; } }

    //[SerializeField]
    //KeyCode m_toggleShop;
    //public static KeyCode ToggleShop { get { hasInstance(); return Instance.m_toggleShop; } }

    //[SerializeField]
    //KeyCode m_start;
    //public static KeyCode Start { get { hasInstance(); return Instance.m_start; } }
    
    ////Encounter Combat Buttons.
    //[SerializeField]
    //KeyCode m_attack;
    //public static KeyCode Attack { get { hasInstance(); return Instance.m_attack; } }

    //[SerializeField]
    //KeyCode m_item;
    //public static KeyCode Item { get { hasInstance(); return Instance.m_item; } }

    //[SerializeField]
    //KeyCode m_flee;
    //public static KeyCode Flee { get { hasInstance(); return Instance.m_flee; } }

    //camera states
    /*[SerializeField]
    KeyCode m_abzu;
    public static KeyCode Abzu { get { hasInstance(); return Instance.m_abzu; } }

    [SerializeField]
    KeyCode m_locked;
    public static KeyCode Locked { get { hasInstance(); return Instance.m_locked; } }

    [SerializeField]
    KeyCode m_warthog;
    public static KeyCode Warthog { get { hasInstance(); return Instance.m_warthog; } }

    [SerializeField]
    KeyCode m_firstPerson;
    public static KeyCode FirstPerson { get { hasInstance(); return Instance.m_firstPerson; } }*/


    //[SerializeField]
    //KeyCode m_cameraSwap;
    //public static KeyCode CameraSwap { get { hasInstance(); return Instance.m_cameraSwap; } }

    #endregion keycodes


    /// <summary>
    /// These are the AxisCodes that we configured in unity.Input manager as name
    /// </summary>
    public enum AxisCode
    {
        /// <summary>
        /// this is a cheat that will always return 0 using ALInput.GetAxis
        /// </summary>
        Unset,
        MouseX,
        MouseY,
        Horizontal,
        Vertical,
        MHorizontal,
        MUp,
        MForward,
        LHorizontal,
        LVertical
    };

    public enum DirectionCode
    {
        /// <summary>
        /// returns Vector3.zero
        /// </summary>
        Unset,
        /// <summary>
        /// AxisCode.Horizontal,0,AxisCode.Vertical
        /// </summary>
        MoveInput,
        /// <summary>
        /// AxisCode.MouseX,AxisCode.MouseY,0
        /// </summary>
        LookInput
    }
    /// <summary>
    /// Tuple is a dummy object that lets me link 3 objects into one without an offical class
    /// this allows us to define behavior in the dictionary here rather than in movementDirection
    /// will need to be refined when doing dynamic updating to controls
    /// </summary>
    private static Dictionary<DirectionCode, System.Tuple<AxisCode, AxisCode, AxisCode>> m_registeredDirections =
        new Dictionary<DirectionCode, System.Tuple<AxisCode, AxisCode, AxisCode>>()
        {
            {DirectionCode.LookInput, new System.Tuple<AxisCode, AxisCode, AxisCode>(AxisCode.MouseX,AxisCode.MouseY,AxisCode.Unset) },
            {DirectionCode.MoveInput, new System.Tuple<AxisCode, AxisCode, AxisCode>(AxisCode.Horizontal,AxisCode.Unset,AxisCode.Vertical) }
        };


    public AxisCode m_playerLateralMovement;
    public static AxisCode PlayerLateralMovement { get { hasInstance(); return Instance.m_playerLateralMovement; } }
    
    public static bool GetKeyUp(KeyCode key) { hasInstance(); return Input.GetKeyUp(key); }
    public static bool GetKeyDown(KeyCode key) { hasInstance(); return Input.GetKeyDown(key); }
    public static bool GetKey(KeyCode key) { hasInstance(); return Input.GetKey(key); }

    public static float GetAxis(AxisCode key)
    {
        hasInstance();
        //Unset set to 0 here for efficiency reasons
        return (key == AxisCode.Unset) ? 0: Input.GetAxis(key.ToString());
    }

    
    /// <summary>
    /// this function checks our registered direction codes and supples a vec3 as desired 
    /// </summary>
    /// <param name="dC">Enum defined in ALinput</param>
    /// <returns>either Vector3.zero on failure or a new vector 3 based on registeredDirections</returns>
    public static Vector3 GetDirection(DirectionCode dC)
    {
        System.Tuple<AxisCode, AxisCode, AxisCode> directionInstructions;

        //this breaks and directionInstuctions is null so player cannot receive input yet.
        if (!m_registeredDirections.TryGetValue(dC, out directionInstructions))
            return Vector3.zero;

        return new Vector3
        (
                GetAxis(directionInstructions.Item1),
                GetAxis(directionInstructions.Item2),
                GetAxis(directionInstructions.Item3)
        );
    }


    private static ALInput Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject); //unity is stupid. Needs this to not implode
        Instance = this;
        LoadFromFile();
    }


    /// <summary>
    /// at some point this will load configuration from file, right now just applies defaults
    /// </summary>
    private void LoadFromFile()
    {
        
        m_forward = KeyCode.W;
        m_goLeft = KeyCode.A;
        m_goRight = KeyCode.D;
        m_action = KeyCode.F;
        m_altAction = KeyCode.R;
        m_toggle = KeyCode.Space;
        m_menuKey = KeyCode.Escape;
        m_cancleKey = KeyCode.E;

        //OLD KEYS
        //m_sprint = KeyCode.LeftShift;
        //m_keyTarget = KeyCode.Z;
        //m_forgetTarget = KeyCode.X;
        //m_punch = KeyCode.P;
        //m_manualCamera = KeyCode.Mouse1;
        //m_backward = KeyCode.S;

        ////Default Combat buttons
        //m_attack = KeyCode.Mouse0;
        //m_item = KeyCode.Mouse1;
        //m_flee = KeyCode.N;

        //m_rotateForward = KeyCode.Keypad8;
        //m_rotateBackwards = KeyCode.Keypad2;
        //m_rotateRight = KeyCode.Keypad4;
        //m_rotateLeft = KeyCode.Keypad6;

        ///*m_abzu = KeyCode.F1;
        //m_locked = KeyCode.F2;
        //m_warthog = KeyCode.F3;
        //m_firstPerson = KeyCode.F4;*/
        //m_cameraSwap = KeyCode.T;

        //m_mountBoat = KeyCode.M;
        //m_dismountBoat = KeyCode.N;
        //m_harvest = KeyCode.R;
        //m_throwBait = KeyCode.O;

        //m_craftBait = KeyCode.B;

        //m_toggleInventory = KeyCode.I;
        //m_toggleShop = KeyCode.O;

        //m_start = KeyCode.Space;

        //m_showOptionsPause = KeyCode.Escape;

        //m_playerLateralMovement = AxisCode.Horizontal;


    }
}
