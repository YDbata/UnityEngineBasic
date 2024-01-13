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
        currentWeaponConfig = defaultWeapon;
        currentWeapon = new LazyValue<Weapon>(SetupDefaultWeapon);
    }

    /// <summary>
    /// �⺻ ���⸦ �����ϰ� ��ȯ
    /// </summary>
    /// <returns></returns>
    private Weapon SetupDefaultWeapon()
    {
        return AttachWeapon(defaultWeapon);
    }

    private void Start()
    {
        currentWeapon.ForceInit();
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

    #region ���� ���� �ڵ�
    /// <summary>
    /// ĳ������ �տ� ���� ����ִ� �Լ�
    /// </summary>
    /// <param name="defaultWeapon"></param>
    /// <returns></returns>
    private Weapon AttachWeapon(WeaponConfig weapon)
    {
        if(weapon == null)
        {
            return null;
        }
        return weapon.Spawn(rightHandTransform, leftHandTransform, _animator);
    }
    #endregion
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

    /// <summary>
    /// ���ϸ��̼� ����!
    /// </summary>
    private void TriggerAttack()
    {
        _animator.ResetTrigger("StopAttack");
        _animator.SetTrigger("Attack");
    }

    /// <summary>
    ///  ���ϸ��̼� ��������!
    /// </summary>
    private void StopAttack()
    {
        _animator.SetTrigger("StopAttack");
        _animator.ResetTrigger("Attack");
    }

    /// <summary>
    /// ������ �� �ִ� ����� ��� True��ȯ
    /// </summary>
    /// <param name="combatTarget"></param>
    /// <returns></returns>
    public bool CanAttack(GameObject combatTarget)
    {
        if(combatTarget == null) return false;
        if (!mover.CanMoveTo(combatTarget.transform.position) && !GetIsInRange(combatTarget.transform)) return false;

        // �׾����� ���� �ƴѰ��� ����
        Health targetHealth = combatTarget.GetComponent<Health>();
        return targetHealth != null && !targetHealth.IsDead();
    }

    internal void Attack(GameObject gameObject)
    {
        //mover.StartMoveAction(gameObject.transform.position, 1);
        _actionScheduler.StartAction(this);
        target = gameObject.GetComponent<Health>();

    }

    public void Shoot()
    {
        Hit();
    }

    public void Hit()
    {
        if (target == null) return;
        float damage = GetComponent<BaseStats>().GetStat(Stats.Damage);
        
        // ���⸦ ��� �ִٸ� OnHit�̺�Ʈ ȣ��
        if(currentWeapon.value != null)
        {
            currentWeapon.value.OnHit();
        }

        if (currentWeaponConfig.HasProjectile())
        {
            currentWeaponConfig.LaunchProjectile(rightHandTransform, leftHandTransform,
                target, gameObject, damage);
        }
        else
        {
            target.TakeDamage(this.gameObject, damage);
        }
    }

    private bool GetIsInRange(Transform tarGetTransform)
    {
        return Vector3.Distance(transform.position, tarGetTransform.position) < currentWeaponConfig.GetRange();
    }

    /// <summary>
    /// ������� �Լ�
    /// </summary>
    public void Cancle()
    {
        StopAttack();
        target = null;
        mover.Cancle();
        timeSinceLastAttack = 0;
    }
}
