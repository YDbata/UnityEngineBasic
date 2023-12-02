using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Projectile : MonoBehaviour
{
    public int damage = 7;
    public Vector2 moveSpeed = new Vector2(3.0f, 0);
    public Vector2 knockback = Vector2.zero;
    public float timeover = 7.0f;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // localScale은 방향전환을 위한 값이다.
        rb.velocity = new Vector2(moveSpeed.x*transform.localScale.x, moveSpeed.y);
        timeover -= Time.fixedDeltaTime;
        if(timeover < 0)
        {
            Destroy(this.gameObject);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback
                : new Vector2(-knockback.x, knockback.y);
            bool gotHit = damageable.GetHit(damage, deliveredKnockback);
            if(gotHit)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
