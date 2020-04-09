using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// applied to water object
/// tells us if player is in the water or not
/// </summary>
public class Water : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            PlayerInstance.Instance.Oxygen.m_isUnderWater = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            PlayerInstance.Instance.Oxygen.m_isUnderWater = false;
    }
}
