using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Show and hide menu
/// </summary>

public class Menu : MonoBehaviour
{
    /// <summary>
    /// Set visibility of this
    /// </summary>
    /// <param name="activeState"></param>
    public void Show(bool activeState)
    {
        
        gameObject.SetActive(activeState);

    }

}
