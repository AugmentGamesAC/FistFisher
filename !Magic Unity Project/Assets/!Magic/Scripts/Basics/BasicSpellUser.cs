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

    /*public override void ModifyHealth(float change) //take damage
    {

    }*/

    private void Update()
    {
        ModifyMana(ManaRegen); //mana regen
        ModifyShield(ShieldRegen); //this should do nothing, but here in the event it does later
    }


}
