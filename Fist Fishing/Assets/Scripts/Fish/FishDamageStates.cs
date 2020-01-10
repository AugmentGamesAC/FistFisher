using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDamageStates : MonoBehaviour
{
    public HealthModule m_healthModule;
    private GameObject m_currentModel;

    #region working inspector dictionary
    /// <summary>
    /// this is the mess required to make dictionaries with  list as a value work in inspector
    /// used in this case to pair enum of menu enum with a list of menu objects
    /// </summary>
    [System.Serializable]
    public class ListOfDamageStates : InspectorDictionary<float, GameObject> { }
    [SerializeField]
    protected ListOfDamageStates m_damageStates = new ListOfDamageStates();
    public ListOfDamageStates DamageStates { get { return m_damageStates; } }


    #endregion working inspector dictionary

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {

        m_currentModel = DamageStates[1.0f];
    }

    // Update is called once per frame
    void Update()
    {
        float curhp = m_healthModule.HealthPercentage;

        //while(curhp >= )
        foreach (float flt in DamageStates.Keys)
        {
            //Debug.Log(flt + DamageStates[flt].name);
            if (curhp < flt)
            {
                //Debug.LogWarning(curhp + ": Switching States to " + DamageStates[flt]);
                m_currentModel = DamageStates[flt];
            }
            if (DamageStates[flt] != null)
                DamageStates[flt].SetActive(false);

        }
        if (m_currentModel != null)
            m_currentModel.SetActive(true);
    }
}
