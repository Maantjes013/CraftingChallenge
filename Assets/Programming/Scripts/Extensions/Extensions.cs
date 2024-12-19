using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public static class Extensions
{
    /// <summary>
    ///     Destroys all children of this transform
    /// </summary>
    public static void DestroyChildren(this Transform transform)
    {
        foreach (Transform child in transform)
            Object.Destroy(child.gameObject);
    }

    /// <summary>
    ///     Disables all children of this transform
    /// </summary>
    public static void DisableChildren(this Transform transform)
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
    }
}