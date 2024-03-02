using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
	[Header("Rifle Things")]
	[SerializeField] Camera cam;
	[SerializeField] float damage = 10f;
	[SerializeField] float shootingRange = 100f;
	[SerializeField] Animator animator;
	[SerializeField] PlayerController playerController;

	[Header("Rifle Effects")]
	[SerializeField] ParticleSystem muzzleSpark;
	[SerializeField] GameObject ImpactEffect;

	[Header("Rifle shooting")]
	[SerializeField] private float fireCharge = 15f;
	private float nextTimeToShoot = 0;

	[Header("Rifle Ammunition")]
	[SerializeField] int maxAmmo = 20;// 최대 총알 갯수
	[SerializeField] int mag = 15;// 탄창 갯수
	[SerializeField] int curAmmo;//현재 총알 갯수
	[SerializeField] float reloadingTime = 1.3f;// 재장전 시간
	[SerializeField] bool setReloading = false;// 재장선 상태

	[Header("sound Effact")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;

    private void Awake()
	{
		curAmmo = maxAmmo;
	}

	// Update is called once per frame
	void Update()
    {
		if (setReloading) return;

        if (curAmmo <= 0 && mag > 0)
        {
			//TODO 재장전
			StartCoroutine(Reload());
			return;
        }

		bool onFire = Menus.ShootButtonClicked;// Input.GetButton("Fire1");
		//마우스 왼쪽 버튼 눌렸을 때
		if (onFire && Time.time >= nextTimeToShoot)
		{
			animator.SetBool("Fire", true);
			animator.SetBool("Idle", false);
			nextTimeToShoot = Time.time + 1 / fireCharge;
			Shoot();

		}
		//마우스 왼쪽 버튼을 뗐을 때
		else if(!onFire)
		{
			animator.SetBool("Fire", false);
			animator.SetBool("Idle", true);
		}
    }

	private void Shoot()
	{
		curAmmo--;
		AmmoHUD.instance.updateAmmoText(curAmmo, maxAmmo);
        AmmoHUD.instance.updateMagText(mag);

        RaycastHit hitInfo;

		muzzleSpark.Play();
		audioSource.PlayOneShot(clip);
		if (Physics.Raycast(cam.transform.position,cam.transform.forward,
			out hitInfo, shootingRange))
		{
			//.Log(hitInfo.collider.name);

			Damageable damageable = hitInfo.collider.gameObject.GetArountComponent<Damageable>();

			Enemy enemy = hitInfo.collider.gameObject.GetArountComponent<Enemy>();

			if (damageable)
			{
				damageable.HitDamage(damage);
				GameObject impact = Instantiate(ImpactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
				//GameObject impact = Instantiate(ImpactEffect, hitInfo.point,Quaternion.identity);
				Destroy(impact, 1.0f);
			}

			if (enemy)
			{
				enemy.HitDamage(damage);
				GameObject impact = Instantiate(ImpactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
				//GameObject impact = Instantiate(ImpactEffect, hitInfo.point,Quaternion.identity);
				Destroy(impact, 1.0f);
			}
		}
	}

	IEnumerator Reload()
	{
		playerController.CanMove = false;
		setReloading = true;
		Debug.Log("Reloading...");

		animator.SetBool("Reloading", true);

		yield return new WaitForSeconds(reloadingTime);
        animator.SetBool("Reloading", false);
		mag--;
		curAmmo = maxAmmo;
        AmmoHUD.instance.updateAmmoText(curAmmo, maxAmmo);
        AmmoHUD.instance.updateMagText(mag);
		playerController.CanMove = true;
		setReloading = false;
	}
}
