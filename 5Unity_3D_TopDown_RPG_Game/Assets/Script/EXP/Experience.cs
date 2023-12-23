using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Experience : MonoBehaviour
{
    [SerializeField] private float experiencePoints = 0;

    // 경험치를 얻었을때 호출할 Action
    public event Action onExperienceGained;

    //경험치를 얻는 함수
    public void GainExperience(float experience)
    {
        experiencePoints += experience;
        Debug.Log("EXP : " + experiencePoints);
        onExperienceGained?.Invoke();
    }

    //현재 경험치를 반환하는 함수
    public float GetPoints() { return experiencePoints; }
}



