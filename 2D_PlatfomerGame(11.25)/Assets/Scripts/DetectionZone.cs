using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public UnityEvent NoCollidersRemain;

    public List<Collider2D> detectedColiders = new List<Collider2D>();
    Collider2D col;

    // Start is called before the first frame update
    void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedColiders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedColiders.Remove(collision);

        if (detectedColiders.Count <= 0)
        {
            NoCollidersRemain?.Invoke();
        }
    }
}
