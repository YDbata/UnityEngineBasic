using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Turrat : MonoBehaviour, IDamageable
{
    //[SerializeField] NavMeshAgent agent;
    [SerializeField] Transform enemyModel;
    [SerializeField] Animator animator;
    [SerializeField] Camera enemyvision;

    [Header("Stats")]
    [SerializeField] float health = 100;
    [SerializeField] float shootingRange = 50;

    [Header("Enemy State")]
    [SerializeField] Transform PlayerBody;
    [SerializeField] Transform PlayerLookPoint;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float shootingRadius;
    [SerializeField] bool playerInShootingRadius;
    private bool isDead = false;
    

    [Header("Rifle Effects")]
    [SerializeField] ParticleSystem muzzleSpark;

    [Header("Rifle Shooting")]
    private bool canShoot = true;
    [SerializeField] private float nextTimeToShoot = 0.5f;

    private void Start()
    {
        if (isDead) return;
    }

    // Update is called once per frame
    void Update()
    {

        if(isDead) return;
        playerInShootingRadius = Physics.CheckSphere(enemyModel.position, shootingRadius, playerLayer);
        
        if (playerInShootingRadius) ShootPlayer();

    }

    private void ShootPlayer()
    {
        //agent.SetDestination(PlayerBody.position);
        //agent.isStopped = true;

        enemyModel.LookAt(PlayerLookPoint);
        RaycastHit hitinfo;

        if (canShoot)
        {
            muzzleSpark.Play();
            animator.SetTrigger("Shoot");
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

 

    public void Die()
    {
        //agent.isStopped = true;
        shootingRadius = 0;
        playerInShootingRadius = false;
        isDead = true;
        animator.SetBool("Dead", true);

        Destroy(this.gameObject, 5);
    }

    public void HitDamage(float amount)
    {

        health -= amount;
        if (health < 0)
        {
            Die();
        }
    }
}
