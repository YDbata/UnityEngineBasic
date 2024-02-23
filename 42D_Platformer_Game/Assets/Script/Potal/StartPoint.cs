using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPoint : MonoBehaviour
{
    // 이동한 맵이름
    public string StartPointMapName;

    private PlayerController player;
    private CameraControll maincamera;
    private ParallaEffect paralla;
    GameObject Player;


    // Start is called before the first frame update
    void Start()
    {
        maincamera = FindObjectOfType<CameraControll>();
        player = FindObjectOfType<PlayerController>();
        Player = GameObject.Find("Player");
        paralla.GetComponent<ParallaEffect>().cam = Camera.main;
        paralla.GetComponent<ParallaEffect>().followTarget = Player.transform;


        if(StartPointMapName == player.currentMapName)
        {
            maincamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            player.transform.position = this.transform.position;

        }

        // BG parallax 반영



    }

}
