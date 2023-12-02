using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<Vector2> damageableHit;
    public UnityEvent<int, int> healthChanged;

    public bool LockVelocity
    {
        get { return animator.GetBool(AnimationStrings.LockVelocity); }
        set { animator.SetBool(AnimationStrings.LockVelocity, value); }
    }

    Animator animator;
    //최대 체력
    [SerializeField]
    private int _maxHealth = 100;

    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }
    //현재 체력
    [SerializeField]
    private int _health = 100;

    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            healthChanged?.Invoke(_health, MaxHealth);
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    //생존 여부
    [SerializeField]
    private bool _isAlive = true;

    public bool IsAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.IsAlive, _isAlive);
            Debug.Log(_isAlive);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public bool GetHit(int attackDamage)
    {
        if (IsAlive)
        {
            Health -= attackDamage;

            animator.SetTrigger("Hit");
            Debug.Log("Health" + Health);
            return true;
        }
        else
        {
            Debug.Log("Is Death");
            return false;
        }
    }

    public bool GetHit(int attackDamage, Vector2 knockback)
    {
        if (IsAlive)
        {
            Health -= attackDamage;

            animator.SetTrigger("Hit");
            Debug.Log("Health" + Health);

            LockVelocity = true;
            CharectorEvents.characterDamaged?.Invoke(this.gameObject, attackDamage);
            damageableHit?.Invoke(knockback);
            return true;
        }
        return false;
    }

    public bool Heal(int healthRestore)
    {
        if (IsAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);

            Health += actualHeal;
            CharectorEvents.characterHealed?.Invoke(this.gameObject, healthRestore);
            Debug.Log("Player Heal " + Health);
            return true;
        }
        return false;
    }
}
