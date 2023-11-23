using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



// Rigidbody2D�� ������Ʈ�� ����ϹǷ� �Ʒ� �����߰�(���� �ʿ� ����)
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // �ȴ� �ӵ� ���� ����
    public float walkSpeed = 5.0f;
    // �÷��̾��� �̵� �Է��� �����ϴ� ����
    Vector2 moveInput;
    // �÷��̾ �����̴��� Ȯ���ϴ� �Ӽ�
    public bool IsMoving { get; private set; }
    // Rigidbody2D ������Ʈ�� ���� ����
    Rigidbody2D rb;

    // GameObject�� Ȱ��ȭ �ɶ� ����Ǵ� �Լ�
    private void Awake()
    {
        // Awake�Լ����� Rigidbody2D ������Ʈ�� �����´�
        rb = GetComponent<Rigidbody2D>();
    }

    // ���� �����Ӹ��� �Ͼ�� update
    private void FixedUpdate()
    {
        // �÷��̾��� �̵��� ó��
        // �Է¿� ���� �÷��̾��� Rigidbody2D �ӵ��� ������Ʈ�Ѵ�.
        rb.velocity = new Vector2(moveInput.x * walkSpeed, rb.velocity.y);
    }

    // Input System���� ���� �Է´�� �̵��� ó���ϴ� �Լ�
    // param�� InputAction�� ���� ���ؼ��� ������ using UnityEngine.InputSystem�� �߰����ش�.
    public void OnMove(InputAction.CallbackContext context)
    {
        // �Է� ���ؽ�Ʈ�κ��� �̵� �Է°��� �о�´�.
        moveInput = context.ReadValue<Vector2>();
        // �̵��� 0�� �ȴ� ��� �÷��̾ �����δٰ� ǥ���Ѵ�.
        IsMoving = moveInput != Vector2.zero;
    }
}
