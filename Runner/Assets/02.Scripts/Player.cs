using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   
    public float Hp
    {
        get => _hp; 
        
        set 
        { 
            if(value == _hp) return;
            _hp = value;
            OnHpChanged(value);
            
        }
    }


    [SerializeField] private float _hp; // Serialize : 데이터를 텍스트로, Deserialize : 텍스트를 데이터로
    public float hpMax
    {
        get { return _hpMax; }
    }
    [SerializeField] private float _hpMax = 100;
    public delegate void OnHpChangedHandler(float value);
    public event OnHpChangedHandler OnHpChanged;

}
