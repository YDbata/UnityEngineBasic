using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	public Image FillAmount;
    public void GiveFullHealth()
	{
		//체력바가 다 꽉 찬 상태로 초기화
		FillAmount.fillAmount = 1;
	}

	public void SetHealth(float healthPer)
	{
		FillAmount.fillAmount = healthPer;
	}
}
