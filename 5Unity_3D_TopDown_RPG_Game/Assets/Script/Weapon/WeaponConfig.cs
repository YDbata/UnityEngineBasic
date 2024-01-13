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
    /// 무기 프리팹 생성하는 코드
    /// </summary>
    /// <param name="rightHandTransform"></param>
    /// <param name="leftHandTransform"></param>
    /// <param name="animator"></param>
    /// <returns></returns>
    public Weapon Spawn(Transform rightHand, Transform leftHand, Animator animator)
    {
        // 이전무기 부수기
        DestroyOldWeapon(rightHand, leftHand);

        // 새로운 무기 생성(무기의 이름 = weaponName)
        Weapon weapon = null;


        // 생성할 무기 모델이 존재한다면
        if(equippedPrefab != null)
        {
            Transform handTransform = GetTransform(rightHand, leftHand);
            weapon = Instantiate(equippedPrefab, handTransform);
            weapon.gameObject.name = weaponName;
        }

        // 사용되고 있는 AnimatorController반환
        // var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
        
        // 설정해놓은 Override animator Controller가 있는지 확인
        if(animatorOverride != null)
        {
            animator.runtimeAnimatorController = animatorOverride;
        }
        // 기존에 동작하고 있던 override를 컨트롤러 다시 설정하는 코드
        /*else if(overrideController != null)
        {
            animator.runtimeAnimatorController = overrideController;
        }*/

        return weapon;
    }

    /// <summary>
    /// 오른손, 왼손중어디에 무기를 끼울지 정함
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
    /// 이전에 가지고 있던 무기 모델 파괴
    /// </summary>
    /// <param name="rightHand">오른손 무기</param>
    /// <param name="leftHand">왼손 무기</param>
    private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
    {
        // 현재는 한손에만 무기 착용 가능(왼손과 오른손 따로따로가 불가능)
        Transform oldWeapon = rightHand.Find(weaponName);
        if (oldWeapon == null)
        {
            oldWeapon = leftHand.Find(weaponName);
        }

        if (oldWeapon == null) return;

        // 이름을 바꿔서 중복호출을 막는다
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
        // identity는 0,0,0 반환
        Projectile projectileInstance = Instantiate(projectile, 
            GetTransform(rightHand, leftHand).position, Quaternion.identity);

        projectileInstance.SetTarget(target, instigator, calculatedDamage);
    }

    public float GetRange()
    {
        return weaponRange;
    }
}
