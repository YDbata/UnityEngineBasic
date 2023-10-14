using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;

    // 이동거리 = 속력 * 시간
    // 고정 프레임당 이동거리 = 속력 * 고정프레임간 시간 변화
    private void FixedUpdate()
    {
        // transform.position += Vector3.forward * _speed * Time.fixedDeltaTime; 아래와 같은 내용
        
        transform.Translate(Vector3.forward * _speed * Time.fixedDeltaTime);
    }
}
