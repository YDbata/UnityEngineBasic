using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    [SerializeField] private Text damageText;

    public void DestroyText()
    {
        Destroy(gameObject);
    }


    public void SetValue(float amount)
    {
        damageText.text = string.Format("{0:0}", amount);
    }
}
