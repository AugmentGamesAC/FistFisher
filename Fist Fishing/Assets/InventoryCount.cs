using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCount : MonoBehaviour
{

    public GameObject m_InventoryCounter;

    public Inventory m_PlayerInventory;

    public GameObject m_FishCount;

    public GameObject m_BaitCount;

    public GameObject m_BudType1;

    public GameObject m_BudType2;

    


    // Start is called before the first frame update
    void Start()
    {
        m_FishCount.GetComponent<Text>().text = m_PlayerInventory.m_fishCount.ToString();
        m_BaitCount.GetComponent<Text>().text = m_PlayerInventory.m_BaitCount.ToString();
        m_BudType1.GetComponent<Text>().text = m_PlayerInventory.m_coral1Count.ToString();
        m_BudType2.GetComponent<Text>().text = m_PlayerInventory.m_coral2Count.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        m_FishCount.GetComponent<Text>().text = m_PlayerInventory.m_fishCount.ToString();
        m_BaitCount.GetComponent<Text>().text = m_PlayerInventory.m_BaitCount.ToString();
        m_BudType1.GetComponent<Text>().text = m_PlayerInventory.m_coral1Count.ToString();
        m_BudType2.GetComponent<Text>().text = m_PlayerInventory.m_coral2Count.ToString();
    }
}
