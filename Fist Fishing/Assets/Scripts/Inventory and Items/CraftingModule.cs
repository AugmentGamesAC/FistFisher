using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// old script that allowed player to turn coral into bait back when crafting was a planned feature
/// </summary>
public class CraftingModule : MonoBehaviour
{

    [SerializeField]
    protected PlayerMotion m_playerRef;
    public PlayerMotion PlayerRef { get { return m_playerRef; } }

    [SerializeField]
    protected Inventory m_inventoryRef;
    public Inventory InventoryRef { get { return m_inventoryRef; } }

    [SerializeField]
    protected PlayerMovement m_playerMovementRef;
    public PlayerMovement PlayerMovementRef { get { return m_playerMovementRef; } }


    public GameObject m_baitPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (m_playerRef == null)
            m_playerRef = gameObject.GetComponent<PlayerMotion>();
        if (m_inventoryRef == null)
            m_inventoryRef = gameObject.GetComponent<Inventory>();
        if (m_playerMovementRef == null)
            m_playerMovementRef = gameObject.GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        //Old code
        //if (ALInput.GetKeyDown(ALInput.CraftBait) && m_playerMovementRef.m_isMounted==true) //can craft if mounted
        //{
        //    CraftBait();
        //}
    }

    public void CraftBait()
    {            //make sure player has 1 of each coral
        GameObject c1 = m_inventoryRef.GetReferenceToStoredObjectOfHarvestableType(HarvestableType.Coral1);
        GameObject c2 = m_inventoryRef.GetReferenceToStoredObjectOfHarvestableType(HarvestableType.Coral2);
        if (c1 == null || c2 == null)
            return;

        GameObject newBait = ObjectPoolManager.Get(m_baitPrefab);
        if (newBait == null)
            return;


        m_inventoryRef.RemoveFromInventory(c1);
        m_inventoryRef.RemoveFromInventory(c2);

        m_inventoryRef.AddToInventory(newBait);
    }

}
