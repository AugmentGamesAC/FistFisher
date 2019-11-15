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
    [SerializeField]
    protected KeyCode m_forward;
    public static KeyCode Forward { get { hasInstance(); return Instance.m_forward; } }
    [SerializeField]
    KeyCode m_goRight;
    public static KeyCode GoRight { get { hasInstance(); return Instance.m_goRight; } }
    [SerializeField]
    KeyCode m_goLeft;
    public static KeyCode GoLeft { get { hasInstance(); return Instance.m_goLeft; } }
    [SerializeField]
    KeyCode m_jump;
    public static KeyCode Jump { get { hasInstance(); return Instance.m_jump; } }
    [SerializeField]
    KeyCode m_sprint;
    public static KeyCode Sprint { get { hasInstance(); return Instance.m_sprint; } }
    [SerializeField]
    KeyCode m_keyTarget;
    public static KeyCode KeyTarget { get { hasInstance(); return Instance.m_keyTarget; } }
    [SerializeField]
    KeyCode m_forgetTarget;
    public static KeyCode ForgetTarget { get { hasInstance(); return Instance.m_forgetTarget; } }



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
        Vertical
        //futhere axis will be needed for a controler. 
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
    /// this allows us to define behavoir in the dictionary here rather than in movementDirection
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
        m_jump = KeyCode.Space;
        m_sprint = KeyCode.LeftShift;
        m_keyTarget = KeyCode.Z;
        m_forgetTarget = KeyCode.X;

        m_playerLateralMovement = AxisCode.Horizontal;
    }
}
