using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    private bool _isGrounded = true;
    public bool IsGrounded
    {
        get { return _isGrounded; }
        set 
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.IsGrounded, _isGrounded);
        }
    }

    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0? Vector2.right : Vector2.left;

    private bool _isOnWall = true;
    public bool IsOnWall
    {
        get { return _isOnWall; }
        set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.IsOnWall, _isOnWall);
        }
    }

    private bool _isOnCeiling= true;
    public bool IsOnCeiling
    {
        get { return _isOnCeiling; }
        set
        {
            _isOnCeiling = value;
            animator.SetBool("IsOnCeiling", _isOnCeiling);
        }
    }

    CapsuleCollider2D touchingCol;
    Animator animator;

    public ContactFilter2D castFilter;
    [SerializeField] private float groundDistance = 0.05f;
    [SerializeField] private float wallDistance = 0.2f;
    [SerializeField] private float ceilingDistance = 0.05f;
    

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
    }
}
