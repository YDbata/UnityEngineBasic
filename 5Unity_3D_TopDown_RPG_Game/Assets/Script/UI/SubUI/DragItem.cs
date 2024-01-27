using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * IPointerEnterHandler : ���콺 �����Ͱ� UI��� ���ο� ���� �� ȣ��ȴ�.
 * IPointerExitHandler : ���콺 �����Ͱ� UI��� ���ο��� ������ �� ȣ��
 * IBeginDragHandler : �巡�׸� ������ �� ȣ��ȴ�.
 * IDragHandler : �巡�׽� ȣ��ȴ�.
 * IEndDragHandler : �巡�׸� ������ �� ȣ��ȴ�.
 * IDropHandler : �巡�׸� ������ ���콺 ���� ��ư�� ���� ȣ��
 */


/// <summary>
/// UI ���̵��� �����̳ʷκ��� �巡�� �� ��ӵ� �� �ֵ��� �Ѵ�.
/// </summary>
public class DragItem<T> : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    where T : class
{
    Vector3 startPosition; // �巡���� ���� ��ġ
    Transform originalParent; // ���� �θ� ����
    IDragSource<T> source; // �巡�� �� ������ ����

    Canvas parentCanvas; // �θ� ĵ����
    private void Awake()
    {
        parentCanvas = GetComponentInParent<Canvas>();
        source = GetComponentInParent<IDragSource<T>>();
    }

    /// <summary>
    /// �巡�� ���۽� ȣ��
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        startPosition = transform.position;
        originalParent = transform.parent;

        GetComponent<CanvasGroup>().blocksRaycasts = false; // ����ĳ��Ʈ ���� ����
        transform.SetParent(parentCanvas.transform, true); // �������� ������ ���´�(�θ��� �����ǿ� ������ ���� �ʴ´�)
    }

    public void OnDrag(PointerEventData eventData)
    {
        // eventData.position : ���콺�� ��ġ
        transform.position = eventData.position;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // item �ʱ�ȭ
        transform.position = startPosition; // ��ġ�� ���� ��ġ�� ������
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.SetParent(originalParent, true); // 

        IDragDestination<T> container;
        
        // �������� ����
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
            //�������� �Ѱ��ִ� �ڵ�
            DropItemIntoContainer(container);
        }
    }

    /// <summary>
    /// �����̳ʸ� �������� �޼ҵ�
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
    /// �������� �����̳ʿ� �־��ִ� �ڵ�
    /// </summary>
    /// <param name="container"></param>
    private void DropItemIntoContainer(IDragDestination<T> destination)
    {
        Debug.Log("DropItemIntoContainer");
        // referenceEquals�� ���� ������ ������� �˾ƺ���
        if (object.ReferenceEquals(destination, source)) return;
        
        var destinationContainer = destination as IDragContainer<T>;
        var sourceContainer = source as IDragContainer<T>;

        // �������� ��ȯ�� �� ���� ��� : ������ �ϳ��� null�̰ų� �������� ����ִ� ���
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

        // �ִ� ������ �Ѱ�
        // �Ѱ�� ���� ������ ������ ���� ���ϱ�

        /*
        var acceptable = destination.MaxAcceptable(draggingItem);

        // �ִ뺸�� ������ Ȯ��
        var toTransfer = Mathf.Min(draggingNumber, acceptable);

        // ������ ���� ���ؼ� �ֱ�
        if(toTransfer > 0)
        {
            source.RemoveItems(toTransfer);
            destination.AddItems(draggingItem, toTransfer);
        }*/

        destination.AddItems(draggingItem, draggingNumber);
    }

    
}