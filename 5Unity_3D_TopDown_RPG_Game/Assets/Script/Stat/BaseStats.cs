using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStats : MonoBehaviour
{
    [Range(1, 99)] 
    [SerializeField] int startingLevel = 1;
    [SerializeField] CharacterClass characterClass;
    [SerializeField] Progression progression = null;

    LazyValue<int> currentLevel;

    private void Awake()
    {
        currentLevel = new LazyValue<int>(CalculateLevel);
    }
   

    // Start is called before the first frame update
    void Start()
    {
        currentLevel.ForceInit();
    }

    public float GetStat(Stats stats)
    {
        return GetBaseStat(stats);
    }

    private float GetBaseStat(Stats stats)
    {
        return progression.GetStat(stats, characterClass, GetLevel());
    }

    private int GetLevel()
    {
        return currentLevel.value;
    }

    private int CalculateLevel()
    {
        return startingLevel;
    }
}
