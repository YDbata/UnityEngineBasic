using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Runner : MonoBehaviour
{
    public bool isMovable
    {
        get => _isMovable;
        set
        {
            _isMovable = value;
            if (value == false)
                speedModified = 0.0f;
        }
    }

    private bool _isMovable = true;


    public float speedModified
    {
        get => _speedModified;
        set
        {
            _speedModified = value;
            _animator.SetFloat("speed", value);
        }
    }

    


    [SerializeField] private float _speed = 3.0f;
    private float _speedModified;

    [SerializeField] private float _speedModifyingPeriod = 1.0f;
    private float _modifyingTimer;
    [Range(0.0f, 1.0f)][SerializeField] private float _stability;


    private Animator _animator;
    // 이동거리 = 속력 * 시간
    // 고정 프레임당 이동거리 = 속력 * 고정프레임간 시간 변화


    public void Finish(int grade)
    {
        isMovable = false;
        switch (grade)
        {
            case 0:
                _animator.Play("Jumping");
                break;
            case 1:
            case 2:
                _animator.Play("Salute");
                break;
            default:
                _animator.Play("KneelDown");
                break;
        }

        PlayManager.instance.RegisterRunnerFinish(this);

    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        PlayManager.instance.RegisterRunner(this);
    }
    private void FixedUpdate()
    {

        if(!_isMovable)
        {
            return ;
        }
        // transform.position += Vector3.forward * _speed * Time.fixedDeltaTime; 아래와 같은 내용
        if(_modifyingTimer <= 0)
        {
            speedModified = _speed * Random.Range(_stability, 1.0f);
            _modifyingTimer = _speedModifyingPeriod;
        }
        else
        {
            _modifyingTimer -= Time.fixedDeltaTime;
        }
        transform.Translate(Vector3.forward * _speedModified * Time.fixedDeltaTime);
    }
}
