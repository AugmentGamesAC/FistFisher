﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.IO;

/// <summary>
/// this class stores, saves, and loads all global configuration data
/// </summary>
[System.Serializable]
public class Configurations : MonoBehaviour
{

    #region singletonification
    private static Configurations Instance;
    private static void hasInstance()
    {
        if (Instance == default)
            throw new System.InvalidOperationException("ALInput not Initilized");
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
    //[SerializeField]
    //protected List<KeyConfiguration> keyConfigurations; //(in the future, for customizable controls)



    public bool IsYAxisInverted => m_invertYAxis;
    public bool IsXAxisInverted => m_invertXAxis;
    public float Gamma => m_gamma;
    public float VolumeSFX => m_sfxVolume;
    public float VolumeMusic => m_musicVolume;
    public float AxisSensitivity => m_sensitivity;
    public int QualityLevel => m_qualityLevel;
    public Vector2 ScreenResolution => m_screenResolution;





    void Start()
    {
        //LoadAllSettingsFromFile();
    }


    void Update()
    {

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
    
}