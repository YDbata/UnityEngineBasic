using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] TakeDamageEvent takeDamage;
    [SerializeField] UnityEvent onDie;


    [Serializable] public class TakeDamageEvent : UnityEvent<float>
    {

    }

    ActionScheduler actionScheduler;

    LazyValue<float> healthPoints;
    bool isDead = false;

    private void Awake()
    {
        healthPoints = new LazyValue<float>(GetInitialHealth);
        actionScheduler = GetComponent<ActionScheduler>();
    }

    private float GetInitialHealth()
    {
        return GetComponent<BaseStats>().GetStat(Stats.Health);
    }

    private void Start()
    {
        healthPoints.ForceInit();
    }

    public void TakeDamage(GameObject instigator, float damage)
    {
        healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);
        Debug.Log("Health : " + healthPoints.value);
        if(healthPoints.value == 0) {
            Debug.Log("Die");
        }
        else
        {
            takeDamage.Invoke(damage);
        }
    }
}
