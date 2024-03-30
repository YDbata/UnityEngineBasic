using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootSteps : MonoBehaviour
{
	private AudioSource audioSource;

	[Header("FootStep Source")]
	[SerializeField] private AudioClip[] footstepSound;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void Step()
	{
		AudioClip clip = GetRandomFootStep();
		audioSource.PlayOneShot(clip);
	}

	private AudioClip GetRandomFootStep()
	{
		return footstepSound[UnityEngine.Random.Range(0, footstepSound.Length)];
	}
}
