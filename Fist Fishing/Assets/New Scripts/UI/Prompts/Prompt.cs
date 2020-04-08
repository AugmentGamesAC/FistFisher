using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// base for prompts
/// contextually visible UI elements
/// </summary>
[System.Serializable]
public class Prompt : MonoBehaviour
{
    /// <summary>
    /// Lower the number more likely it will appear.
    /// 0 is reserved as a null option
    /// </summary>
    [SerializeField]
    protected int m_priority;
    public int Priority => m_priority;

    [SerializeField]
    protected Sprite m_display;
    public Sprite Display => m_display;

    [SerializeField]
    protected string m_description;
    public string Description => m_description;

    protected Collider m_collider;

    public delegate void CombatStarts();
    public event CombatStarts OnCombatStart;

    public virtual void Init(Sprite sprite, string desc, int priority = 1)
    {
        m_display = sprite;
        m_description = desc;
        m_priority = priority;

        m_collider = GetComponent<Collider>();
        m_collider.isTrigger = true;

        OnCombatStart += Prompt_OnCombatStart;
    }

    private void Prompt_OnCombatStart()
    {
        PlayerInstance.Instance.PromptManager.DeregisterPrompt(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMotion>() == default)
            return;

        PlayerInstance.Instance.PromptManager.RegisterPrompt(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMotion>() == default)
            return;
        PlayerInstance.Instance.PromptManager.DeregisterPrompt(this);
    }
}
