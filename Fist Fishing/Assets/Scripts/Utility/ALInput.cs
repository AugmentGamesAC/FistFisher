using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is our "abstract layer for input"
/// it's purpose was to allow us to easily change the keys associated to axis/key inputs, 
/// and to allow us to just ask this for is that associated input was active
/// </summary>
public class ALInput : MonoBehaviour
{
    /// <summary>
    /// checks that we actually have an instance of ALInput running
    /// </summary>
    private static void hasInstance()
    {
        if (Instance == default)
            throw new System.InvalidOperationException("ALInput not Initilized");
    }


    #region keycodes keycodes
    /// <summary>
    /// Action, AltAction, Cancel, Toggle, Menu, Inventory, MouseAction, MouseCancel. 8 elements - In that order please.
    /// </summary>
    [SerializeField]
    protected KeyCode[] m_currentKeyCodes;
    /// <summary>
    /// This has 2 extra buttons that are exclusive to keyboard and mouse.
    /// </summary>
    [SerializeField]
    protected KeyCode[] m_keyboardKeyCodes;
    /// <summary>
    /// Last 2 elements are left empty on purpose please leave them that way
    /// </summary>
    [SerializeField]
    protected KeyCode[] m_controllerKeyCodes;
    /// <summary>
    /// Covers Selecting/Starting, Quick Buy, Quick Sell, Attacking, Mounting, Dismounting, and Gathering, Combat: Attack Action
    /// </summary>
    public static KeyCode Action { get { hasInstance(); return Instance.m_currentKeyCodes[0]; } }
    /// <summary>
    /// Covers Canceling Menus, Quick Buy, Quick Sell, Ascending, Combat: Item Action
    /// </summary>
    public static KeyCode AltAction { get { hasInstance(); return Instance.m_currentKeyCodes[1]; }  }
    /// <summary>
    /// Button for exiting menus, decsending, and Combat: using item
    /// </summary>
    public static KeyCode Cancel { get { hasInstance(); return Instance.m_currentKeyCodes[2]; } }
    /// <summary>
    /// Toggles screens, camera mode, quips?
    /// </summary>
    public static KeyCode Toggle { get { hasInstance(); return Instance.m_currentKeyCodes[3]; } }
    /// <summary>
    /// Opens Menu
    /// </summary>
    public static KeyCode Menu { get { hasInstance(); return Instance.m_currentKeyCodes[4]; } }
    /// <summary>
    /// Opening player inventory
    /// </summary>
    public static KeyCode Inventory { get { hasInstance(); return Instance.m_currentKeyCodes[5]; } }
    /// <summary>
    /// Make selections with the LMB, acts as a second action button for prompts. Only for keyboard & mouse.
    /// </summary>
    public static KeyCode MouseAction { get { hasInstance(); return Instance.m_currentKeyCodes[6]; } }
    /// <summary>
    /// Cancel prompts and things with RMB. Only for keyboard & mouse.
    /// </summary>
    public static KeyCode MouseCancel { get { hasInstance(); return Instance.m_currentKeyCodes[7]; } }

