using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image healthBar;

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (health.GetHealthPoints() / health.GetMaxHealthPoints());
    }
}
