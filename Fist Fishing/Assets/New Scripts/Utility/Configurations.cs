using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.IO;
using UnityEngine.UI;

/// <summary>
/// this class stores, saves, and loads all global configuration data
/// </summary>
[System.Serializable]
public class Configurations : MonoBehaviour
{

    #region singletonification
    public static Configurations Instance;
    private static void hasInstance()
    {
        if (Instance == default)
            throw new System.InvalidOperationException("Configurations not Initilized");
    }
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject); //unity is stupid. Needs this to not implode
        Instance = this;
    }
    #endregion

    //this allows us to know the current context to deal with the keys
    //protected ContextGroup m_worldCurrentContext;
    //public ContextGroup CurrentWorldContext => m_worldCurrentContext;
    //public void SetCurrentWorldContext(ContextGroup context) { m_worldCurrentContext = context; }

    [SerializeField]
    protected Vector2 m_screenResolution;
    [SerializeField]
    protected int m_qualityLevel;
    [SerializeField]
    protected float m_gamma;
    [SerializeField]
    protected float m_sfxVolume;
    [SerializeField]
    protected float m_musicVolume;
    [SerializeField]
    protected float m_sensitivity;
    [SerializeField]
    protected bool m_invertXAxis;
    [SerializeField]
    protected bool m_invertYAxis;



    /******************************************************************************************************************************************/
    //[SerializeField]
    //protected List<KeyConfiguration> keyConfigurations; //(in the future, for customizable controls)
    //protected HardcodedKBM m_controls;
    //[SerializeField]
    //public HardcodedKBM Controls => m_controls;
    /******************************************************************************************************************************************/



    protected MenuScreens m_lastMenuShownBeforeShowingOptionsPauseScreen = MenuScreens.NotSet;



    public bool IsYAxisInverted => m_invertYAxis;
    public bool IsXAxisInverted => m_invertXAxis;
    public float Gamma => m_gamma;
    public float VolumeSFX => m_sfxVolume;
    public float VolumeMusic => m_musicVolume;
    public float AxisSensitivity => m_sensitivity;
    public int QualityLevel => m_qualityLevel;
    public Vector2 ScreenResolution => m_screenResolution;


    public Light m_Light;
    public Slider m_gammaSlider;

    [SerializeField]
    public void SetGamma()
    {
        m_gamma = m_gammaSlider.value;
        m_Light.intensity = m_gamma;
    }

    void Start()
    {
        //LoadAllSettingsFromFile();

        //Camera cam = UnityEngine.Camera.current;
        //Color ambientLight = RenderSettings.ambientLight;
        //RenderSettings.ambientIntensity = m_gamma+99999999;
        m_Light.intensity = m_gamma;
        m_gammaSlider.value = m_gamma;
    }


    void Update()
    {
        if (ALInput.GetKeyDown(ALInput.ShowOptionsPause))
        {
            if(NewMenuManager.CurrentMenu == MenuScreens.OptionsMenu)
            {
                NewMenuManager.DisplayMenuScreen(m_lastMenuShownBeforeShowingOptionsPauseScreen);
            }
            else
            {
                m_lastMenuShownBeforeShowingOptionsPauseScreen=NewMenuManager.CurrentMenu;
                NewMenuManager.DisplayMenuScreen(MenuScreens.OptionsMenu);
            }
        }
    }


    /// <summary>
    /// this takes the variables of this class and shoves them into a file for future loading
    /// </summary>
    protected void SaveAllSettingsToFile()
    {
        throw new System.NotImplementedException("Not Implemented");


    }

    /// <summary>
    /// this takes all the data from a file and loads it into the variables above
    /// </summary>
    protected void LoadAllSettingsFromFile()
    {
        throw new System.NotImplementedException("Not Implemented");
    }
    

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    /*public static bool IsThisPressed(ActionID actionID)
    {
        return Instance.Controls.m_KBMKeyConfig.IsThisPressed(actionID);
    }*/

    /*public static Vector3 AxisDirections(ActionID actionID)
    {
        return Instance.Controls.m_KBMKeyConfig.AxisDirections(actionID);
    }*/

    /*public static float GetAxis(ALInput.AxisCode ac)
    {
        return KeyConfiguration.GetAxis(ac);
    }*/
}
