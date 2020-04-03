using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpawningTweaks
{
    public static void AdjustForBottom(GameObject g)
    {
        if (g == default)
            return;
        Collider c = g.gameObject.GetComponent<Collider>();
        if (c == default)
            return;
        Bounds b = c.bounds;
        if (b == default)
            return;
        Vector3 o = g.transform.position;
        Vector3 offset = b.extents;
        o.y += offset.y;
        g.transform.position = o;
    }
}