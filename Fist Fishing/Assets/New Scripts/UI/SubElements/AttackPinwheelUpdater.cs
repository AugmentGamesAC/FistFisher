using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPinwheelUpdater : PinwheelUpdater<CombatMoveInfo>
{
    [SerializeField]
    protected SelectedMoveUI selectedMoveUI;

    private void Start()
    {
        m_UIElement = selectedMoveUI;
    }
}
