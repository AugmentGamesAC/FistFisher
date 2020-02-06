using UnityEngine;
public class CoreFish: MonoBehaviour, IFishData
{
    #region IFishData
    [SerializeField]
    protected float m_damage;
    public float Damage => m_damage;

    [SerializeField]
    protected float m_combatSpeed;
    public float CombatSpeed => m_combatSpeed;
    [SerializeField]
    protected float m_attackRange;
    public float AttackRange => m_attackRange;

    [SerializeField]
    protected FishHealth m_Health;
    public FishHealth Health => m_Health;

    [SerializeField]
    protected FishBrain.FishClassification m_fishClassification;
    public FishBrain.FishClassification FishClassification => m_fishClassification;
    #endregion
    #region IFishData IItem Overlap
    public Sprite IconDisplay => m_item.IconDisplay;

    public IItem Item => m_item;
    #endregion
    protected IItem m_item;
    
    public void Init(IFishData fishData, IItem item)
    {
        m_item = item;
        throw new System.NotImplementedException();
    }

}
