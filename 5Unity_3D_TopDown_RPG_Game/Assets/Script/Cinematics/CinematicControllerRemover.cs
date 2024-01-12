using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicControllerRemover : MonoBehaviour
{

    GameObject player;
    [SerializeField] private bool PlayOnStart;
    [SerializeField] private List<GameObject> DeactivesOnStoped;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        if(PlayOnStart)
        {
            GetComponent<PlayableDirector>().Play();
        }
    }

    private void OnEnable()
    {
        GetComponent<PlayableDirector>().played += DisableController;
        GetComponent<PlayableDirector>().stopped += EnableController;
    }

    private void OnDisable()
    {
        GetComponent<PlayableDirector>().played -= DisableController;
        GetComponent<PlayableDirector>().stopped -= EnableController;
    }

    private void EnableController(PlayableDirector director)
    {
        player.GetComponent<PlayerController>().enabled = true;
        for(int i = 0;i < DeactivesOnStoped.Count; i++)
        {
            DeactivesOnStoped[i].SetActive(false);
        }
    }

    private void DisableController(PlayableDirector director)
    {
        player.GetComponent<ActionScheduler>().CancleCurrentAction();
        player.GetComponent<PlayerController>().enabled = false;
    }
}
