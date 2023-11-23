using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



// Rigidbody2D의 컴포넌트를 사용하므로 아래 구문추가(굳이 필요 없음)
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // 걷는 속도 설정 변수
    public float walkSpeed = 5.0f;
    // 플레이어의 이동 입력을 저장하는 벡터
    Vector2 moveInput;
    // 플레이어가 움직이는지 확인하는 속성
    public bool IsMoving { get; private set; }
    // Rigidbody2D 컴포넌트에 대한 참조
    Rigidbody2D rb;

    // GameObject가 활성화 될때 실행되는 함수
    private void Awake()
    {
        // Awake함수에서 Rigidbody2D 컴포넌트를 가져온다
        rb = GetComponent<Rigidbody2D>();
    }

    // 고정 프레임마다 일어나는 update
    private void FixedUpdate()
    {
        // 플레이어의 이동의 처리
        // 입력에 따라 플레이어의 Rigidbody2D 속도를 업데이트한다.
        rb.velocity = new Vector2(moveInput.x * walkSpeed, rb.velocity.y);
    }

    // Input System에서 받은 입력대로 이동을 처리하는 함수
    // param에 InputAction을 쓰기 위해서는 맨위에 using UnityEngine.InputSystem을 추가해준다.
    public void OnMove(InputAction.CallbackContext context)
    {
        // 입력 컨텍스트로부터 이동 입력값을 읽어온다.
        moveInput = context.ReadValue<Vector2>();
        // 이동이 0이 안니 경우 플레이어가 움직인다고 표현한다.
        IsMoving = moveInput != Vector2.zero;
    }
}
