using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedFishUI : MonoBehaviour
{
    [SerializeField]
    protected PsudoFishUIData MyPsudoData;

    [SerializeField]
    protected FloatTextUpdater DistanceDisplay;



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
        DistanceDisplay.UpdateTracker(MyPsudoData.Distance);

        //

        
    }
}

[System.Serializable]
public class PsudoFishUIData
{
    [SerializeField]
    protected FloatTracker m_distance;
    public FloatTracker Distance => m_distance;

    public PsudoFishUIData(GameObject obj)
    {
        m_distance = obj.AddComponent<FloatTracker>();
    }
}
