using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicNotMagicUser : ANotMagicUser
{
    public override Transform Aiming => throw new System.NotImplementedException();
}
