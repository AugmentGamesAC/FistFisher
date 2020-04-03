using UnityEngine;
using UnityEngine.UI;

public abstract class CoreUIElement<T> : MonoBehaviour
{
    /// <summary>
    /// Should Update UI allows for overriding checks to be passed in 
    /// </summary>
    /// <param name="newData"></param>
    /// <param name="additionalCheck"></param>
    /// <returns></returns>
    protected bool ShouldUpdateUI(T newData, System.Func<T,bool> additionalCheck = default)
    {
        gameObject.SetActive(newData != null && ((additionalCheck == default)? true: additionalCheck(newData)));
        return gameObject.activeSelf;
    }

    public abstract void UpdateUI(T newData);
}

