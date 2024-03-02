using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : MonoBehaviour , IState
{
	[SerializeField] Transform[] wayPoints;
	[SerializeField] int currentWayPoint = 0;
	[SerializeField] float walkingPointRadius = 2;

	private Enemy currentEnemy;
	private Animator animator;
	private NavMeshAgent agent;

	public void EnterState(Enemy enemy)
	{
		if(!animator) animator = gameObject.GetArountComponent<Animator>();
		if (!agent) agent = gameObject.GetArountComponent<NavMeshAgent>();

		currentEnemy = enemy;

		animator.SetBool("Walk", true);
		animator.SetBool("AimRun", false);
		animator.SetBool("Shoot", false);
		animator.SetBool("Die", false);
	}

	public void UpdateState()
	{
		if (Vector3.Distance(wayPoints[currentWayPoint].position, currentEnemy.EnemyModel.position)
			< walkingPointRadius)
		{
			currentWayPoint++;
			if (currentWayPoint >= wayPoints.Length)
			{
				currentWayPoint = 0;
			}
		}
		agent.SetDestination(wayPoints[currentWayPoint].position);
	}

	public void ExitState()
	{
		animator.SetBool("Walk", false);
		animator.SetBool("AimRun", false);
		animator.SetBool("Shoot", false);
		animator.SetBool("Die", false);
	}
}
