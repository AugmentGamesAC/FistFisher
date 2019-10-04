using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeTester : MonoBehaviour
{
    public CraftingStation CS;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            CS.SpawnCrystal();
        }
    }
}
