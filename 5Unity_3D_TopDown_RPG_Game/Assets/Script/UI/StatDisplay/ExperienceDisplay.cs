using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceDisplay : MonoBehaviour
{
    [SerializeField] private Experience experience;
    [SerializeField] private Progression progression;
    [SerializeField] private BaseStats baseStats;
    [SerializeField] private Image experienceBar;
    [SerializeField] private Text experienceText;

    // Update is called once per frame
    void Update()
    {
        // ���� �������� �ʿ��� ����ġ��
        // ���� ������
        // int penultiamtelevel = progression.GetLevels(Stats.ExperienceToLevelUp, CharacterClass.Player);
        // ���� �������� ���� �ʿ��� ����ġ ���
        float XPToLevelUp = progression.GetStat(Stats.ExperienceToLevelUp, CharacterClass.Player, baseStats.GetLevel());

        experienceBar.fillAmount = experience.GetPoints() / XPToLevelUp;
        experienceText.text = $"{experience.GetPoints()} / {XPToLevelUp}"; // S"" --> String.Format()
    }
}
