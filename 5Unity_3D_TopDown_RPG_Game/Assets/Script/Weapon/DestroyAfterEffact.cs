using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterEffact : MonoBehaviour
{
    [SerializeField] GameObject targetToDestroy = null;


    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<ParticleSystem>().IsAlive())
        {
            if(targetToDestroy != null)
            {
                Destroy(targetToDestroy);
            }
            else
            {
                Destroy(gameObject);
            }
        }        
    }
}
