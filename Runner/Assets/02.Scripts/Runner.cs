using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;

    // �̵��Ÿ� = �ӷ� * �ð�
    // ���� �����Ӵ� �̵��Ÿ� = �ӷ� * ���������Ӱ� �ð� ��ȭ
    private void FixedUpdate()
    {
        // transform.position += Vector3.forward * _speed * Time.fixedDeltaTime; �Ʒ��� ���� ����
        
        transform.Translate(Vector3.forward * _speed * Time.fixedDeltaTime);
    }
}
