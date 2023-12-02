using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WalkableDirection
{
    Right,
    Left,
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(TouchingDirections))]
public class Knight : MonoBehaviour
{
    public float walkSpeed = 3.0f;
    [SerializeField] private bool _hasTarget = false;
    [SerializeField] private DetectionZone attackZone;

    Rigidbody2D rb;
    CapsuleCollider2D capsuleCollider;
    TouchingDirections touchingDirections;
    Animator animator;
    Damageable damageable;

    private WalkableDirection walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public float AttackCoolTime
    {
        get { return animator.GetFloat(AnimationStrings.AttackCoolDown); }
        set { animator.SetFloat(AnimationStrings.AttackCoolDown, Mathf.Max(value, 0)); }
    }

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.HasTarget, value);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.CanMove);
        }
    }

    public WalkableDirection WalkDirection
    {
        get { return walkDirection; }
        set
        {
            if(walkDirection != value)
            {
                gameObject.transform.localScale
                    = new Vector2(gameObject.transform.localScale.x * -1,
                    gameObject.transform.localScale.y);

                if(value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if(value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            walkDirection = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    private void Update()
    {
        HasTarget = attackZone.detectedColiders.Count > 0;

        if (AttackCoolTime > 0) 
        {
            AttackCoolTime -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }

        if (!damageable.LockVelocity)
        {
            if (CanMove)
            {
                rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0 * walkDirectionVector.x, rb.velocity.y);
            }
        }
    }

    private void FlipDirection()
    {
        if(WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if(WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
    }

    public void OnCliffDetected()
    {
        if (touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }


    public void OnHit(Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
        Debug.Log(knockback);
    }

}
