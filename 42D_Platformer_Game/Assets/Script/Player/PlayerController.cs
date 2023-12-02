using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



// Rigidbody2D의 컴포넌트를 사용하므로 아래 구문추가(굳이 필요 없음)
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // 걷는 속도 설정 변수
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float runSpeed = 8.0f;
    [SerializeField] private float jumplmpulse = 10;
    [SerializeField] private float airWalkSpeed = 5;
    [SerializeField] private float airRunSpeed = 5;



    // 걸을때 뛸때 속도를 조절하기 위한 현재 속도 저장 변수
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

    // 플레이어의 이동 입력을 저장하는 벡터
    Vector2 moveInput;

    // 움직이는지 확인하는 변수
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
    // 플레이어의 방향을 확인하는 변수
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
    // 달리는 상태인지 확인하는 변수
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

    // Rigidbody2D 컴포넌트에 대한 참조
    Rigidbody2D rb;
    // 이 Script가 적용된 Game Object의 animation에 대한 조작을 위한 변수
    Animator animator;
    //
    TouchingDirections touchingDirections;
    Damageable damageable;

    // GameObject가 활성화 될때 실행되는 함수
    private void Awake()
    {
        // Awake함수에서 Rigidbody2D 컴포넌트를 가져온다
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        //
        damageable = GetComponent<Damageable>();
    }

    // 화살
    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger(AnimationStrings.RangedAttack);
        }
    }

    // 고정 프레임마다 일어나는 update
    private void FixedUpdate()
    {
        // 플레이어의 이동의 처리
        // 입력에 따라 플레이어의 Rigidbody2D 속도를 업데이트한다.
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


    // Input System에서 받은 입력대로 이동을 처리하는 함수
    // param에 InputAction을 쓰기 위해서는 맨위에 using UnityEngine.InputSystem을 추가해준다.
    public void OnMove(InputAction.CallbackContext context)
    {
        // 입력 컨텍스트로부터 이동 입력값을 읽어온다.
        moveInput = context.ReadValue<Vector2>();
        // 이동이 0이 안니 경우 플레이어가 움직인다고 표현한다.
        IsMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
    }

    // 달릴때 상태변경해주는 함수
    public void OnRun(InputAction.CallbackContext context)
    {
        // context. started는 shift가 눌리기 시작한 시점을 의미한다.
        // 아래 else if문을 넣은 이유는 shift가 한번 눌리고 누르는 동안은 다른 상태가 되기 때문에
        // 눌리지 않은 상태인 caneled인 상태에서만 false로 바뀌도록 작성하였다.
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


    // 공격입력 처리
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
