using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailStatusDisplay : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Text MaxhealthText;

    // Update is called once per frame
    void Update()
    {
        MaxhealthText.text = string.Format("{0:0}", health.GetMaxHealthPoints());
    }
}
