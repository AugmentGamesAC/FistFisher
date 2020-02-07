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

    protected void MemberUpdate(ImageUpdater updater, Sprite sprite)
    {
        updater.ForceUpdate(sprite);
    }

    protected void MemberUpdate(TextUpdater updater, string text)
    {
        updater.ForceUpdate(text);
    }
    protected void MemberUpdate(FloatTextUpdater updater, string format, float value)
    {
        updater.SetFormatter(format);
        updater.ForceUpdate(value);
    }
    protected void MemberUpdate(FloatTextUpdater updater, float value)
    {
        updater.ForceUpdate(value);
    }
}

