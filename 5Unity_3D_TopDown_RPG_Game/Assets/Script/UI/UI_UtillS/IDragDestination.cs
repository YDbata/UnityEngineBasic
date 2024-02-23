using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Drag �� �� �������� ������ �Ѱ��ִ� �޼ҵ� ����
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IDragDestination<T> where T : class
{
    /// <summary>
    /// �־��� �������� ������ �󸶳� �޾Ƶ��� �� �ִ� �� üũ�ϴ� �޼ҵ�
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    int MaxAcceptable(T item);

    /// <summary>
    /// �������� �Ѱ� ���� �� UI �� �����͸� ������Ʈ�ϴ� �޼ҵ�
    /// </summary>
    /// <param name="item"></param>
    /// <param name="number"></param>
    void AddItems(T item, int number);
    
}
