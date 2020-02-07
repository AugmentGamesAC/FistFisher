using UnityEngine;
using UnityEngine.UI;

public class CoreUIElement : MonoBehaviour
{

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

