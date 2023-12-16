using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour, IAction
{
    [SerializeField] private Transform target;
    [SerializeField] private float maxMoveSpeed = 6f;
    [SerializeField] private float maxNavPathLength = 40;

    NavMeshAgent navMeshAgent;
    CharacterController _controller;
    Animator _animator;
    ActionScheduler _actionScheduler;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _actionScheduler = GetComponent<ActionScheduler>();
    }

    private void Update()
    {
        UpdateAnimator();
    }

    public void StartMoveAction(Vector3 destination, float speedFraction)
    {
        _actionScheduler.StartAction(this);
        MoveTo(destination, speedFraction);
    }

    public bool CanMoveTo(Vector3 destination)
    {
        NavMeshPath path = new NavMeshPath();
        bool hasPath = NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path);
        if(!hasPath) { 
            return  false;
        }
        if(path.status != NavMeshPathStatus.PathComplete)
        {
            return false;
        }

        if (GetPathLength(path) > maxNavPathLength) { return false; }
        return true;
    }

    private float GetPathLength(NavMeshPath path)
    {
        float total = 0;
        if(path.corners.Length < 2) { return total; }
        for(int i = 0;i < path.corners.Length - 1;i++) {
            total += Vector3.Distance(path.corners[i], path.corners[i + 1]);
        }
        return total;
    }

    public void MoveTo(Vector3 destination, float speedFaction)
    {
        navMeshAgent.destination = destination;
        navMeshAgent.speed = maxMoveSpeed * Mathf.Clamp01(speedFaction);
        navMeshAgent.isStopped = false;
    }


    private void UpdateAnimator()
    {
        Vector3 velocity = navMeshAgent.velocity;
        // InverseTranseformDirection : 월드좌표에서 캐릭터의 방향으로 변환한다. 추가 조사 필요
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        _animator.SetFloat("Speed", speed);
    }

    public void Cancle()
    {
        navMeshAgent.isStopped = true;
    }
}
