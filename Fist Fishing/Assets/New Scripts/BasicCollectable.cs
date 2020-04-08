using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// base collectable type with no additional behaviours
/// just allows for item to be spawned, harvested, and added to inventory
/// </summary>
public class BasicCollectable : MonoBehaviour, IDyingThing
{
    [SerializeField]
    protected BasicCollectableDefinition m_definitionForThis;

    public bool SetDefinition(BasicCollectableDefinition def)
    {
        if (def == null)
            return false;
        m_definitionForThis = def;
        return true;
    }

    public void Start()
    {
        Harvestable h;
        if (gameObject.TryGetComponent<Harvestable>(out h))
            Destroy(h);
        h = gameObject.AddComponent<Harvestable>();
        h.TransferProperties(m_definitionForThis.ItemData);

    }



    public event Death Death;
    public void Die()
    {
        this.gameObject.SetActive(false);
        Death?.Invoke();
    }


    [ContextMenu("Magically Harvest This")]
    public void GoGetHarvested()
    {
        Harvestable h = gameObject.GetComponent<Harvestable>();
        PlayerInstance.Instance.PlayerInventory.AddItem(h, 1);
        Die();
    }
}
