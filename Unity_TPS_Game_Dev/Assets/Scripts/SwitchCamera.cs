using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchCamera : MonoBehaviour
{
    [Header("Cameras")]
    public GameObject aimCam;
    public GameObject ThirdPersonCam;
/*    public GameObject aimCanvas;
    public GameObject ThirdPersonCanvas;*/
    public Sprite aimcrosshair;
    public Sprite crosshair;
    public Image CrosshairImage;

    public Animator animator;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2")) // 마우스 우클릭
        {
            animator.SetBool("Aiming", true);
            ThirdPersonCam.SetActive(false);
            aimCam.SetActive(true);
            CrosshairImage.sprite = aimcrosshair;
            /*ThirdPersonCanvas.SetActive(false);
            aimCanvas.SetActive(true);*/
        }
        else // if(!Input.GetButton("Fire2"))
        {
            animator.SetBool("Aiming", false);
            ThirdPersonCam.SetActive(true);
            aimCam.SetActive(false) ;
            CrosshairImage.sprite = crosshair;
            /*            ThirdPersonCanvas.SetActive(true);
                        aimCanvas.SetActive(false);*/

        }
    }
}
