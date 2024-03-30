using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

enum EEnumyType
{
	None,
	Solder,
	Dron,
}

public class ShootState : MonoBehaviour, IState
{
	[SerializeField] private EEnumyType enemytype;

	[Header("Rifle shooting")]
	[SerializeField] Camera enemyVision;
	[SerializeField] float nextTimeToShoot = 2f;
	[SerializeField] Transform playerLookPoint;
	[SerializeField] float shootingRange = 50;
	[SerializeField] float Damage = 5;

	[Header("Rifle Effects")]
	[SerializeField] ParticleSystem muzzleSpark;
	[SerializeField] ParticleSystem muzzleFlame;

	[Header("Sounds")]
	//[SerializeField] DronSound dronSound;
	[SerializeField] AudioSource audioSource;
	[SerializeField] List<AudioClip> audioClipList;

	private Animator animator;
	private NavMeshAgent agent;

	private Enemy currentEnemy;
	private bool canShoot = true;
	public void EnterState(Enemy enemy)
	{
		if (!animator) animator = gameObject.GetArountComponent<Animator>();
		if (!agent) agent = gameObject.GetArountComponent<NavMeshAgent>();
		//if (!dronSound) dronSound = gameObject.GetOrAddComponent<DronSound>();
		currentEnemy = enemy;

		animator.SetBool("Walk", false);
		animator.SetBool("AimRun", false);
		animator.SetBool("Shoot", true);
		animator.SetBool("Die", false);
	}

	public void UpdateState()
	{
		agent.SetDestination(transform.position);

		currentEnemy.EnemyModel.LookAt(playerLookPoint);

		if (canShoot)
		{
			muzzleSpark.Play();
			if(muzzleFlame && !muzzleFlame.isPlaying) muzzleFlame.Play();
			switch (enemytype)
			{
				case EEnumyType.Solder:
					PlaySound("Gun Machine Gun 444");
					break;
				case EEnumyType.Dron:
                    PlaySound("Gun Machine Gun 353");
                    PlaySound("FlameThrower");
					break;

            }
			//if(dronSound) dronSound.PlayShootSound();
			//if(dronSound) dronSound.PlayFlameSound();

			RaycastHit hitInfo;

			if (Physics.Raycast(enemyVision.transform.position, enemyVision.transform.forward,
				out hitInfo, shootingRange))
			{
				Debug.Log("Shooting " + hitInfo.collider.name);
				PlayerController player = hitInfo.transform.GetComponent<PlayerController>();
				if (player)
				{
					player.HitDamage(Damage);
				}

				animator.SetBool("Walk", false);
				animator.SetBool("AimRun", false);
				animator.SetBool("Shoot", true);
				animator.SetBool("Die", false);
			}

			canShoot = false;
			Invoke(nameof(ResetShooting), nextTimeToShoot);
		}
	}

	private void ResetShooting() { canShoot = true; }

	private void PlaySound(string clipName)
	{
		AudioClip clip = audioClipList.Find((x) => x.name == clipName);
		if (clip)
		{
			audioSource.PlayOneShot(clip);
		}
	}

	public void ExitState()
	{
		animator.SetBool("Walk", false);
		animator.SetBool("AimRun", false);
		animator.SetBool("Shoot", false);
		animator.SetBool("Die", false);
	}


}
