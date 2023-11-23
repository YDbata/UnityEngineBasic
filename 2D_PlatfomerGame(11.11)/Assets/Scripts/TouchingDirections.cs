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
            animator.SetBool("IsGrounded", _isGrounded);
        }
    }

    CapsuleCollider2D touchingCol;
    Animator animator;

    public ContactFilter2D castFilter;
    [SerializeField] private float groundDistance = 0.05f;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
    }
}
