using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SelectedMoveUI))]
public class AttackPinwheelUpdater : PinwheelUpdater<CombatMoveInfo>
{
    protected Dictionary<int, PinwheelTab> m_tabs;
    protected int m_currentSelection;

    private void Start()
    {
        m_currentSelection = 1;

        var tabs = GetComponentsInChildren<PinwheelTab>();
        m_tabs = new Dictionary<int, PinwheelTab>(tabs.Length);
        foreach (var tab in tabs)
            m_tabs.Add(tab.ID, tab);

        SetValue(m_currentSelection);
    }

    public void SetValue (int index)
    {
        if (index < 1 || index > m_tabs.Count)
            return;
       
        m_tracker.SetSelectedOption(index);
    }


    protected override void UpdateState(IPinWheel<CombatMoveInfo> value)
    {
        base.UpdateState(value);

        if (m_tabs == null)
            return;

        if (value.SelectedSlot < 1 || value.SelectedSlot > m_tabs.Count)
            return;

        m_tabs[m_currentSelection].SetSelected(false);
        m_currentSelection = value.SelectedSlot;
        m_tabs[value.SelectedSlot].SetSelected(true);
    }  
    

}
