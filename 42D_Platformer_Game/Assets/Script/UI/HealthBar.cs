using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text healthBarText;


    Damageable playerDamageAble;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            playerDamageAble = player.GetComponent<Damageable>();
        }
    }

    private void OnEnable()
    {
        if (playerDamageAble)
        {
            playerDamageAble.healthChanged.AddListener(OnPlayerHealthChanged);
        }
    }
    private void OnDisable()
    {
        if (playerDamageAble)
        {
            playerDamageAble.healthChanged.RemoveListener(OnPlayerHealthChanged);
        }
    }


    private void Start()
    {
        healthSlider.value = CalculateSliderPercentage(playerDamageAble.Health, playerDamageAble.MaxHealth);
        healthBarText.text = $"HP {playerDamageAble.Health}/{playerDamageAble.MaxHealth}";
    }

    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
        healthBarText.text = $"HP {newHealth}/{maxHealth}";
    }
    
}
