using UnityEngine;
using UnityEngine.UI;

public abstract class CoreUIElement<T> : MonoBehaviour
{

    protected bool ShouldUpdateUI(T newData)
    {
        gameObject.SetActive(newData != null);
        return gameObject.activeSelf;
    }

    public abstract void UpdateUI(T newData);
}

