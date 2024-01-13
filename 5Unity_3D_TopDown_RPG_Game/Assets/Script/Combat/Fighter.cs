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
    /// 기본 무기를 설정하고 반환
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

    #region 무기 관련 코드
    /// <summary>
    /// 캐릭터의 손에 무기 들려주는 함수
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
    /// 에니메이션 공격!
    /// </summary>
    private void TriggerAttack()
    {
        _animator.ResetTrigger("StopAttack");
        _animator.SetTrigger("Attack");
    }

    /// <summary>
    ///  에니메이션 공격중지!
    /// </summary>
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
        if (!mover.CanMoveTo(combatTarget.transform.position) && !GetIsInRange(combatTarget.transform)) return false;

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

    public void Shoot()
    {
        Hit();
    }

    public void Hit()
    {
        if (target == null) return;
        float damage = GetComponent<BaseStats>().GetStat(Stats.Damage);
        
        // 무기를 들고 있다면 OnHit이벤트 호출
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
    /// 공격취소 함수
    /// </summary>
    public void Cancle()
    {
        StopAttack();
        target = null;
        mover.Cancle();
        timeSinceLastAttack = 0;
    }
}
