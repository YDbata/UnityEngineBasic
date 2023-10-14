using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageButton : MonoBehaviour
{
    private Button _button;
    [SerializeField] private Player _player;


    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => _player.hp -= 5.0f);
    }
}

