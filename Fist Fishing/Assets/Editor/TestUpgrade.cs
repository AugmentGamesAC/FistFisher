using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class TestUpgrade : Upgrade
{
    [Test]
    public void Construct_test()
    {
        statManager = new PlayerStatManager();
        cost = 50.0f;

        //setup stat mods, this should be done.

    }
}
