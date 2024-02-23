using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Drag 한 후 아이템의 정보를 넘겨주는 메소드 선언
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IDragDestination<T> where T : class
{
    /// <summary>
    /// 주어진 아이템의 개수를 얼마나 받아들일 수 있는 지 체크하는 메소드
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    int MaxAcceptable(T item);

    /// <summary>
    /// 아이템을 넘겨 줬을 때 UI 및 데이터를 업데이트하는 메소드
    /// </summary>
    /// <param name="item"></param>
    /// <param name="number"></param>
    void AddItems(T item, int number);
    
}
