using Clasee;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lesson_2
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] public float walkSpeed = 5.0f;
        [SerializeField] public float runSpeed = 8.0f;
        [SerializeField] public float jumpImpulse = 5;
        Rigidbody2D rb;

        public float CurrentMoveSpeed { 

            get {
                if (_isMoving)
                {
                    if (_isRunning)
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
                    return 0;
                }
            }
            set { walkSpeed = value; }
            
        }

        private bool _isMoving = false;
        public bool IsMoving
        {
            get {return _isMoving; }
            set {
                _isMoving = value;
                animator.SetBool("IsMoving", _isMoving);
                
            }
        }

        private bool _isRunning = false;
        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                _isRunning = value;
                animator.SetBool("IsRunning", _isRunning);

            }
        }

        private bool _isFacingRingt = true;

        public bool IsFacingRingt
        {
            get { return _isFacingRingt; }
            set
            {
                if(_isFacingRingt != value)
                {
                    transform.localScale *= new Vector2(-1, 1);
                }
                _isFacingRingt=value;
            }
        }

        Vector2 moveInput;
        Animator animator;
        TouchingDirections touchingDirections;


        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            touchingDirections = GetComponent<TouchingDirections>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            // Debug.Log(CurrentMoveSpeed);
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
            animator.SetFloat("yVelocity", rb.velocity.y);
        }

        public void OnRun(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                IsRunning = true;
            }
            else if(context.canceled)
            {
                IsRunning= false;
            }
        }

        private void SetFacingDirection(Vector2 direction)
        {
            if(moveInput.x >  0 && !IsFacingRingt)
            {
                IsFacingRingt = true;
            }
            else if(moveInput.x < 0 && IsFacingRingt)
            {
                IsFacingRingt = false;
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Debug.Log(context.ReadValue<Vector2>());
            moveInput = context.ReadValue<Vector2>();
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.started && touchingDirections)
            {
                animator.SetTrigger("Jump");
                rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
            }
        }

    }
}

