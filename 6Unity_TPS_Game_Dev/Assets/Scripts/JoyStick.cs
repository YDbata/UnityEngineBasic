using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform baseRect = null;
    [SerializeField] private RectTransform background = null;
    [SerializeField] private RectTransform handle;

    [SerializeField] private Canvas canvas;

    [SerializeField] private int handleRange = 1;

    private void Start()
    {
        baseRect = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        handle.anchoredPosition = eventData.position;

        Vector2 position = RectTransformUtility.WorldToScreenPoint(Camera.main, background.position);
        Vector2 radiuse = background.sizeDelta / 2;
        Vector2 input = (eventData.position - position)/ (radiuse*canvas.scaleFactor);
        handle.anchoredPosition = input * radiuse * handleRange;

        Debug.Log("eventData " + handle.anchoredPosition);
        Debug.Log("OnDrag "+ eventData.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        

        Debug.Log("OnPointerDown");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp");
    }
}
