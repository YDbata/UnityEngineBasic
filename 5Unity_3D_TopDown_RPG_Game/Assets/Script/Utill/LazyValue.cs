using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 값(value)을 저장하고, 첫 사용 직전에 초기화를 실행하는 클래스입니다.
/// </summary>
public class LazyValue<T>
{
    private T _value; // 값 저장용 필드
    private bool _initialized = false; // 초기화 여부를 나타내는 변수
    private InitializerDelegate _initializer; // 초기화 delegate

    public delegate T InitializerDelegate();

    public LazyValue(InitializerDelegate initializer)
    {
        _initializer = initializer;
    }

    /// <summary>
    /// 값을 반환하거나 설정합니다.
    /// 초기화 전에 값을 반환하는 경우는 초기화 합니다.
    /// </summary>
    public T value { 
        get { 
            // 값을 반환하기 전에 초기화를 합니다.
            ForceInit();
            return _value; 
        } 
        set {
            // 값이 설정되었으므로 초기화를 사용하지 않습니다.
            _initialized = true;
            _value = value; 
        }
    }

    /// <summary>
    /// 델리게이트를 통해서 값을 초기화 합니다.
    /// </summary>
    public void ForceInit()
    {
        if(!_initialized )
        {
            _value = _initializer();
            _initialized = true;
        }
    }
}
