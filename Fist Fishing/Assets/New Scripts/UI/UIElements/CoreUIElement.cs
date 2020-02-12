using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// this is the base class for UI elements
/// it only needs to know if it's updating or not 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class CoreUIElement<T> : MonoBehaviour
{
    /// <summary>
    /// if the data it's tracking exists, it should show it
    /// </summary>
    /// <param name="newData"></param>
    /// <returns></returns>
    protected bool ShouldUpdateUI(T newData)
    {
        gameObject.SetActive(newData != null);
        return gameObject.activeSelf;
    }

    public abstract void UpdateUI(T newData);
}

