using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpellUser : ASpellUser
{
    public override Transform Aiming => throw new System.NotImplementedException();

    public override bool IsDead() 
    {
        if(m_ShieldCurrent<=0.0f)
            return true;
        return false;
    }

    /*public override void TakeDamage(float change)
    {

    }*/

    private void Update()
    {
        ModifyMana(ManaRegen);
    }


}
