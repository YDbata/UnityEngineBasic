using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DronSound : MonoBehaviour
{
	[Header("Dron Sounds")]
	[SerializeField] AudioClip shootingSound;
	[SerializeField] AudioClip flameSound;
	private AudioSource audioSource;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void PlayShootSound()
	{
		audioSource.PlayOneShot(shootingSound);
	}

	public void PlayFlameSound()
	{
		audioSource.PlayOneShot(flameSound);
	}
}
