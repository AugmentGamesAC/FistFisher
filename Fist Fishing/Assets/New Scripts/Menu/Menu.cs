using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Menu : MonoBehaviour
{
    /// <summary>
    /// Set visibility of this
    /// may be more logic in future.
    /// </summary>
    public void Show(bool activeState)
    {
        gameObject.SetActive(activeState);
    }

    public void Awake()
    {
        Show(false);
    }
}
