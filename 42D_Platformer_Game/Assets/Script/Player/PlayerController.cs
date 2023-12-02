using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



// Rigidbody2D�� ������Ʈ�� ����ϹǷ� �Ʒ� �����߰�(���� �ʿ� ����)
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // �ȴ� �ӵ� ���� ����
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float runSpeed = 8.0f;
    [SerializeField] private float jumplmpulse = 10;
    [SerializeField] private float airWalkSpeed = 5;
    [SerializeField] private float airRunSpeed = 5;



    // ������ �۶� �ӵ��� �����ϱ� ���� ���� �ӵ� ���� ����
    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else
                    {
                        if (IsRunning)
                        {
                            return airRunSpeed;
                        }
                        else
                        {
                            return airWalkSpeed;
                        }
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
    }

    // �÷��̾��� �̵� �Է��� �����ϴ� ����
    Vector2 moveInput;

    // �����̴��� Ȯ���ϴ� ����
    private bool _isMoving = false;
    public bool IsMoving
    {
        get { return _isMoving; }
        set
        {
            _isMoving = value;
            animator.SetBool("IsMoving", _isMoving);
        }
    }
    // �÷��̾��� ������ Ȯ���ϴ� ����
    private bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        set
        {
            if(_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }
    // �޸��� �������� Ȯ���ϴ� ����
    private bool _isRunning = true;
    public bool IsRunning
    {
        get { return _isRunning; }
        set
        {
            _isRunning = value;
            animator.SetBool("IsRunning", _isRunning);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool("CanMove");
        }
    }

    // Rigidbody2D ������Ʈ�� ���� ����
    Rigidbody2D rb;
    // �� Script�� ����� Game Object�� animation�� ���� ������ ���� ����
    Animator animator;
    //
    TouchingDirections touchingDirections;
    Damageable damageable;

    // GameObject�� Ȱ��ȭ �ɶ� ����Ǵ� �Լ�
    private void Awake()
    {
        // Awake�Լ����� Rigidbody2D ������Ʈ�� �����´�
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        //
        damageable = GetComponent<Damageable>();
    }

    // ȭ��
    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger(AnimationStrings.RangedAttack);
        }
    }

    // ���� �����Ӹ��� �Ͼ�� update
    private void FixedUpdate()
    {
        // �÷��̾��� �̵��� ó��
        // �Է¿� ���� �÷��̾��� Rigidbody2D �ӵ��� ������Ʈ�Ѵ�.
        if (!damageable.LockVelocity)
        {
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        }
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        } else if(moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }


    // Input System���� ���� �Է´�� �̵��� ó���ϴ� �Լ�
    // param�� InputAction�� ���� ���ؼ��� ������ using UnityEngine.InputSystem�� �߰����ش�.
    public void OnMove(InputAction.CallbackContext context)
    {
        // �Է� ���ؽ�Ʈ�κ��� �̵� �Է°��� �о�´�.
        moveInput = context.ReadValue<Vector2>();
        // �̵��� 0�� �ȴ� ��� �÷��̾ �����δٰ� ǥ���Ѵ�.
        IsMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
    }

    // �޸��� ���º������ִ� �Լ�
    public void OnRun(InputAction.CallbackContext context)
    {
        // context. started�� shift�� ������ ������ ������ �ǹ��Ѵ�.
        // �Ʒ� else if���� ���� ������ shift�� �ѹ� ������ ������ ������ �ٸ� ���°� �Ǳ� ������
        // ������ ���� ������ caneled�� ���¿����� false�� �ٲ�� �ۼ��Ͽ���.
        if (context.started)
        {
            IsRunning = true;
        }else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumplmpulse);
        }
    }


    // �����Է� ó��
    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void OnHit(Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }


    
}
