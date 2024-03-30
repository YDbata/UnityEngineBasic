using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
	[SerializeField] private float walkSpeed = 5;
	[SerializeField] private float sprintSpeed = 8;
	[SerializeField] private float ratationSpeed = 10;
	[SerializeField] private CharacterController _controller;
	[SerializeField] private TPSCameraController _cameraController;
	[SerializeField] private Transform playerCamera;

	[SerializeField] private Transform GroundCheck;
	[SerializeField] private float surfaceDistance = 0.4f;
	[SerializeField] private LayerMask groundMask;
	[SerializeField] private float Gravity = -9.81f;
	[SerializeField] private float jumpRange = 1.0f;

	[Header("PLayer Health")]
	private float playerHealth = 100;
	private float currentHealth = 0;
	[SerializeField] HealthBar healthBar;

	[Header("GUI")]
	[SerializeField] GameObject EndGameMenuUI;

	private Quaternion targetRotation;
	private float turnCalmTime = 0.1f;
	private float turnCalmVelocity;

	private bool isGrounded = false;
	private Vector3 velocity;

	private Animator animator;

	public bool CanMove = true;

	private void Awake()
	{
		_controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		currentHealth = playerHealth;
		healthBar.GiveFullHealth();
	}
	
	void Start()
    {
		//Cursor.lockState = CursorLockMode.Locked;
	}
	private void Update()
	{
		//땅에 닿았는지 확인하는 코드
		isGrounded = Physics.CheckSphere(GroundCheck.position, surfaceDistance, groundMask);

		if (isGrounded && velocity.y < 0)
		{
			velocity.y = -2.0f;
		}

		//중력 연산
		velocity.y += Gravity * Time.deltaTime;
		_controller.Move(velocity * Time.deltaTime);

		//Move();
		Jump();
	}


	private void Move()
	{
		if (!CanMove) return;
		Vector3 direction = Vector3.zero;
#if UNITY_EDITOR
		//0 ...... 1
		float x = Input.GetAxisRaw("Horizontal");
		float z = Input.GetAxisRaw("Vertical");

		bool isSprint = Input.GetKey(KeyCode.LeftShift);

		direction = new Vector3(x, 0, z).normalized;
#elif UNITY_ANDROID
		direction = Joystick.InputDir;
#endif
		float speed = isSprint ? sprintSpeed : walkSpeed;


		if (direction.magnitude < 0.1f)
		{
			speed = 0;
		}

		float targetAngle = Mathf.Atan2(direction.x, direction.z) * 
			Mathf.Rad2Deg + playerCamera.eulerAngles.y;

		float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, 
			targetAngle, ref turnCalmVelocity, turnCalmTime);

		transform.rotation = Quaternion.Euler(0, angle, 0);

		Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

		_controller.Move(moveDirection.normalized * Time.deltaTime * speed);

		animator.SetFloat("Speed", speed);
		//transform.position = Vector3.MoveTowards(transform.position, movedir, speed);)
	}
	private void Jump()
	{
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			velocity.y = Mathf.Sqrt(jumpRange * -2 * Gravity);
			animator.SetTrigger("Jump");
		}
	}

	public void HitDamage(float takeDamage)
	{
		currentHealth -= takeDamage;
		healthBar.SetHealth(currentHealth / playerHealth);
		if (currentHealth <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		EndGameMenuUI.SetActive(true);
		Destroy(gameObject, 1.0f);
	}
}
