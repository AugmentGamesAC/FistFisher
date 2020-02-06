using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CombatInfo
{
    //Common class for both Player and Fish Combat info.
    //created so the combat manager can refer to generic Combat Info objects.

    void TakeDamage(float damage);
}
