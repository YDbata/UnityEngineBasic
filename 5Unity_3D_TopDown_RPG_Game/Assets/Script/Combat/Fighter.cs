using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour, IAction
{
    [SerializeField] private float timeBetweenAttacks = 1;

    private Mover mover;
    private ActionScheduler _actionScheduler;
    private Animator _animator;
    private Health target = null;

    private float timeSinceLastAttack = 0;

    private void Awake()
    {
        mover = GetComponent<Mover>();
        _animator = GetComponentInChildren<Animator>();
        _actionScheduler = GetComponent<ActionScheduler>();

    }

    void Update()
    {
        
        if (!target) return;

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
        timeSinceLastAttack += Time.deltaTime;
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
        _animator.ResetTrigger("Attack");
        _animator.SetTrigger("StopAttack");
    }

    public bool CanAttack(GameObject combatTarget)
    {
        if(combatTarget == null) return false;
        if (!mover.CanMoveTo(combatTarget.transform.position)) return false;
        return true;
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
