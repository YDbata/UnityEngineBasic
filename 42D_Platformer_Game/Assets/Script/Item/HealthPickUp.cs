using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public int healthRestroe = 20;
    public Vector3 spinrotationSpeed = new Vector3(0, 180, 0);
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (damageable)
        {
            bool wasHealed = damageable.Heal(healthRestroe);
            if (wasHealed)
            {
                audioSource.Play();
                spriteRenderer.enabled = false;

                Debug.Log("audio play time" + audioSource.clip.length);
                Destroy(this.gameObject, audioSource.clip.length);
            }
        }
    }

    private void Update()
    {
        transform.eulerAngles += spinrotationSpeed * Time.deltaTime;
    }
}
