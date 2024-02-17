using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour, IAction
{
    [SerializeField] private float timeBetweenAttacks = 1;
    [SerializeField] private Transform rightHandTransform = null;
    [SerializeField] private Transform leftHandTransform = null;
    [SerializeField] private WeaponConfig defaultWeapon = null;

    private Mover mover;
    private ActionScheduler _actionScheduler;
    private Animator _animator;

    private Health target = null;

    private float timeSinceLastAttack = 0;

    WeaponConfig currentWeaponConfig;
    LazyValue<Weapon> currentWeapon;

    private void Awake()
    {
        mover = GetComponent<Mover>();
        _animator = GetComponentInChildren<Animator>();
        _actionScheduler = GetComponent<ActionScheduler>();

    }

    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (!target) return;
        if (target.IsDead()) return;

        if (!GetIsInRange(target.transform))
        {
            mover.MoveTo(target.transform.position, 1);
        }
        else
        {
            mover.Cancle();
            AttackBehaviour();
        }
    }

    private void AttackBehaviour()
    {
        // timeSinceLastAttack += Time.deltaTime;
        transform.LookAt(target.transform);
        if(timeSinceLastAttack > timeBetweenAttacks)
        {
            TriggerAttack();
            timeSinceLastAttack = 0;
        }

    }

    private void TriggerAttack()
    {
        _animator.ResetTrigger("StopAttack");
        _animator.SetTrigger("Attack");
    }

    private void StopAttack()
    {
        _animator.SetTrigger("StopAttack");
        _animator.ResetTrigger("Attack");
    }

    /// <summary>
    /// 공격할 수 있는 대상의 경우 True반환
    /// </summary>
    /// <param name="combatTarget"></param>
    /// <returns></returns>
    public bool CanAttack(GameObject combatTarget)
    {
        if(combatTarget == null) return false;
        if (!mover.CanMoveTo(combatTarget.transform.position)) return false;

        // 죽었을때 적이 아닌것을 제어
        Health targetHealth = combatTarget.GetComponent<Health>();
        return targetHealth != null && !targetHealth.IsDead();
    }

    internal void Attack(GameObject gameObject)
    {
        //mover.StartMoveAction(gameObject.transform.position, 1);
        _actionScheduler.StartAction(this);
        target = gameObject.GetComponent<Health>();

    }

    public void Hit()
    {
        if (target == null) return;
        float damage = GetComponent<BaseStats>().GetStat(Stats.Damage);
        target.TakeDamage(this.gameObject, damage);
    }

    private bool GetIsInRange(Transform tarGetTransform)
    {
        return Vector3.Distance(transform.position, tarGetTransform.position) < 2;
    }

    public void Cancle()
    {
        StopAttack();
        target = null;
        mover.Cancle();
        timeSinceLastAttack = 0;
    }
}
