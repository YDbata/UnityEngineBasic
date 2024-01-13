using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Weapon", menuName ="Weapons/Make New Weapon", order = 1)]
public class WeaponConfig : ScriptableObject
{

    [SerializeField] AnimatorOverrideController animatorOverride = null;
    [SerializeField] Weapon equippedPrefab = null;
    [SerializeField] float weaponDamage = 5f;
    [SerializeField] float percentageBonus = 0;
    [SerializeField] float weaponRange = 2f;
    [SerializeField] bool isRightHanded = true;
    [SerializeField] Projectile projectile = null;

    const string weaponName = "Weapon";

    /// <summary>
    /// ���� ������ �����ϴ� �ڵ�
    /// </summary>
    /// <param name="rightHandTransform"></param>
    /// <param name="leftHandTransform"></param>
    /// <param name="animator"></param>
    /// <returns></returns>
    public Weapon Spawn(Transform rightHand, Transform leftHand, Animator animator)
    {
        // �������� �μ���
        DestroyOldWeapon(rightHand, leftHand);

        // ���ο� ���� ����(������ �̸� = weaponName)
        Weapon weapon = null;


        // ������ ���� ���� �����Ѵٸ�
        if(equippedPrefab != null)
        {
            Transform handTransform = GetTransform(rightHand, leftHand);
            weapon = Instantiate(equippedPrefab, handTransform);
            weapon.gameObject.name = weaponName;
        }

        // ���ǰ� �ִ� AnimatorController��ȯ
        // var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
        
        // �����س��� Override animator Controller�� �ִ��� Ȯ��
        if(animatorOverride != null)
        {
            animator.runtimeAnimatorController = animatorOverride;
        }
        // ������ �����ϰ� �ִ� override�� ��Ʈ�ѷ� �ٽ� �����ϴ� �ڵ�
        /*else if(overrideController != null)
        {
            animator.runtimeAnimatorController = overrideController;
        }*/

        return weapon;
    }

    /// <summary>
    /// ������, �޼��߾�� ���⸦ ������ ����
    /// </summary>
    /// <param name="rightHand"></param>
    /// <param name="leftHand"></param>
    /// <returns></returns>
    private Transform GetTransform(Transform rightHand, Transform leftHand)
    {
        Transform handTransform;
        if (isRightHanded) handTransform = rightHand;
        else handTransform = leftHand;
        return handTransform;
    }

    /// <summary>
    /// ������ ������ �ִ� ���� �� �ı�
    /// </summary>
    /// <param name="rightHand">������ ����</param>
    /// <param name="leftHand">�޼� ����</param>
    private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
    {
        // ����� �Ѽտ��� ���� ���� ����(�޼հ� ������ ���ε��ΰ� �Ұ���)
        Transform oldWeapon = rightHand.Find(weaponName);
        if (oldWeapon == null)
        {
            oldWeapon = leftHand.Find(weaponName);
        }

        if (oldWeapon == null) return;

        // �̸��� �ٲ㼭 �ߺ�ȣ���� ���´�
        oldWeapon.name = "DESTROYING";
        Destroy(oldWeapon.gameObject);
    }

    public bool HasProjectile() 
    {
        return projectile != null;
    }


    public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target,
        GameObject instigator, float calculatedDamage)
    {
        // identity�� 0,0,0 ��ȯ
        Projectile projectileInstance = Instantiate(projectile, 
            GetTransform(rightHand, leftHand).position, Quaternion.identity);

        projectileInstance.SetTarget(target, instigator, calculatedDamage);
    }

    public float GetRange()
    {
        return weaponRange;
    }
}
