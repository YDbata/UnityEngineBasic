using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
	[SerializeField] private float walkSpeed = 5;
	[SerializeField] private float rotationSpeed = 10;
	[SerializeField] private CharacterController _controller;
	[SerializeField] private TPSCameraController _cameraController;

	[SerializeField] private Transform playerCamera;
	[SerializeField] private Transform GroundCheck;
	[SerializeField] private float surfaceDistance = 0.4f;
	[SerializeField] private LayerMask groundMask;
	[SerializeField] private float Gravity = -9.82f;
	[SerializeField] private float jumpRange = 1;
	//[SerializeField] private GameObject LookAt;
	private Quaternion targetRotation;
	public float turnCalmTime = 0.1f;
	private float turnCalmVelocity;

	private bool isGrounded = false;
	private Vector3 velocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _controller = GetComponent<CharacterController>();

	}
	float z = 0;
	private void Update()
	{

		/*		if (Input.GetKey(KeyCode.UpArrow))
				{
					z += 1;
					transform.rotation = Quaternion.Euler(0, 0, z);
				}
				Debug.Log("rotation : " + transform.rotation);*/
		// 땅에 닿았는지 확인하는 코드
		isGrounded = Physics.CheckSphere(GroundCheck.position, surfaceDistance, groundMask);

		if(isGrounded && velocity.y < 0)
		{
			velocity.y = -2.0f;
		}

		// 중력 연산
		velocity.y += Gravity * Time.deltaTime;
		_controller.Move(velocity*Time.deltaTime);

		Move();
		Jump();
		//  transform.Rotate(Vector3.up);

	}

	private void Jump()
	{
		if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			velocity.y = Mathf.Sqrt(jumpRange * -2 * Gravity);
		}
	}

	private void Move()
	{
		Vector3 direction = Vector3.zero;
#if UNITY_EDITOR
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        direction = new Vector3(x, 0, z);
#elif UNITY_ANDROID
		direction = Joystick.InputDir;
#endif


		float moveAmount = Mathf.Clamp01(Mathf.Abs(direction.x) + Mathf.Abs(direction.z));
		Vector3 movedir = _cameraController.PlanarRotationY*direction;

		float speed = walkSpeed;

		if(direction.magnitude < 0.1f)
		{
			speed = 0;
		}

		float targetAngle = Mathf.Atan2(direction.x,direction.z)*Mathf.Rad2Deg + playerCamera.eulerAngles.y;
		float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
		transform.rotation = Quaternion.Euler(0, angle, 0);

		Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;


        _controller.Move(moveDirection * Time.deltaTime * speed);

        /*if (moveAmount > 0)
		{
			_controller.Move(movedir * Time.deltaTime * speed);
			targetRotation = Quaternion.LookRotation(movedir);
		}


		//transform.position = Vector3.MoveTowards(transform.position, movedir, speed);
		_controller.Move(movedir * Time.deltaTime * speed);
		transform.rotation = 
			Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime*rotationSpeed);
		//LookAt.transform.rotation = Quaternion.RotateTowards(LookAt.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
*/

    }
}



/*if(Application.platform == RuntimePlatform.WindowsPlayer)
{
    float x = Input.GetAxis("Horizontal");
    float z = Input.GetAxis("Vertical");

    direction = new Vector3(x, 0, z);
}
else
{
    direction = Joystick.InputDir;
}*/
