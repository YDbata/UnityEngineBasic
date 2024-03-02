using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GeneratorButton : MonoBehaviour
{
    [Header("Generator Light and Button")]
    public GameObject greenLight;
    public GameObject redLight;

    public bool state = true;

    [Header("Generator Sound Effect And Radius")]
    [SerializeField] private float radius = 10f;
    [SerializeField] private Transform player;

    [SerializeField] private KeyCode InteractionKey = KeyCode.F;

    [SerializeField] private AudioSource generatorAudioSource;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;

    [Header("Mission Check")]
    [SerializeField] private GameObject mission2;

    // Update is called once per frame
    void Update()
    {
        if(Menus.InteractionButtonClicked && Vector3.Distance(transform.position,player.position) < radius)
        {
            state = !state;
            ChangeState();
        }
    }

    private IEnumerator ChangeState()
    {
        if (!MissionComplete.Instance.Mission2Complete())
        {
            mission2.SetActive(true);
            yield return new WaitForSeconds(3);
            mission2.SetActive(false);
        }
        else
        {

            MissionComplete.Instance.UpdateMissionComplete(3, !state);
            if (state)
            {
                greenLight.SetActive(true);
                redLight.SetActive(false);
            }
            else
            {
                generatorAudioSource.Pause();
                audioSource.PlayOneShot(clip);
                greenLight.SetActive(false);
                redLight.SetActive(true);
            }
        }
    }
}
