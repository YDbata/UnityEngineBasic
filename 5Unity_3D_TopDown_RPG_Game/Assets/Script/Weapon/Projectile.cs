using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �߻�ü
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
        // �߰��� �ʿ��ϴٸ� ����� �������� ȸ��
        if (target != null && isHoming && !target.IsDead()) 
        {
            transform.LookAt(GetAimLocation());
        }
        transform.Translate(Vector3.forward * speed*Time.deltaTime);
    }

    /// <summary>
    /// Health�� ��ȯ�ϴ� ���
    /// </summary>
    /// <param name="target"></param>
    /// <param name="instigator"></param>
    /// <param name="damage"></param>
    public void SetTarget(Health target, GameObject instigator, float damage)
    {
        SetTarget(instigator, damage, target);
    }

    /// <summary>
    /// TargetPoint�� ��ȯ�ϴ� ���
    /// </summary>
    /// <param name="targetPoint"></param>
    /// <param name="instigator"></param>
    /// <param name="damage"></param>
    public void SetTarget(Vector3 targetPoint, GameObject instigator, float damage)
    {
        SetTarget(instigator, damage, null, targetPoint);
    }

    /// <summary>
    /// ���� ��� ������ ����
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
        // ����� ������ �⺻����
        if (target == null) return targetPoint;

        CharacterController targetCapsule = target.GetComponent<CharacterController>();
        // ����� Collider�� ������ ����� ��ġ�� ��ȯ
        if (targetCapsule == null)  return target.transform.position;
        // Collider�� ������ ������ ������ �����´�.
        return target.transform.position + Vector3.up*targetCapsule.height/2;
    }

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        // Ÿ�ٰ� �浹 Ÿ�ٰ� �ٸ� ��
        if(target != null && health != target) return;
        // �׾��� �� ��ŵ
        if (health != null && health.IsDead()) return;
        // player�� ������ �� ��ŵ
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
