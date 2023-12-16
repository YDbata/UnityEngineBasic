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
    // characterClasses ����Ŭ������ �׿����� ���������� ����
    [SerializeField] ProgressionCharacterClass[] characterClasses = null;

    // lookupTable ���� �� ���� Ŭ������ ���� ������ �̸� ����Ͽ� �����ϴ� Dictionary�Դϴ�.
    Dictionary<CharacterClass, Dictionary<Stats, float[]>> lookupTable = null;

    // Ư�������� Ư�� ���� Ŭ���� �� ������ �ش��ϴ� ���Ȱ��� ��ȯ
    public float GetStat(Stats stats, CharacterClass characterClass, int level)
    {
        BuildLookup();

        // lookupTable�� �ش� charaterClass�� ���� ���� �����Ǿ����� ������ 0��ȯ
        if (!lookupTable[characterClass].ContainsKey(stats))
            return 0;

        float[] levels = lookupTable[characterClass][stats];
        //levels �迭�� 0���� ���� ��� 0��ȯ
        if(levels.Length <= 0) return 0;

        // level�� ������ �������� �ִ뺸�� ũ�� �ִ밪 ��ȯ
        if (levels.Length < level)
            return levels[levels.Length - 1];

        // ������ �´� ���Ȱ� ��ȯ
        return levels[level - 1];
    }

    /// <summary>
    /// lookupTable�� �����ϰ� ���� ������ ����
    /// </summary>
    private void BuildLookup()
    {
        // �̹� lookup�� ������ ����
        if (lookupTable != null) return;

        // lookupTable �ʱ�ȭ
        lookupTable = new Dictionary<CharacterClass, Dictionary<Stats, float[]>>();

        // characterClasses �迭�� �ִ� ����Ŭ���� �� ���� ������ ������ ���� ó��
        foreach(ProgressionCharacterClass progressionClass in characterClasses)
        {
            // statLookupTable ����
            var statLookupTable = new Dictionary<Stats, float[]>();

            // ���� Ŭ������ ���� ���� ������ ������ ���� ó��
            foreach (ProgressionStat progressionStat in progressionClass.stats)
            {

                statLookupTable[progressionStat.stats] = progressionStat.levels;
            }
            // ���� Ŭ������ ���� ������ lookupTable�� �����մϴ�.
            lookupTable[progressionClass.characterClass] = statLookupTable;
        }
    }

    /// <summary>
    /// ����Ŭ���� ������ �����ϴ� ���� Ŭ����
    /// </summary>
    [Serializable] class ProgressionCharacterClass
    {
        public CharacterClass characterClass; // ���� Ŭ����
        public ProgressionStat[] stats; // �ش� ���� Ŭ������ �������� �迭
    }

    /// <summary>
    /// ���������� �����ϴ� ���� Ŭ����
    /// </summary>
    [Serializable] class ProgressionStat
    {
        public Stats stats;     // ����
        public float[] levels;  // ������ ���� �� �迭
    }
}
