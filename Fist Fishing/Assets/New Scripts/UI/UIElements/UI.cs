using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackerTracker : MonoBehaviour
{
    [SerializeField]
    protected List<MonoUITracker<float>> m_floatTrackerList = new List<MonoUITracker<float>>();
    [SerializeField]
    protected List<MonoUITracker<Text>> m_textTracketList = new List<MonoUITracker<Text>>();
    [SerializeField]
    protected List<MonoUITracker<RawImage>> m_rawImageTrackerList = new List<MonoUITracker<RawImage>>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
