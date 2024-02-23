using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public static class GameObjectExtentions
{ 
    public static T GetAroundComponent<T>(this GameObject obj) where T : Component
    {
        T t = null;
        t = obj.GetComponentInParent<T>();
        if (t != null) return t;

        t = obj.GetComponentInChildren<T>();
        if (t != null) return t;

        t = obj.GetComponent<T>();
        if (t != null) return t;

        return null;

    }
}
