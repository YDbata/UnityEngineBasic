using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeristentObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject persistentObjectPrefab;
    static bool hasSpqaned = false;

    private void Awake()
    {
        if (hasSpqaned) return;
        SpawnPeristentObjects();
        hasSpqaned=true;
    }

    private void SpawnPeristentObjects()
    {
        GameObject persistentObject = Instantiate(persistentObjectPrefab);
        DontDestroyOnLoad(persistentObject);
    }

}