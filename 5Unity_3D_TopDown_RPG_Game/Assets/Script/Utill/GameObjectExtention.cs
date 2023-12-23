using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class GameObjectExtention
{
    /// <summary>
    /// 계층의 위아래에 컴포너트를 확인하고 이를 실행시킴 없으면 추가해줌
    /// 개발하는 사람들이 다 다르게 개발을 할 수도 있기 때문
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    // T의 형식을 지정해줘야 AddComponent를 사용할 수 있다.
    public static T GetcomponentAroundOrAdd<T>(this GameObject obj) where T : Component
    {
        // 현재 오브젝트의 자식 계층에 있는 컴포넌트를 가져온다.
        T component = obj.GetComponentInChildren<T>(true);
        if(component != null)
            return component;

        //현재 오브젝트의 부모계층에 있는 컴포넌트를 가져온다.
        component = obj.GetComponentInParent<T>();
        if (component != null) return component;
        
        //현재 오브젝트에 컴포넌트를 가져온다.
        component = obj.GetComponent<T>();
        if (component != null) return component;

        // 없으면 추가해서 반환한다.
        component = obj.AddComponent<T>();
        return component;
    }
}
