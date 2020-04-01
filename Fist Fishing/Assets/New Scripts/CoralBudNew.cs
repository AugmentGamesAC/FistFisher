using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralBudNew : MonoBehaviour, IDyingThing
{
    protected CoralBudDefinition m_definitionForThis;
    public bool SetDefinition(CoralBudDefinition def)
    {
        if (def == null)
            return false;
        m_definitionForThis = def;
        //m_coralItemData = m_definitionForThis.ItemData;
        return true;
    }


    public event Death Death;
    public void Die()
    {
        this.gameObject.SetActive(false);
        Death?.Invoke();
    }


}
