using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Unity.VisualScripting;
using UnityEngine;

public class Operatable<T>
{
    public T value;
    public static Operatable<T> operator +(Operatable<T> a, Operatable<T> b)
    {
        // something to do plusing
        Console.WriteLine($"{a}, {b}");
        return a + b;
    }

}

public class Player : MonoBehaviour
{
   
    public float hp
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
    /*public event OnHpChangedHandler OnHpChanged;*/
    public event Action<float> OnHpChanged;

    // Action 대리자
    // 파라미터를 0~16개까지 받을 수 있는 void를 반환하는 형태의 대리자
    public Action<int> action;

    // Func 대리자
    // 파라미터를 0 ~16개까지 받을 수 있고 제네릭타입을 반환하는 형태의 대리자
    public Func<int, float, string> func;
    public Func<int, bool> boolfunc;
    // Predicate와 비슷하지만 predicate 형태를 별개로 많이 쓰기 때문에 따로 떨어져 나옴

    // Predicate 대리자 -> 자료구조에서 특별한 자료를 탐색할때 사용
    // 파라미터 1개, bool타입 반환하는 형태
    public Predicate<int> predicate;
    
    


    //Generic
    //어떤 타입을 일반화하는 사용자정의 서식
    // where 한정자 : Generic 타입이 어떤 타입으로 공변가능한지 제한을 거는 한정자
    public T Sum<T>(T a, T b) where T : Operatable<T> => (a + b).value;

    public int Sum(int a, int b) => a + b;
    public float Sum(float a, float b) => a + b;
    public double Sum(double a, double b) => a + b;

    public void Start()
    {
        predicate += (value) => { Debug.Log("1"); return value > 3;};
        predicate += (value) => { Debug.Log("2"); return value < 5; };
    }
    public void DepleteHp(float amount)
    {
        /*Console.WriteLine(Sum<int>(1, 2));*/

        Debug.Log(predicate(4));
        hp -= amount;
    }

}
