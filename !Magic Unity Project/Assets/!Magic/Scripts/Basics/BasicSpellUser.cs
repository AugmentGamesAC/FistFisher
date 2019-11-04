using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpellUser : ASpellUser
{
    [SerializeField]
    protected Transform m_AimingPoint;
    public override Transform Aiming
    {
        get
        {
            if (m_AimingPoint == default(Transform))
                m_AimingPoint = transform;
            return transform;
        }
    }

    public override bool IsDead() 
    {
        if(m_ShieldCurrent<=0.0f)
            return true;
        return false;
    }

    /*public override void ModifyHealth(float change) //take damage
    {

    }*/

    private void Update()
    {
        ModifyMana(ManaRegen * Time.deltaTime); //mana regen
        ModifyShield(ShieldRegen * Time.deltaTime); //this should do nothing, but here in the event it does later
    }


}
