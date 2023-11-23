using Lesson_10;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lesson_11
{
    public class Attack : MonoBehaviour
    {
        public int attackDamage = 10;
        public Vector2 knockback = Vector2.zero;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Lesson_12.Damageable damageable = collision.GetComponent<Lesson_12.Damageable>();
            if (damageable != null)
            {
                Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
                //TODO : ���� �����ϴ� ĳ���Ͱ� �ٶ󺸴� �������� vector2 ����
                damageable.GetHit(attackDamage, deliveredKnockback);
            }
        }
    }

}
