using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class GameObjectExtention
{
    /// <summary>
    /// ������ ���Ʒ��� ������Ʈ�� Ȯ���ϰ� �̸� �����Ŵ ������ �߰�����
    /// �����ϴ� ������� �� �ٸ��� ������ �� ���� �ֱ� ����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    // T�� ������ ��������� AddComponent�� ����� �� �ִ�.
    public static T GetcomponentAroundOrAdd<T>(this GameObject obj) where T : Component
    {
        // ���� ������Ʈ�� �ڽ� ������ �ִ� ������Ʈ�� �����´�.
        T component = obj.GetComponentInChildren<T>(true);
        if(component != null)
            return component;

        //���� ������Ʈ�� �θ������ �ִ� ������Ʈ�� �����´�.
        component = obj.GetComponentInParent<T>();
        if (component != null) return component;
        
        //���� ������Ʈ�� ������Ʈ�� �����´�.
        component = obj.GetComponent<T>();
        if (component != null) return component;

        // ������ �߰��ؼ� ��ȯ�Ѵ�.
        component = obj.AddComponent<T>();
        return component;
    }
}
