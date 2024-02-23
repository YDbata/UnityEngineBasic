using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
	
	[SerializeField] public float walkSpeed = 5;
	[SerializeField] public float sprintSpeed = 5;
	[SerializeField] private float rotationSpeed = 10;
	[SerializeField] private CharacterController _controller;
	[SerializeField] private TPSCameraController _cameraController;

	[SerializeField] private Transform playerCamera;
	[SerializeField] private Transform GroundCheck;
	[SerializeField] private float surfaceDistance = 0.4f;
	[SerializeField] private LayerMask groundMask;
	[SerializeField] private float Gravity = -9.82f;
	[SerializeField] private float jumpRange = 1;

	public bool CanMove = true;
    
    //[SerializeField] private GameObject LookAt;

    private Quaternion targetRotation;
	public float turnCalmTime = 0.1f;
	private float turnCalmVelocity;

	private bool isGrounded = false;
	private Vector3 velocity;

	private Animator animator;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
		//TODO : jump 애니메이션 자연스럽게
		Jump();
		//  transform.Rotate(Vector3.up);

	}



	private void Move()
	{
		if(!CanMove) { return; }
		Vector3 direction = Vector3.zero;
#if UNITY_EDITOR
        float x = Input.GetAxis("Horizontal"); // 0과 1로만 나옴
        float z = Input.GetAxis("Vertical");
        direction = new Vector3(x, 0, z);
#elif UNITY_ANDROID
		direction = Joystick.InputDir;
#endif


/*		float moveAmount = Mathf.Clamp01(Mathf.Abs(direction.x) + Mathf.Abs(direction.z));
		Vector3 movedir = _cameraController.PlanarRotationY*direction;*/
        bool isSprint = Input.GetKey(KeyCode.LeftShift);

        float speed = isSprint ? sprintSpeed : walkSpeed;

		if(direction.magnitude < 0.1f)
		{
			speed = 0;
		}
		Debug.Log("direction log");

		float targetAngle = Mathf.Atan2(direction.x,direction.z)*Mathf.Rad2Deg + playerCamera.eulerAngles.y;
		float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
		transform.rotation = Quaternion.Euler(0, angle, 0);

		Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;


        _controller.Move(moveDirection * Time.deltaTime * speed);

		animator.SetFloat("Speed", speed); //_controller.velocity.magnitude 가능
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

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpRange * -2 * Gravity);
            animator.SetTrigger("Jump");
        }
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
