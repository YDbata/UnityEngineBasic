using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 발사체
/// </summary>
public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] bool isHoming = true;
    [SerializeField] GameObject hitEffect = null;
    [SerializeField] float maxLifeTime = 10;
    [SerializeField] GameObject[] destroyOnHit = null;
    [SerializeField] float lifeAfterImpact = 2;
    [SerializeField] UnityEvent onHit;

    Health target = null;
    private Vector3 targetPoint;
    GameObject instigator = null;
    float damage = 0;

    private void Start()
    {
        transform.LookAt(GetAimLocation());
    }

    private void Update()
    {
        // 추격이 필요하다면 대상의 방향으로 회전
        if (target != null && isHoming && !target.IsDead()) 
        {
            transform.LookAt(GetAimLocation());
        }
        transform.Translate(Vector3.forward * speed*Time.deltaTime);
    }

    /// <summary>
    /// Health를 반환하는 경우
    /// </summary>
    /// <param name="target"></param>
    /// <param name="instigator"></param>
    /// <param name="damage"></param>
    public void SetTarget(Health target, GameObject instigator, float damage)
    {
        SetTarget(instigator, damage, target);
    }

    /// <summary>
    /// TargetPoint만 반환하는 경우
    /// </summary>
    /// <param name="targetPoint"></param>
    /// <param name="instigator"></param>
    /// <param name="damage"></param>
    public void SetTarget(Vector3 targetPoint, GameObject instigator, float damage)
    {
        SetTarget(instigator, damage, null, targetPoint);
    }

    /// <summary>
    /// 최종 대상 데이터 설정
    /// </summary>
    /// <param name="instigator"></param>
    /// <param name="damage"></param>
    /// <param name="target"></param>
    /// <param name="targetPoint"></param>
    public void SetTarget(GameObject instigator, float damage, Health target = null, Vector3 targetPoint = default)
    {
        this.target = target;
        this.targetPoint = targetPoint;
        this.damage = damage;
        this.instigator = instigator;

        Destroy(gameObject, maxLifeTime);
    }

    private Vector3 GetAimLocation()
    {
        // 대상이 없으면 기본방향
        if (target == null) return targetPoint;

        CharacterController targetCapsule = target.GetComponent<CharacterController>();
        // 대상의 Collider가 없으면 대상의 위치값 반환
        if (targetCapsule == null)  return target.transform.position;
        // Collider가 있으면 높이의 절반을 가져온다.
        return target.transform.position + Vector3.up*targetCapsule.height/2;
    }

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        // 타겟과 충돌 타겟과 다를 때
        if(target != null && health != target) return;
        // 죽었을 때 스킵
        if (health != null && health.IsDead()) return;
        // player에 겹쳤을 때 스킵
        if(other.gameObject == instigator) return;

        health.TakeDamage(instigator, damage);

        speed = 0;

        onHit.Invoke();

        if (hitEffect)
        {
            Instantiate(hitEffect, GetAimLocation(), transform.rotation);
        }

/*        foreach(GameObject toDestroy in destroyOnHit)
        {
            Destroy(toDestroy);
        }*/

        Destroy(gameObject);
    }
}
