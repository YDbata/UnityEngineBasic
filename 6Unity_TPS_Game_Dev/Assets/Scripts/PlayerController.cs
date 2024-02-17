using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5;
    [SerializeField] private CharacterController _controller;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontal_axit = Input.GetAxis("Horizontal");
        float vertical_axit = Input.GetAxis("Vertical");
        // Debug.Log("horizontal_axit "+ horizontal_axit);

        float Speed = walkSpeed;

        Vector3 direction = new Vector3(horizontal_axit, 0 , vertical_axit).normalized;
        if(direction.magnitude < 0.1f)
        {
            Speed = 0;
        }

        _controller.Move(direction*Time.deltaTime*Speed);
        
    }
}
