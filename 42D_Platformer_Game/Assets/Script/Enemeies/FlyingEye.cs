using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class FlyingEye : MonoBehaviour
{
    public float flightSpeed = 2.0f;
    public List<Transform> waypoints;
    public float waypointReachedDistance = 0.3f;
    //public Vector2 flyingDirectionVector = Vector2.right;
    public Collider2D deathCollider;
    

    [SerializeField] private bool _hasTarget = false;
    [SerializeField] private DetectionZone hitBoxZone;

    Animator animator;
    Rigidbody2D rb;
    Damageable damageable;
    Transform nextWayPoint;
    private int waypointIndex = 0;

    public float AttackCoolDown
    {
        get { return animator.GetFloat(AnimationStrings.AttackCoolDown); }
        set { animator.SetFloat(AnimationStrings.AttackCoolDown, Math.Max(value,0)); }
    }

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set { 
            _hasTarget = value; 
            animator.SetBool(AnimationStrings.HasTarget, _hasTarget);
        }
    }
    public bool CanMove
    {
        get { return animator.GetBool(AnimationStrings.CanMove); }
    }

    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }

    private void Update()
    {
        HasTarget = hitBoxZone.detectedColiders.Count > 0;
        if(AttackCoolDown > 0)
        {
            AttackCoolDown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {
            if(CanMove)
            {
                Flight();

            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void Start()
    {
        nextWayPoint = waypoints[waypointIndex];
        this.transform.position = nextWayPoint.position;
    }

    private void Flight()
    {
        Vector2 directionToWaypoint = (nextWayPoint.position - this.transform.position).normalized;
        //FlipDirection(directionToWaypoint);

        float distance = Vector2.Distance(nextWayPoint.position, this.transform.position);

        rb.velocity = directionToWaypoint * flightSpeed;
        //this.transform.localScale = new Vector2(gameObject.transform.localScale.x*flyingDirectionVector.x
        //    ,gameObject.transform.localScale.y);
        if(distance <= waypointReachedDistance)
        {
            
            waypointIndex++;
            if(waypointIndex >= waypoints.Count)
            {
                waypointIndex = 0;
            }

            nextWayPoint = waypoints[waypointIndex];
        }

    }

    public void OnDeath()
    {
        rb.gravityScale = 2.0f;
        deathCollider.enabled = true;
    }

    /*private void FlipDirection(Vector2 directionwaypoint)
    {
        if (directionwaypoint.x < 0)
            flyingDirectionVector = Vector2.left;
        else flyingDirectionVector = Vector2.right;
    }*/
}
