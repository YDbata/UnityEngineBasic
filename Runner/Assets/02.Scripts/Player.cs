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


    [SerializeField] private float _hp; // Serialize : �����͸� �ؽ�Ʈ��, Deserialize : �ؽ�Ʈ�� �����ͷ�
    public float hpMax
    {
        get { return _hpMax; }
    }
    [SerializeField] private float _hpMax = 100;
    public delegate void OnHpChangedHandler(float value);
    public event OnHpChangedHandler OnHpChanged;

}