    /// <summary>
    /// Opens Menu with keyboard (This is always an option because it's jarring otherwise)
    /// </summary>
    public static KeyCode MenuKeyboard { get { hasInstance(); return Instance.m_currentKeyCodes[8]; } }
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
        LookHorizontal,
        LookVertical,
        MoveHorizontal,
        MoveVertical,
        JoystickMoveHorizontal,
        JoystickMoveVertical,
        JoystickLookHorizontal,
        JoystickLookVertical,
        Switch,
        JoystickSwitch
    };

    public enum AxisType
    {
        LookHorizontal,
        LookVertical,
        MoveHorizontal,
        MoveVertical,
        Switch
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
        LookInput,
        JoystickMoveInput,
        JoystickLookInput
    }
    /// <summary>
    /// Tuple is a dummy object that lets me link 3 objects into one without an offical class
    /// this allows us to define behavior in the dictionary here rather than in movementDirection
    /// will need to be refined when doing dynamic updating to controls
    /// </summary>
    private static Dictionary<DirectionCode, System.Tuple<AxisCode, AxisCode, AxisCode>> m_registeredMouseDirections =
        new Dictionary<DirectionCode, System.Tuple<AxisCode, AxisCode, AxisCode>>()
        {
            {DirectionCode.LookInput, new System.Tuple<AxisCode, AxisCode, AxisCode>(AxisCode.LookHorizontal,AxisCode.LookVertical,AxisCode.Unset) },
            {DirectionCode.MoveInput, new System.Tuple<AxisCode, AxisCode, AxisCode>(AxisCode.MoveHorizontal,AxisCode.Unset,AxisCode.MoveVertical) }
        };
    private static Dictionary<DirectionCode, System.Tuple<AxisCode, AxisCode, AxisCode>> m_registeredJoystickDirections =
        new Dictionary<DirectionCode, System.Tuple<AxisCode, AxisCode, AxisCode>>()
        {
            {DirectionCode.JoystickLookInput, new System.Tuple<AxisCode, AxisCode, AxisCode>(AxisCode.JoystickLookHorizontal,AxisCode.JoystickLookVertical,AxisCode.Unset) },
            {DirectionCode.JoystickMoveInput, new System.Tuple<AxisCode, AxisCode, AxisCode>(AxisCode.JoystickMoveHorizontal,AxisCode.Unset,AxisCode.JoystickMoveVertical) }
        };

    public static bool GetKeyUp(KeyCode key) { hasInstance(); return Input.GetKeyUp(key); }
    public static bool GetKeyDown(KeyCode key) { hasInstance(); return Input.GetKeyDown(key); }
    public static bool GetKey(KeyCode key) { hasInstance(); return Input.GetKey(key); }


    public static float GetAxis(AxisType type)
    {
        hasInstance();
        //Return the correct control input based on if controller is toggled or not

        //Invert is toggled return the inverse 
        if (InvertToggle && type == AxisType.LookVertical)
            return (ControllerToggle ? -Input.GetAxis("Joystick" + type.ToString()) : -Input.GetAxis(type.ToString()));

        return (ControllerToggle ? Input.GetAxis("Joystick" + type.ToString()) : Input.GetAxis(type.ToString()));

    }

    public static float GetAxisByCode(AxisCode key)
    {
        hasInstance();
        //Unset set to 0 here for efficiency reasons
        return (key == AxisCode.Unset) ? 0 : Input.GetAxis(key.ToString());
    }
    /// <summary>
    /// Returns true on the frame an axis that has a button is modified by a press. (Use in update)
    /// AXIS NEEDS TO HAVE A BUTTON
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool GetAxisPress(AxisType type)
    {
        if (!ControllerToggle && type == AxisType.Switch)
            return (Input.GetAxis(type.ToString()) != 0 ? true : false);

        return (ControllerToggle ? Input.GetButtonDown("Joystick" + type.ToString()) : Input.GetButtonDown(type.ToString()));
    }
    /// <summary>
    /// gets if it is the keyboard or controller inputs used
    /// </summary>
    /// <returns></returns>
    public static bool IsControllerToggled()
    {
        return (Instance.m_controllerToggle ? true : false);
    }
    /// <summary>
    /// toggles if controller is being used or keyboard
    /// </summary>
    public static void ToggleController()
    {
        Instance.m_controllerToggle = !ControllerToggle;
        //Swap controls
        Instance.m_currentKeyCodes = ControllerToggle ? Instance.m_controllerKeyCodes : Instance.m_keyboardKeyCodes;
    }

    /// <summary>
    /// When looking, allows for axis direction inversion
    /// </summary>
    public static void InvertLookYAxis()
    {
        Instance.m_invertToggle = !InvertToggle;
    }
    /// <summary>
    /// this function checks our registered direction codes and supples a vec3 as desired 
    /// </summary>
    /// <param name="dC">Enum defined in ALinput</param>
    /// <returns>either Vector3.zero on failure or a new vector 3 based on registeredDirections</returns>
    public static Vector3 GetDirection(DirectionCode dC)
    {
        System.Tuple<AxisCode, AxisCode, AxisCode> directionInstructions;

        var controller = (ControllerToggle) ? m_registeredJoystickDirections : m_registeredMouseDirections;
        if (!controller.TryGetValue(dC, out directionInstructions))
            return Vector3.zero;

        return new Vector3
        (
                GetAxisByCode(directionInstructions.Item1),
                GetAxisByCode(directionInstructions.Item2),
                GetAxisByCode(directionInstructions.Item3)
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
        //Set starting controls
        {
            
        }
    }


    /// <summary>
    /// at some point this will load configuration from file
    /// </summary>
    private void LoadFromFile()
    {
    }

    [SerializeField]
    protected bool m_controllerToggle;
    public static bool ControllerToggle { get { hasInstance(); return Instance.m_controllerToggle; } }
    [SerializeField]
    protected bool m_invertToggle;
    public static bool InvertToggle { get { hasInstance(); return Instance.m_invertToggle; } }
}
