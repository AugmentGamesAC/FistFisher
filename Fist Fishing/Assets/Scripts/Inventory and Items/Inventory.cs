using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    public int m_fishCount = 0;
    public int m_coral1Count = 0;
    public int m_coral2Count = 0;
    public int m_BaitCount = 0;

    public InventoryObject m_displayInventoryObject;

    public FishItem m_AntEaterScriptableObject;
    public FishItem m_YellowScriptableObject;
    public FishItem m_RhynoScriptableObject;
    public BaitItem m_baitScriptableObject;
    public Coral1Item m_coral1ScriptableObject;
    public Coral2Item m_coral2ScriptableObject;

    #region Currency
    //currency has to be an unsigned long. I need to imagine people are insane enough to 
    [SerializeField]
    protected ulong m_currentCurrency = 0;
    public ulong CurrentCurrency { get { return m_currentCurrency; } }

    ulong ConvertToULong(int amount)
    {
        int absAmount = Mathf.Abs(amount); //just to super-double-check it's not negative, as that can mess with casting as unsigned
        ulong longified = Convert.ToUInt64(absAmount);
        return longified;
    }

    /// <summary>
    /// removes currency. 
    /// True: reduced properly
    /// False: value was larger than what user has or was given negative value
    /// </summary>
    public bool SpendMoney(int amount)
    {
        if (amount < 0) //if you're not spending money, fail
            return false;
        ulong amountConverted = ConvertToULong(amount);
        if (amountConverted > CurrentCurrency) //if you're spending more than you have, fail
            return false;
        m_currentCurrency -= amountConverted; //otherwise, spend monies
        //RemoveAmount(AItem item, int amount);
        return true;
    }

    /// <summary>
    /// adds currency. 
    /// True: money added
    /// False: money was now maxed or was given negative value
    /// </summary>
    public bool GainMoney(int amount)
    {
        if (amount < 0) //can't gain a negative here
            return false;
        ulong amountConverted = ConvertToULong(amount);
        if (m_currentCurrency == ulong.MaxValue) //if already maxed, don't bother
            return false;

        ulong sum = m_currentCurrency + amountConverted;

        if ((m_currentCurrency > 0 && amountConverted > 0) &&
            (sum < m_currentCurrency || sum < amountConverted)) //if each value is greater than 0, and the sum is somehow less than either of them, we have an overflow issue
            m_currentCurrency = ulong.MaxValue; //if we're going to overflow, but we're gaining something, may as well just use this to max monies
        else
            m_currentCurrency = sum;

        //m_displayInventoryObject.AddItem(m_currencyScriptableObject, 1);

        return true;
    }
    #endregion Currency

    #region ObjectTracking

    [SerializeField]
    protected List<GameObject> m_storedObjects = new List<GameObject>();
    public List<GameObject> StoredObjects { get { return m_storedObjects; } }

    /// <summary>
    /// tries to put the object into inventory. 
    /// Fails if it isn't a harvestable or already somehow in inventory
    /// </summary>
    public bool AddToInventory(GameObject obj)
    {
        ItemWorth iw = obj.GetComponent<ItemWorth>();

        if (m_storedObjects.Contains(obj))
            return false;


        if (m_displayInventoryObject != null)
            ResolveCountUpdates(obj);

        m_storedObjects.Add(obj);

        obj.SetActive(false);

        return true;
    }

    protected void ResolveCountUpdates(GameObject obj)
    {
        if (IsABait(obj))
        {
            m_BaitCount++;
            m_displayInventoryObject.AddItem(m_baitScriptableObject, 1);//Use delegate and event in future for every time a inventory item gets changed.
        }
        else if (IsAHarvestable(obj))
        {
            Harvestable test = obj.GetComponent<Harvestable>();

            HarvestableType hType = test.m_harvestableType;
            switch (hType)
            {
                case HarvestableType.DeadFish:
                    {
                        FishBrain.FishClassification FishClass = test.gameObject.GetComponentInChildren<BasicFish>().FishClass;
                        if (FishClass == FishBrain.FishClassification.Fearful)
                        {
                            m_displayInventoryObject.AddItem(m_YellowScriptableObject, 1);
                        }
                        else if (FishClass == FishBrain.FishClassification.Agressive)
                        {
                            m_displayInventoryObject.AddItem(m_RhynoScriptableObject, 1);
                        }
                        else if (FishClass == FishBrain.FishClassification.Passive)
                        {
                            m_displayInventoryObject.AddItem(m_AntEaterScriptableObject, 1);
                        }
                    }
                    m_fishCount++;
                    break;
                case HarvestableType.Coral1:
                    m_coral1Count++;
                    m_displayInventoryObject.AddItem(m_coral1ScriptableObject, 1);
                    break;
                case HarvestableType.Coral2:
                    m_coral2Count++;
                    m_displayInventoryObject.AddItem(m_coral2ScriptableObject, 1);
                    break;
                case HarvestableType.NotSet:
                    return ;
            }
        }
        else
        {
            return ;
        }

    }


    /// <summary>
    /// removes a given gameobject from inventory
    /// fails if object is not valid or not in inventory
    /// </summary>
    public bool RemoveFromInventory(GameObject obj)
    {
        /*if (!IsAHarvestable(obj) && !IsABait(obj))
            return false;*/

        if (!m_storedObjects.Contains(obj))
            return false;

        if (IsABait(obj))
        {
            m_BaitCount--;
            m_displayInventoryObject.RemoveAmount(m_baitScriptableObject, 1);
        }
        else if (IsAHarvestable(obj))
        {
            Harvestable test = obj.GetComponent<Harvestable>();

            HarvestableType hType = test.m_harvestableType;
            switch (hType)
            {
                case HarvestableType.DeadFish:
                    {
                        FishBrain.FishClassification FishClass = test.gameObject.GetComponentInChildren<BasicFish>().FishClass;
                        if (FishClass == FishBrain.FishClassification.Fearful)
                        {
                            m_displayInventoryObject.RemoveAmount(m_YellowScriptableObject, 1);
                        }
                        else if (FishClass == FishBrain.FishClassification.Agressive)
                        {
                            m_displayInventoryObject.RemoveAmount(m_RhynoScriptableObject, 1);
                        }
                        else if (FishClass == FishBrain.FishClassification.Passive)
                        {
                            m_displayInventoryObject.RemoveAmount(m_AntEaterScriptableObject, 1);
                        }
                    }
                    m_fishCount--;
                    break;
                case HarvestableType.Coral1:
                    m_coral1Count--;
                    m_displayInventoryObject.RemoveAmount(m_coral1ScriptableObject, 1);
                    break;
                case HarvestableType.Coral2:
                    m_coral2Count--;
                    m_displayInventoryObject.RemoveAmount(m_coral2ScriptableObject, 1);
                    break;
                case HarvestableType.NotSet:
                    return false;
            }

        }
        else
        {
            return false;
        }

        m_storedObjects.Remove(obj);

        return true;
    }

    public GameObject GetReferenceToStoredObjectOfHarvestableType(HarvestableType hType)
    {
        if (m_storedObjects.Count == 0)
            return null;

        foreach (GameObject g in m_storedObjects)
        {
            if (IsAHarvestable(g) == true)
            {
                HarvestableType objtype = g.GetComponent<Harvestable>().m_harvestableType;
                if (objtype == hType)
                    return g;
            }
        }
        return null;
    }
    public GameObject GetReferenceToStoredBait()
    {
        if (m_storedObjects.Count == 0 || m_BaitCount == 0)
            return null;

        foreach (GameObject g in m_storedObjects)
        {
            if (IsABait(g) == true)
            {
                return g;
            }
        }
        return null;
    }

    private bool IsAHarvestable(GameObject obj)
    {
        Harvestable test = obj.GetComponent<Harvestable>();
        if (test == null)
            return false;
        return true;
    }
    private bool IsABait(GameObject obj)
    {
        Bait test = obj.GetComponent<Bait>();
        if (test == null)
            return false;
        return true;
    }


    #endregion ObjectTracking


    private void OnApplicationQuit()
    {
        m_displayInventoryObject.m_inventorySlots = new InventorySlot[32];
    }
}
