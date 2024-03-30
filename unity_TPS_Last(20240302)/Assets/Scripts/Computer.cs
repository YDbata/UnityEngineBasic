using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [Header("Cumputer State")]
    public Light lights;
    private bool state = true;


    [Header("Computer Assign Things")]
    public Transform player;
    public float radius = 2.5f;
    [SerializeField] private KeyCode InteractionKey = KeyCode.F;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Menus.InteractionButtonClicked && Vector3.Distance(transform.position, player.position) < radius)
        {
            state = !state;
            ChangeState();
        }
    }

    private void ChangeState()
    {
        MissionComplete.Instance.UpdateMissionComplete(2, !state);
        if (state)
        {
            lights.intensity = 1;
        }
        else
        {
            audioSource.PlayOneShot(clip);
            lights.intensity = 0;
        }
    }
}
