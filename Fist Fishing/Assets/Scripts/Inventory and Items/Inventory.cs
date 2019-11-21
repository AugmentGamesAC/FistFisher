using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{

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

        return true;
    }
    #endregion Currency

    #region HarvestableTracking

    [SerializeField]
    protected List<GameObject> m_storedObjects = new List<GameObject>();
    public List<GameObject> StoredObjects { get { return m_storedObjects; } }

    /// <summary>
    /// tries to put the object into inventory. 
    /// Fails if it isn't a harvestable or already somehow in inventory
    /// </summary>
    public bool AddToInventory(GameObject obj)
    {
        if (!IsAHarvestable(obj))
            return false;

        if (m_storedObjects.Contains(obj))
            return false;

        m_storedObjects.Add(obj);
        //obj.SetActive(false); //wait, I can't just set not active, or the object pooling messes with this. argh.

        /*******************************************************************************************************************************/
        //TEMPORARY MEASURE TILL WE FIGURE OUT UI
        /*******************************************************************************************************************************/

        Vector3 pos = obj.transform.position;
        pos.y = -999.9f;
        obj.transform.position = pos;

        /*******************************************************************************************************************************/
        /*******************************************************************************************************************************/
        /*******************************************************************************************************************************/

        return true;
    }

    /// <summary>
    /// removes a given gameobject from inventory
    /// fails if object is not valid or not in inventory
    /// </summary>
    public bool RemoveFromInventory(GameObject obj)
    {
        if (!IsAHarvestable(obj))
            return false;

        if (!m_storedObjects.Contains(obj))
            return false;

        m_storedObjects.Remove(obj);

        return true;
    }

    private bool IsAHarvestable(GameObject obj)
    {
        Harvestable test = obj.GetComponent<Harvestable>();
        if (test == null)
            return false;
        return true;
    }


    #endregion HarvestableTracking

    #region BaitTracking

    [SerializeField]
    protected List<GameObject> m_storedBait = new List<GameObject>();
    public List<GameObject> StoredBait { get { return m_storedBait; } }

    /// <summary>
    /// tries to put the object into inventory. 
    /// Fails if it isn't a harvestable or already somehow in inventory
    /// </summary>
    public bool AddToBaitInventory(GameObject obj)
    {
        if (!IsABait(obj))
            return false;

        if (m_storedBait.Contains(obj))
            return false;

        m_storedBait.Add(obj);
        //obj.SetActive(false); //wait, I can't just set not active, or the object pooling messes with this. argh.

        /*******************************************************************************************************************************/
        //TEMPORARY MEASURE TILL WE FIGURE OUT UI
        /*******************************************************************************************************************************/

        Vector3 pos = obj.transform.position;
        pos.y = -999.9f;
        obj.transform.position = pos;

        /*******************************************************************************************************************************/
        /*******************************************************************************************************************************/
        /*******************************************************************************************************************************/

        return true;
    }

    /// <summary>
    /// removes a given gameobject from inventory
    /// fails if object is not valid or not in inventory
    /// </summary>
    public bool RemoveBaitFromInventory(GameObject obj)
    {
        if (!IsABait(obj))
            return false;

        if (!m_storedBait.Contains(obj))
            return false;

        m_storedBait.Remove(obj);

        return true;
    }

    private bool IsABait(GameObject obj)
    {
        Bait test = obj.GetComponent<Bait>();
        if (test == null)
            return false;
        return true;
    }


    #endregion BaitTracking


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
