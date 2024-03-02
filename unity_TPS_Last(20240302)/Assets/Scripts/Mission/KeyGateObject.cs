using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGateObject : MonoBehaviour
{
    [Header("Animations")]
    [SerializeField] private Animator gateAnimator;
    [SerializeField] private bool openGate;

    [SerializeField] private KeyList keyList;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;

    private string openParameter = "open";

    public void StartAnimation()
    {
        if (keyList.hasKey)
        {
            OpenGate();
        }
    }

    private void OpenGate()
    {
        openGate = !openGate;
        gateAnimator.SetBool(openParameter, openGate);
        audioSource.PlayOneShot(clip);
        if (openGate)
        {
            MissionComplete.Instance.UpdateMissionComplete(1, openGate);
        }
    }

}
