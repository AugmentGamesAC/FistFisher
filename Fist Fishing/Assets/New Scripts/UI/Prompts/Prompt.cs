using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable, RequireComponent(typeof(Collider))]
public class Prompt : MonoBehaviour
{
    [SerializeField]
    protected bool m_inZone = false;
    public bool InZone => m_inZone;

    [SerializeField]
    protected ImageTracker m_display = new ImageTracker();
    public ImageTracker Display => m_display;

    [SerializeField]
    protected TextTracker m_description = new TextTracker();
    public TextTracker Description => m_description;

    protected PromptUpdater promptUpdater;

    protected Collider m_collider;

    private void Start()
    {
        if (promptUpdater == default)
            promptUpdater = FindObjectOfType<PromptUpdater>();//doesn't work because it's in Dont destroy on load.

        if (promptUpdater == default)
            throw new System.Exception("did not find promptUpdater!");

        m_collider = GetComponent<Collider>();
        if (m_collider == null)
            return;

        m_collider.isTrigger = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMotion>() == default)
            return;

        //show prompt image.
        m_inZone = true;
        promptUpdater.UpdateUI(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMotion>() == default)
            return;

        //stop updating this ui element
        m_inZone = false;
        promptUpdater.UpdateUI(null);
    }
}
