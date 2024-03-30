using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoHUD : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI magText;

    public static AmmoHUD instance;

    void Awake()
    {
        instance = this;
    }

    public void updateAmmoText(int AmmoCount, int maxAmmo)
    {
        ammoText.text = $"Ammo : {AmmoCount}/{maxAmmo}";
    }

    public void updateMagText(int MagCount)
    {
        magText.text = $"Mag : {MagCount}";
    }
}
