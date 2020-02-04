using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedFishUI : MonoBehaviour
{
    [SerializeField]
    protected PsudoFishUIData MyPsudoData;

    [SerializeField]
    protected FloatTextUpdater EnemyDistanceDisplay;
    [SerializeField]
    protected TextUpdater EnemyNameDisplay;
    [SerializeField]
    protected ImageUpdater EnemyTypeImageDisplay;
    [SerializeField]
    protected ImageUpdater EnemyIconDisplay;
    [SerializeField]
    protected FloatTextUpdater EnemyHealthNumberDisplay;
    [SerializeField]
    protected FloatTextUpdater EnemySwimSpeedDisplay;

    
    [ContextMenu("SwapData")]
    public void newPsudoData()
    {
        //Create new fish data
        PsudoFishUIData newPsudoFishData = new PsudoFishUIData(this.gameObject);

        UpdateUI(newPsudoFishData);

    }

    public void UpdateUI(PsudoFishUIData newData)
    {
        //out with old
        MyPsudoData = newData;
        EnemyDistanceDisplay.UpdateTracker(MyPsudoData.Distance);
        EnemyNameDisplay.UpdateTracker(MyPsudoData.Name);
        EnemyTypeImageDisplay.UpdateTracker(MyPsudoData.TypeImage);
        EnemyIconDisplay.UpdateTracker(MyPsudoData.IconImage);
        EnemyHealthNumberDisplay.UpdateTracker(MyPsudoData.HealthText);
        EnemySwimSpeedDisplay.UpdateTracker(MyPsudoData.SwimSpeed);
        //


    }
}

[System.Serializable]
public class PsudoFishUIData
{
    [SerializeField]
    protected FloatTracker m_distance;
    public FloatTracker Distance => m_distance;
    [SerializeField]
    protected TextTracker m_name;
    public TextTracker Name => m_name;
    [SerializeField]
    protected ImageTracker m_typeImage;
    public ImageTracker TypeImage => m_typeImage;
    [SerializeField]
    protected ImageTracker m_iconImage;
    public ImageTracker IconImage => m_iconImage;
    [SerializeField]
    protected FloatTracker m_healthNumber;
    public FloatTracker HealthText => m_healthNumber;
    [SerializeField]
    protected FloatTracker m_swimSpeed;
    public FloatTracker SwimSpeed => m_swimSpeed;


    public PsudoFishUIData(GameObject obj)
    {
        m_distance = obj.AddComponent<FloatTracker>();
        m_name = obj.AddComponent<TextTracker>();
        m_typeImage = obj.AddComponent<ImageTracker>();
        m_iconImage = obj.AddComponent<ImageTracker>();
        m_healthNumber = obj.AddComponent<FloatTracker>();
        m_swimSpeed = obj.AddComponent<FloatTracker>();



    }
}
