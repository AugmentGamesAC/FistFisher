using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Common class for both Player and Fish Combat info.
/// created so the combat manager can refer to generic Combat Info objects.
/// </summary>
public interface CombatInfo
{
    void TakeDamage(float damage);
}
