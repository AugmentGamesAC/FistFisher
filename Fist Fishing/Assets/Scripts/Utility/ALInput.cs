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
        JoystickLookVertical
    };

    public enum AxisType
    {
        LookHorizontal,
        LookVertical,
        MoveHorizontal,
        MoveVertical
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


    public AxisCode m_playerLateralMovement;
    public static AxisCode PlayerLateralMovement { get { hasInstance(); return Instance.m_playerLateralMovement; } }

    public static bool GetKeyUp(KeyCode key) { hasInstance(); return Input.GetKeyUp(key); }
    public static bool GetKeyDown(KeyCode key) { hasInstance(); return Input.GetKeyDown(key); }
    public static bool GetKey(KeyCode key) { hasInstance(); return Input.GetKey(key); }


    public static float GetAxis(AxisType dir)
    {
        hasInstance();
        //Return the correct control input based on if controller is toggled or not

        //Invert is toggled return the inverse 
        if (InvertToggle && dir == AxisType.LookVertical)
            return (ControllerToggle ? -Input.GetAxis("Joystick" + dir.ToString()) : -Input.GetAxis(dir.ToString()));

        return (ControllerToggle ? Input.GetAxis("Joystick" + dir.ToString()) : Input.GetAxis(dir.ToString()));

    }

    public static float GetAxisByCode(AxisCode key)
    {
        hasInstance();
        //Unset set to 0 here for efficiency reasons
        return (key == AxisCode.Unset) ? 0 : Input.GetAxis(key.ToString());
    }

    public static bool IsControllerToggled()
    {
        return (Instance.m_controllerToggle ? true : false);
    }

    public static void ToggleController()
    {
        Instance.m_controllerToggle = !ControllerToggle;
    }

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

        //this breaks and directionInstuctions is null so player cannot receive input yet.
        if (!ControllerToggle)
        {
            if (!m_registeredMouseDirections.TryGetValue(dC, out directionInstructions))
                return Vector3.zero;
        }
        else
        {
            if (!m_registeredJoystickDirections.TryGetValue(dC, out directionInstructions))
                return Vector3.zero;
        }
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

    }

    [SerializeField]
    protected bool m_controllerToggle;
    public static bool ControllerToggle { get { hasInstance(); return Instance.m_controllerToggle; } }
    [SerializeField]
    protected bool m_invertToggle;
    public static bool InvertToggle { get { hasInstance(); return Instance.m_invertToggle; } }
}
