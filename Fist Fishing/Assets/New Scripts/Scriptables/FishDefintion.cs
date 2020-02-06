using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDefintion : ScriptableObject, IFishData, IItem
{
    #region IFishData
    public IItem Item => this;
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
    [SerializeField]
    protected Sprite m_IconDisplay;
    public Sprite IconDisplay => m_IconDisplay;
    #endregion
    #region IItem

    [SerializeField]
    protected int m_stackSize; 
    public int StackSize => m_stackSize;
    [SerializeField]
    protected int m_iD;
    public int ID => m_iD;
    [SerializeField]
    protected int m_worthInCurrency;
    public int WorthInCurrency => m_worthInCurrency;
    [SerializeField]
    protected ItemType m_Type;
    public ItemType Type => m_Type;
    [SerializeField]
    protected string m_Description;
    public string Description => m_Description;
    #endregion

    #region ModelReferences
    [SerializeField]
    protected Mesh m_BaseModelRefence;
    [SerializeField]
    protected GameObject m_BasicFish; 
    [SerializeField]
    protected GameObject m_swimingHPDisplayRefence;
    #endregion

    public GameObject InstatiateFish()
    {
        GameObject FishRoot = ObjectPoolManager.Get(m_BasicFish);
        CoreFish coreFish = FishRoot.GetComponent<CoreFish>();
        coreFish.Init(this, this);
        //TODO: set fishproperites NoteNewFishClass will need to set the required values and support the same interface
        GameObject HPRoot = ObjectPoolManager.Get(m_swimingHPDisplayRefence);
        HPRoot.transform.SetParent(FishRoot.transform);

        HPRoot.GetComponentInChildren<ProgressBarUpdater>().UpdateTracker(coreFish.Health.CurrentAmount);



        throw new System.NotImplementedException();
    }

    public void ConfigFish(FishBrain.FishClassification classification)
    {
        m_fishClassification = classification;
        m_Type = ItemType.Fish;
        m_Description = "Funny not Found";
        m_worthInCurrency = 100;
        m_Health = new FishHealth();
        m_combatSpeed = 4;
    }
}
