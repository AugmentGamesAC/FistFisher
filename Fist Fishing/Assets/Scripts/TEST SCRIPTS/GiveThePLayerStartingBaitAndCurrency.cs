using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveThePlayerStartingBaitAndCurrency : MonoBehaviour
{

    public int m_startingCurrency = 1337;
    public int m_startingBaitAmount = 5;


    // Start is called before the first frame update
    void Start()
    {
        Init();   
    }

    void Init()
    {
        Inventory i = gameObject.GetComponent<Inventory>();
        i.GainMoney(m_startingCurrency);

        CraftingModule c = gameObject.GetComponent<CraftingModule>();

        for (int o = 0; o < m_startingBaitAmount; o++)
        {
            GameObject newBait = ObjectPoolManager.Get(c.m_baitPrefab);
            i.AddToInventory(newBait);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
