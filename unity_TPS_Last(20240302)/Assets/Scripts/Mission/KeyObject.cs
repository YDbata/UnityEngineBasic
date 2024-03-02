using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObject : MonoBehaviour
{
    [SerializeField] private KeyList KeyList = null;
    [SerializeField] private bool key;
    [SerializeField] private bool gate;

    [SerializeField] private KeyGateObject gateObject;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;

    public void FoundObject()
    {
        if (key)
        {
            audioSource.PlayOneShot(clip);
            KeyList.hasKey = true;
            gameObject.SetActive(false);

        }else if (gate)
        {
            gateObject.StartAnimation();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
