using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// helper functions for spawning
/// </summary>
public static class SpawningTweaks
{
    /// <summary>
    /// if object is meant to spawn from the bottom rather than middle, get bounding bod and add y value to gameobject
    /// </summary>
    /// <param name="g"></param>
    public static void AdjustForBottom(GameObject g)
    {
        bool collider = true;
        if (g == null)
            return;
        Collider c = g.gameObject.GetComponentInChildren<Collider>();
        if (c == null)
        {
            collider = false;
            MeshFilter f = g.gameObject.GetComponentInChildren<MeshFilter>();
            if (f == null)
                return;
            c = f.gameObject.AddComponent<MeshCollider>();
            MeshCollider mc = c as MeshCollider;
            mc.sharedMesh = f.sharedMesh;
            c = mc;
        }
        Bounds b = c.bounds;
        Vector3 o = g.transform.position;
        Vector3 offset = b.extents;

        o.y += offset.y;
        g.transform.position = o;

        if (!collider)
            c.enabled = false;
    }
}