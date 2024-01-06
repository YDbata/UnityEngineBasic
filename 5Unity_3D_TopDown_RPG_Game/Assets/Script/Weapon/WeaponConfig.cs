using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Weapon", menuName ="Weapons/Make New Weapon", order = 1)]
public class WeaponConfig : ScriptableObject
{
    [SerializeField] Weapon equippedPrefab = null;
    [SerializeField] float weaponDamage = 5f;
    [SerializeField] float percentageBonus = 0;
    [SerializeField] float weaponRange = 2f;
    [SerializeField] bool isRightHanded = true;
    [SerializeField] Projectile projectile = null;



}
