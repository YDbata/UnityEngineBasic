using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[CreateAssetMenu(fileName ="Progression", menuName ="Stats/New Progression", order = 0)]
public class Progression : ScriptableObject
{
    // characterClasses 직업클래스와 그에따른 스탯정보를 저장
    [SerializeField] ProgressionCharacterClass[] characterClasses = null;

    // lookupTable 스탯 및 직업 클래스에 대한 정보를 미리 계산하여 저장하는 Dictionary입니다.
    Dictionary<CharacterClass, Dictionary<Stats, float[]>> lookupTable = null;

    // 특정스탯의 특정 직업 클래스 및 레벨에 해당하는 스탯값을 반환
    public float GetStat(Stats stats, CharacterClass characterClass, int level)
    {
        BuildLookup();

        // lookupTable에 해당 charaterClass의 스텟 값이 설정되어있지 않으면 0반환
        if (!lookupTable[characterClass].ContainsKey(stats))
            return 0;

        float[] levels = lookupTable[characterClass][stats];
        //levels 배열이 0보다 작을 경우 0반환
        if(levels.Length <= 0) return 0;

        // level이 설정한 디자인의 최대보다 크면 최대값 반환
        if (levels.Length < level)
            return levels[levels.Length - 1];

        // 레벨에 맞는 스탯값 반환
        return levels[level - 1];
    }

    /// <summary>
    /// lookupTable을 빌드하고 계산된 정보를 저장
    /// </summary>
    private void BuildLookup()
    {
        // 이미 lookup이 있으면 종료
        if (lookupTable != null) return;

        // lookupTable 초기화
        lookupTable = new Dictionary<CharacterClass, Dictionary<Stats, float[]>>();

        // characterClasses 배열에 있는 직업클래스 및 스탯 정보를 루프를 통해 처리
        foreach(ProgressionCharacterClass progressionClass in characterClasses)
        {
            // statLookupTable 선언
            var statLookupTable = new Dictionary<Stats, float[]>();

            // 직업 클래스에 대한 스탯 정보를 루프를 통해 처리
            foreach (ProgressionStat progressionStat in progressionClass.stats)
            {

                statLookupTable[progressionStat.stats] = progressionStat.levels;
            }
            // 직업 클래스와 스탯 정보를 lookupTable에 저장합니다.
            lookupTable[progressionClass.characterClass] = statLookupTable;
        }
    }

    /// <summary>
    /// 직업클래스 정보를 저장하는 내부 클래스
    /// </summary>
    [Serializable] class ProgressionCharacterClass
    {
        public CharacterClass characterClass; // 직업 클래스
        public ProgressionStat[] stats; // 해당 직업 클래스의 스탯정보 배열
    }

    /// <summary>
    /// 스탯정보를 저장하는 내부 클래스
    /// </summary>
    [Serializable] class ProgressionStat
    {
        public Stats stats;     // 스탯
        public float[] levels;  // 레벨별 스탯 값 배열
    }
}
