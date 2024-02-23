using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveableEntity : MonoBehaviour
{
    [SerializeField] private string uniqueIdentifier = "";

    //CACHED STATE
    static Dictionary<string, SaveableEntity> globalLookup = new Dictionary<string, SaveableEntity>();

    public object CaptureState()
    {
        Dictionary<string, object> state = new Dictionary<string, object>();
        foreach(ISaveable saveable in GetComponents<ISaveable>())
        {
            state[saveable.GetType().ToString()] = saveable.CaptureState();
        }

        return state;
    }

    public string GetUniqueIdentifier()
    {
        return uniqueIdentifier;
    }

    public void RestorState(object state)
    {
        Dictionary<string, object> stateDic = (Dictionary<string, object>)state;
        foreach(ISaveable saveable in GetComponents<ISaveable>())
        {
            string typeString = saveable.GetType().ToString();
            if(stateDic.ContainsKey(typeString))
            {
                saveable.RestoreState(stateDic[typeString]);
            }
        }
    }
}
