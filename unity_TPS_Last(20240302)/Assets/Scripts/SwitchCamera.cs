using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
	[Header("Cameras")]
	public GameObject aimCam;
	public GameObject ThirdPersonCam;
	public GameObject aimCamCanvas;
	public GameObject ThirdPersonCamCanva;

	public Animator animator;

    // Update is called once per frame
    void Update()
    {
		if (Input.GetButton("Fire2"))
		{
			animator.SetBool("Aiming", true);

			ThirdPersonCam.SetActive(false);
			aimCam.SetActive(true);

			ThirdPersonCamCanva.SetActive(false);
			aimCamCanvas.SetActive(true);
		}
		else
		{
			animator.SetBool("Aiming",false);

			ThirdPersonCam.SetActive(true);
			aimCam.SetActive(false);

			ThirdPersonCamCanva.SetActive(true);
			aimCamCanvas.SetActive(false);
		}
	}
}
