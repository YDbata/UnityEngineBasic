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


    [SerializeField] private float _hp; // Serialize : �����͸� �ؽ�Ʈ��, Deserialize : �ؽ�Ʈ�� �����ͷ�
    public float hpMax
    {
        get { return _hpMax; }
    }
    [SerializeField] private float _hpMax = 100;
    public delegate void OnHpChangedHandler(float value);
    /*public event OnHpChangedHandler OnHpChanged;*/
    public event Action<float> OnHpChanged;

    // Action �븮��
    // �Ķ���͸� 0~16������ ���� �� �ִ� void�� ��ȯ�ϴ� ������ �븮��
    public Action<int> action;

    // Func �븮��
    // �Ķ���͸� 0 ~16������ ���� �� �ְ� ���׸�Ÿ���� ��ȯ�ϴ� ������ �븮��
    public Func<int, float, string> func;
    public Func<int, bool> boolfunc;
    // Predicate�� ��������� predicate ���¸� ������ ���� ���� ������ ���� ������ ����

    // Predicate �븮�� -> �ڷᱸ������ Ư���� �ڷḦ Ž���Ҷ� ���
    // �Ķ���� 1��, boolŸ�� ��ȯ�ϴ� ����
    public Predicate<int> predicate;
    
    


    //Generic
    //� Ÿ���� �Ϲ�ȭ�ϴ� ��������� ����
    // where ������ : Generic Ÿ���� � Ÿ������ ������������ ������ �Ŵ� ������
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
