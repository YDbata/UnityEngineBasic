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
        // 다음 레벨까지 필요한 경험치양
        // 현재 레벨값
        // int penultiamtelevel = progression.GetLevels(Stats.ExperienceToLevelUp, CharacterClass.Player);
        // 현재 레벨값을 토대로 필요한 경험치 계산
        float XPToLevelUp = progression.GetStat(Stats.ExperienceToLevelUp, CharacterClass.Player, baseStats.GetLevel());

        experienceBar.fillAmount = experience.GetPoints() / XPToLevelUp;
        experienceText.text = $"{experience.GetPoints()} / {XPToLevelUp}"; // S"" --> String.Format()
    }
}
