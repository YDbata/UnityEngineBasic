using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
	[SerializeField] private RectTransform baseRect = null;
	[SerializeField] private RectTransform background = null;
	[SerializeField] private RectTransform handle;

	[SerializeField] private Canvas canvas;

	[SerializeField][Range(1, 300)] private float range;

	private static Vector2 inputDir;
	public static Vector3 InputDir 
	{
		get
		{
			return new Vector3(inputDir.x, 0,inputDir.y);
		}
	}
	private void Start()
	{
		baseRect = GetComponent<RectTransform>();
	}

	public void OnPointerDown(PointerEventData eventData)
	{

	}

	[SerializeField] private int handleRange = 1;
	public void OnDrag(PointerEventData eventData)
	{
		handle.anchoredPosition = eventData.position;

		Vector2 position = RectTransformUtility.WorldToScreenPoint(Camera.main, background.position);
		Vector2 input = (eventData.position - position);
		Vector2 inputPos = input.magnitude < range ? input : input.normalized * range;

		handle.anchoredPosition = inputPos;
		inputDir = inputPos / range;
	}


	public void OnPointerUp(PointerEventData eventData)
	{
		handle.anchoredPosition = Vector2.zero;
		inputDir = Vector2.zero;
	}
}
