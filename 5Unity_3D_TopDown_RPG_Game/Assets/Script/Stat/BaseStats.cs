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
    Experience experience;
    private void Awake()
    {
        currentLevel = new LazyValue<int>(CalculateLevel);
        experience = GetComponent<Experience>();
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

    private void OnEnable()
    {
        if(experience != null)
        {
            experience.onExperienceGained += UpdateLevel;
        }
    }

    private void OnDisable()
    {
        if (experience != null)
        {
            experience.onExperienceGained -= UpdateLevel;
        }
    }

    private void UpdateLevel()
    {
        int newLevel = CalculateLevel();
        if(newLevel > currentLevel.value)
        {
            currentLevel.value = newLevel;
            Debug.Log(currentLevel.value);
        }
    }

    private int CalculateLevel()
    {
        if(experience == null) { return startingLevel; }
        float currentEXP = experience.GetPoints();


        int penultimateLevel = progression.GetLevels(Stats.ExperienceToLevelUp, characterClass);

        for(int level = 1;level <= penultimateLevel;level++)
        {
            float EXPToLevelUp = progression.GetStat(Stats.ExperienceToLevelUp,
                characterClass, level);
            if(EXPToLevelUp > currentEXP) return level;

        }
        return penultimateLevel + 1;
    }
}
