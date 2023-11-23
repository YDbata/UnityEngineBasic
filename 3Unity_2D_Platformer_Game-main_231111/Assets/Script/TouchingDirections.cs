using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clasee
{
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
             // cast 설명 들음 : 레이저를 쏴서,...캐릭과 바닥이 붙어있는지 확인
             IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
             Debug.Log(IsGrounded);
        }


        
    }

}
