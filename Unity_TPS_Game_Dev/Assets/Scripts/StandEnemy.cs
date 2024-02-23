using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class StandEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform enemyModel;
    [SerializeField] Animator animator;
    [SerializeField] Camera enemyvision;


    [Header("stand")]
    [SerializeField] Transform wayPoints;


    [Header("walk")]
    // 이동 지점
    [SerializeField] float enemySpeed;
    [SerializeField] float walkingPointRadius = 2;

    [Header("Stats")]
    [SerializeField] float health = 100;
    [SerializeField] float shootingRange = 50;

    [Header("Enemy State")]
    [SerializeField] Transform PlayerBody;
    [SerializeField] Transform PlayerLookPoint;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float visionRadius;
    [SerializeField] float shootingRadius;
    [SerializeField] bool playerInVisionRadius;
    [SerializeField] bool playerInShootingRadius;
    private bool isDead = false;
    

    [Header("Rifle Effects")]
    [SerializeField] ParticleSystem muzzleSpark;

    [Header("Rifle Shooting")]
    private bool canShoot = true;
    [SerializeField] private float nextTimeToShoot = 2f;

    private void Start()
    {
        if (isDead) return;
    }

    // Update is called once per frame
    void Update()
    {

        if(isDead) return;
        playerInVisionRadius = Physics.CheckSphere(enemyModel.position, visionRadius, playerLayer);
        playerInShootingRadius = Physics.CheckSphere(enemyModel.position, shootingRadius, playerLayer);


        if (!playerInVisionRadius && !playerInShootingRadius)
        {
            if(Vector3.Distance(wayPoints.position, enemyModel.position) > walkingPointRadius)
            {
                if (animator.GetBool("Walk") == false)
                {
                    animator.SetBool("Walk", true);
                    animator.SetBool("AimRun", false);

                }
                Patrol();
            }
            else
            {
                animator.SetBool("Walk", false);
                enemyModel.rotation = wayPoints.rotation;
            }
            
        }
        if (!playerInShootingRadius && playerInVisionRadius) ChasePlayer();
        if (playerInShootingRadius) ShootPlayer();

    }

    private void ShootPlayer()
    {
        //agent.SetDestination(PlayerBody.position);
        agent.isStopped = true;

        enemyModel.LookAt(PlayerLookPoint);
        RaycastHit hitinfo;

        if (canShoot)
        {
            muzzleSpark.Play();
            animator.SetBool("AimRun", false);
            animator.SetBool("Shoot", true);
            if (Physics.Raycast(enemyvision.transform.position, enemyvision.transform.forward, out hitinfo, shootingRange))
            {
                Debug.Log("Shooting" + hitinfo.collider.name);
                canShoot = false;
                Invoke(nameof(ResetShooting), nextTimeToShoot);
            }
        }
        
    }

    private void ResetShooting()
    {
        canShoot = true;
    }

    private void ChasePlayer()
    {
        agent.isStopped = false;
        if (agent.SetDestination(PlayerBody.position))
        {
            animator.SetBool("Walk", false);
            animator.SetBool("AimRun", true);
            animator.SetBool("Shoot", false);
        }
        else
        {
            animator.SetBool("AimRun", false);
        }

    }

    private void Patrol()
    {
        agent.isStopped = false;
        agent.SetDestination(wayPoints.position);
    }
    public void HitDamage(float amount)
    {
        visionRadius = 30;

        health -= amount;
        if (health < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        agent.isStopped = true;
        shootingRadius = 0;
        visionRadius = 0;
        playerInVisionRadius = false;
        playerInShootingRadius = false;
        isDead = true;
        animator.SetBool("Walk", false);
        animator.SetBool("Dead", true);

        Destroy(this.gameObject, 5);
    }
}
