using System;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static Vector3 ProjectUp(this Vector3 v3)
    {
        return Vector3.ProjectOnPlane(v3, Vector3.up);
    }

    public static int RandomIndex(this List<GameObject> list)
    {
        if (list.Count == 0) 
            throw new Exception("List Is Empty");
        return UnityEngine.Random.Range(0, list.Count);
    }
}
