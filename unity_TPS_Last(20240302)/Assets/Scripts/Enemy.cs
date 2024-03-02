using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
	[SerializeField] NavMeshAgent agent;
	[SerializeField] Transform enemyModel;
	[SerializeField] Animator animator;

	[Header("Stats")]
	[SerializeField] ParticleSystem DestoryEffect;
	[SerializeField] float maxhealth = 100;
	[SerializeField] float curhealth = 100;
	[SerializeField] HealthBar healthBar;

	[Header("Enemy State")]
	[SerializeField] Transform PlayerBody;

	[SerializeField] LayerMask playerLayer;
	[SerializeField] float visionRadius;
	[SerializeField] float shootingRadius;
	[SerializeField] bool playerInVisionRadius;
	[SerializeField] bool playerInShootingRadius; // 플레이어를 공격할 상태인지

	[Header("Enemy State")]
	[SerializeField] private PatrolState	patrolState;
	[SerializeField] private ChaseState		chaseState;
	[SerializeField] private ShootState		shootState;

	private EnemyStateContext enemyStateContext;

	private bool isDead = false;

	public Transform EnemyModel => enemyModel;
	public Transform Target => PlayerBody;


	private void Awake()
	{
		enemyStateContext = new EnemyStateContext(this);
		curhealth = maxhealth;
		healthBar.GiveFullHealth();
		isDead = false;
	}

	private void Start()
	{
		enemyStateContext.Transition(patrolState);
	}

	// Update is called once per frame
	void Update()
    {
		if (isDead) return;
		playerInVisionRadius = Physics.CheckSphere(enemyModel.position, visionRadius, playerLayer);
		playerInShootingRadius = Physics.CheckSphere(enemyModel.position, shootingRadius, playerLayer);

		if(!playerInVisionRadius && !playerInShootingRadius) UpdateState(EState.Patrol);
		if(playerInVisionRadius && !playerInShootingRadius) UpdateState(EState.Chase);
		if (playerInVisionRadius && playerInShootingRadius) UpdateState(EState.Shoot);

		enemyStateContext.CurrentState.UpdateState();
	}

	private void UpdateState(EState eState)
	{
		switch (eState)
		{
			case EState.Patrol:
				enemyStateContext.Transition(patrolState);
				break;
			case EState.Chase:
				enemyStateContext.Transition(chaseState);
				break;
			case EState.Shoot:
				enemyStateContext.Transition(shootState);
				break;
		}
	}

	public void HitDamage(float amount)
	{
		visionRadius = 30;
		curhealth -= amount;
		healthBar.SetHealth(curhealth / maxhealth);
		if (curhealth <= 0)
		{
			if (DestoryEffect) DestoryEffect.Play();
			Die();
		}
	}

	public void Die()
	{
		agent.isStopped = true;

		isDead = true;

		animator.SetBool("Die", true);
		Destroy(this.gameObject,5);
	}

}
