using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalMission : MonoBehaviour
{

    [Header("Car Assign Things")]
    public Transform player;
    public float radius = 2.5f;
    [SerializeField] private KeyCode InteractionKey = KeyCode.F;
    [SerializeField] private string EndGameScene = "";

    // Start is called before the first frame update
    void Update()
    {
        if (Menus.InteractionButtonClicked && Vector3.Distance(transform.position, player.position) < radius)
        {
            // TODO : 마지막 미션인지 체크
            if (!MissionComplete.Instance.IsFinalMission())
            {
                return ;
            }
            SceneManager.LoadScene(EndGameScene);
        }
    }

}
