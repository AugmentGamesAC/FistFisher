using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RepellentData", menuName = "ScriptableObjects/Repellent", order = 1)]
public class Repellent_IItem : ScriptableObject, IItem
{
        [SerializeField]
        protected int m_stackSize;
        public int StackSize => m_stackSize;
        [SerializeField]
        protected int m_id;
        [SerializeField]
        public int ID => m_id;
        [SerializeField]
        protected int m_worth;
        public int WorthInCurrency => m_worth;
        [SerializeField]
        protected ItemType m_type;
        public ItemType Type => m_type;
        [SerializeField]
        protected string m_description;
        public string Description => m_description;
        [SerializeField]
        protected Sprite m_display;
        public Sprite IconDisplay => m_display;
        [SerializeField]
        protected string m_name;
        public string Name => m_name;
        [SerializeField]
        protected FishBrain.FishClassification m_currentRepellentType = FishBrain.FishClassification.RepellentStrength1;
        [SerializeField]
        public FishBrain.FishClassification RepellentType { get { return m_currentRepellentType; } set { m_currentRepellentType = value; } }

        [SerializeField]
        protected int m_activeTurnCount;
        public int TurnCount { get { return m_activeTurnCount; } set { m_activeTurnCount = value; } }
        public bool CanMerge(IItem newItem)
        {
            Repellent_IItem item = newItem as Repellent_IItem;
            if (item == default)
                return false;
            return m_currentRepellentType == item.m_currentRepellentType;
        }

        public bool ResolveDropCase(ISlotData slot, ISlotData oldSlot)
        {
            return false;
        }
    /// <summary>
    /// Decriments the active turn count of a bait that is out in combat
    /// </summary>
    /// <param name="value">The number of turns to decriment by</param>
    public void DecrimentActiveTurnCount()
    {
        TurnCount--;
    }
    public bool IsStillActive()
    {
        DecrimentActiveTurnCount();
        return TurnCount <= 0;
    }
}

