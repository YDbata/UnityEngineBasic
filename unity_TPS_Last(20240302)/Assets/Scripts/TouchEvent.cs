using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEvent : MonoBehaviour
{
    public PlayerController Controller;
    public float speed = 6.0f;
    private Vector3 moveDirection = Vector3.zero;
    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 touchPosition = touch.position;
            Debug.Log(touchPosition);
            touchPosition.x = (touchPosition.x - Screen.width / 2) / (Screen.width / 2);
            touchPosition.y = (touchPosition.x - Screen.height / 2) / (Screen.height / 2);

            moveDirection = new Vector3(touchPosition.x, 0, touchPosition.y);
            

        }
    }
}
