using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnable
{
    GameObject Instatiate(MeshCollider m);
}
