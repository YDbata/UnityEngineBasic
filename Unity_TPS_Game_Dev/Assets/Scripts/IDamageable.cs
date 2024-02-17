using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void HitDamage(float amount);
    public void Die();
}
