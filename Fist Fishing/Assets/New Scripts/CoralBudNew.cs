using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// new script to apply to coral buds to handle their harvesting
/// </summary>
public class CoralBudNew : MonoBehaviour, IDyingThing
{
    protected CoralBudDefinition m_definitionForThis;
    protected CoralNew m_parent;
    public void SetParent(CoralNew coral)
    {
        m_parent = coral;
    }

    public bool SetDefinition(CoralBudDefinition def)
    {
        if (def == null)
            return false;
        m_definitionForThis = def;
        //m_coralItemData = m_definitionForThis.ItemData;
        return true;
    }


    [ContextMenu("Magically Harvest This")]
    public void GoGetHarvested()
    {
        Harvestable h = gameObject.GetComponent<Harvestable>();
        PlayerInstance.Instance.PlayerInventory.AddItem(h, 1);
        m_parent.BudDied();
        Die();
    }

    public event Death Death;
    public void Die()
    {
        this.gameObject.SetActive(false);
        Death?.Invoke();
    }


}
