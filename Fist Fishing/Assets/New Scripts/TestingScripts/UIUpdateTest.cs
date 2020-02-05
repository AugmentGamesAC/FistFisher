using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpdateTest : MonoBehaviour
{
    [SerializeField]
    protected SelectedFishUI selectedFishUI;

    [SerializeField]
    protected FishCombatInfo fish;

    private void Start()
    {
        selectedFishUI.UpdateUI(fish);
    }
}
