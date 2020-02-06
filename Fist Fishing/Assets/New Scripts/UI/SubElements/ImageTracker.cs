using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ImageTracker : MonoUITracker<Sprite>
{
    private void Start()
    {
        if (m_value == null)
        {
            m_value = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/New Scripts/UI/TESTIMAGES/redtest.png");
        }
    }
}
