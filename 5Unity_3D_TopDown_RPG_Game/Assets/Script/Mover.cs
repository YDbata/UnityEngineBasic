using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float maxMoveSpeed = 6f;
    [SerializeField] private float maxNavPathLength = 40;

    NavMeshAgent navMeshAgent;
    Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
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

}
