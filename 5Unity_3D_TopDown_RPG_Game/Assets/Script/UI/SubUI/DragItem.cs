using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * IPointerEnterHandler : 마우스 포인터가 UI요소 내부에 들어갔을 때 호출된다.
 * IPointerExitHandler : 마우스 포인터가 UI요소 내부에서 나왔을 때 호출
 * IBeginDragHandler : 드래그를 시작할 때 호출된다.
 * IDragHandler : 드래그시 호출된다.
 * IEndDragHandler : 드래그를 끝냈을 때 호출된다.
 * IDropHandler : 드래그를 끝내고 마우스 왼쪽 버튼을 떼면 호출
 */


/// <summary>
/// UI 아이뎀이 컨테이너로부터 드래그 앤 드롭될 수 있도록 한다.
/// </summary>
public class DragItem<T> : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    where T : class
{
    Vector3 startPosition; // 드래그의 시작 위치
    Transform originalParent; // 원래 부모 계층
    IDragSource<T> source; // 드래그 할 아이테 정보

    Canvas parentCanvas; // 부모 캔버스
    private void Awake()
    {
        parentCanvas = GetComponentInParent<Canvas>();
        source = GetComponentInParent<IDragSource<T>>();
    }

    /// <summary>
    /// 드래그 시작시 호출
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        startPosition = transform.position;
        originalParent = transform.parent;

        GetComponent<CanvasGroup>().blocksRaycasts = false; // 레이캐스트 차단 해제
        transform.SetParent(parentCanvas.transform, true); // 독립적인 방향을 갖는다(부모의 포지션에 영향을 받지 않는다)
    }

    public void OnDrag(PointerEventData eventData)
    {
        // eventData.position : 마우스의 위치
        transform.position = eventData.position;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // item 초기화
        transform.position = startPosition; // 위치를 시작 위치로 복원함
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.SetParent(originalParent, true); // 

        IDragDestination<T> container;
        
        // 아이템을 버림
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            container = parentCanvas.GetComponent<IDragDestination<T>>();
        }
        else
        {
            container = GetContainer(eventData);
        }

        if(container != null)
        {
            //아이템을 넘겨주는 코드
            DropItemIntoContainer(container);
        }
    }

    /// <summary>
    /// 컨테이너를 가져오는 메소드
    /// </summary>
    /// <param name="eventData"></param>
    /// <returns></returns>
    private IDragDestination<T> GetContainer(PointerEventData eventData)
    {
        if (eventData.pointerEnter)
        {
            var container = eventData.pointerEnter.GetComponentInParent<IDragDestination<T>>();
            return container;
        }
        return null;
    }

    /// <summary>
    /// 아이템을 콘테이너에 넣어주는 코드
    /// </summary>
    /// <param name="container"></param>
    private void DropItemIntoContainer(IDragDestination<T> destination)
    {
        Debug.Log("DropItemIntoContainer");
        // referenceEquals의 참조 범위가 어디인지 알아보기
        if (object.ReferenceEquals(destination, source)) return;
        
        var destinationContainer = destination as IDragContainer<T>;
        var sourceContainer = source as IDragContainer<T>;

        // 아이템을 교환할 수 없는 경우 : 둘중의 하나가 null이거나 목적지가 비어있는 경우
        if (destinationContainer == null || sourceContainer == null || destinationContainer.GetItem() == null)
        {
            AttemptSimpleTransfer(destination);
            return;
        }

    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="destination"></param>
    private void AttemptSimpleTransfer(IDragDestination<T> destination)
    {
        var draggingItem = source.GetItem();
        var draggingNumber = source.GetNumber();
        Debug.Log($"AttemptSimpleTransfer{draggingItem}{draggingNumber}");

        // 최대 아이템 한계
        // 한계와 현재 아이템 개수의 차이 구하기

        /*
        var acceptable = destination.MaxAcceptable(draggingItem);

        // 최대보다 작은지 확인
        var toTransfer = Mathf.Min(draggingNumber, acceptable);

        // 아이템 숫자 비교해서 넣기
        if(toTransfer > 0)
        {
            source.RemoveItems(toTransfer);
            destination.AddItems(draggingItem, toTransfer);
        }*/

        destination.AddItems(draggingItem, draggingNumber);
    }

    
}