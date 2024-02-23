using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��(value)�� �����ϰ�, ù ��� ������ �ʱ�ȭ�� �����ϴ� Ŭ�����Դϴ�.
/// </summary>
public class LazyValue<T>
{
    private T _value; // �� ����� �ʵ�
    private bool _initialized = false; // �ʱ�ȭ ���θ� ��Ÿ���� ����
    private InitializerDelegate _initializer; // �ʱ�ȭ delegate

    public delegate T InitializerDelegate();

    public LazyValue(InitializerDelegate initializer)
    {
        _initializer = initializer;
    }

    /// <summary>
    /// ���� ��ȯ�ϰų� �����մϴ�.
    /// �ʱ�ȭ ���� ���� ��ȯ�ϴ� ���� �ʱ�ȭ �մϴ�.
    /// </summary>
    public T value { 
        get { 
            // ���� ��ȯ�ϱ� ���� �ʱ�ȭ�� �մϴ�.
            ForceInit();
            return _value; 
        } 
        set {
            // ���� �����Ǿ����Ƿ� �ʱ�ȭ�� ������� �ʽ��ϴ�.
            _initialized = true;
            _value = value; 
        }
    }

    /// <summary>
    /// ��������Ʈ�� ���ؼ� ���� �ʱ�ȭ �մϴ�.
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
