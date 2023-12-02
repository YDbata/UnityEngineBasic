using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public int healthRestore = 20;
    public Vector3 spinrotationSpeed = new Vector3(0, 180, 0);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable)
        {
            bool wasHealed = damageable.Heal(healthRestore);

            if (wasHealed)
                Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        transform.eulerAngles = spinrotationSpeed * Time.deltaTime;
    }
}
