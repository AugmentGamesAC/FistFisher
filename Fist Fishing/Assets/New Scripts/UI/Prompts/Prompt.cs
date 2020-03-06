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

    private void Start()
    {
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;

        promptUpdater = FindObjectOfType<PromptUpdater>();

        if (promptUpdater == default)
            throw new System.Exception("did not find promptUpdater!");
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMotion check = other.GetComponent<PlayerMotion>();
        if (check == default)
            return;

        //show prompt image.
        m_inZone = true;
        promptUpdater.UpdateUI(this);
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerMotion check = other.GetComponent<PlayerMotion>();
        if (check == default)
            return;

        //stop updating this ui element
        m_inZone = false;
        promptUpdater.UpdateUI(null);
    }
}
