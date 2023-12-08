using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WalkableDirection
{
    Right,
    Left,
}


[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Knight : MonoBehaviour
{
    //public float walkAcceleration = 3.0f;
    //public float maxSpeed = 3.0f;
    //public float walkStopRate = 0.6f;
    //public DetectionZone attackZone;
    //public DetectionZone cliffDetectionZone;

    //public float walkSpeed = 3.0f;
    public float maxSpeed = 3.0f;
    public float walkStopRate = 0.6f;
    public float walkAcceleration = 30f;
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
            animator.SetBool(AnimationStrings.HasTarget, _hasTarget);
        }
    }

    public bool CanMove
    {
        get { return animator.GetBool(AnimationStrings.CanMove); }
    }

    public WalkableDirection WalkDirection
    {
        get { return walkDirection; }
        set
        {
            if (walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
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

    // Update is called once per frame
    void Update()
    {
        HasTarget = attackZone.detectedColiders.Count > 0;
        if (AttackCoolTime > 0)
        {
            AttackCoolTime -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }
        if (!damageable.LockVelocity)
        {
            // 가감속 효과는 보통 땅에 있을때 넣기 때문에
            if (CanMove && touchingDirections.IsGrounded)
            {
                rb.velocity =
                    new Vector2
                    {
                        x = Mathf.Clamp(rb.velocity.x + (walkAcceleration * walkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed),
                        y = rb.velocity.y
                    };
                //Debug.Log($"DeltaTime{(Time.fixedDeltaTime)}, {walkAcceleration*walkDirectionVector.x*Time.fixedDeltaTime}, {walkAcceleration}");
                //(walkSpeed * walkDirectionVector.x, rb.velocity.y);
            }
            else
                rb.velocity = new Vector2
                {
                    x = Mathf.Clamp(rb.velocity.x, 0, walkStopRate),
                    y = rb.velocity.y
                };//(0*walkDirectionVector.x, rb.velocity.y);
        }
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("FlipDirection : ");
        }
    }

    public void OnHit(Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnCliffDetected()
    {
        if (touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }
}