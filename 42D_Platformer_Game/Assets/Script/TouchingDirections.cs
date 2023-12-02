using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{



    // ���鿡 ��Ҵ��� ����
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
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    private bool _isOnWall = false;
    public bool IsOnWall
    {
        get { return _isOnWall; }
        private set
        {
            _isOnWall = value;
            animator.SetBool("IsOnWall", _isOnWall);
        }
    }

    //
    private bool _isOnCeiling = true;
    public bool IsOnCeiling
    {
        get { return _isOnCeiling; }
        set
        {
            _isOnCeiling = value;
            animator.SetBool(AnimationStrings.IsOnCeiling, _isOnCeiling);
        }
    }


    // ĳ���Ϳ� ��ü�� ��� Collider
    CapsuleCollider2D touchingCol;
    Animator animator;

    // �浹 �˻�� Filter
    public ContactFilter2D castFilter;

    // ������� �Ÿ� �Ӱ谪
    [SerializeField] private float groundDistance = 0.05f;
    [SerializeField] public float wallDistance = 0.2f;
    //
    [SerializeField] private float ceilingDistance = 0.05f;

    // ���� �浹 ������ ������ �迭
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
        // ���鿡 ��Ҵ��� ���� �����ϰ� ���¸� ������Ʈ
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
    }
}
