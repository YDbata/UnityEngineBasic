using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TPSCameraController : MonoBehaviour
{
	[SerializeField] Transform followTarget;
	
	[SerializeField][Range(-90, 0)] private float minVerticalAngle = -45.0f;
	[SerializeField][Range(0,90)] private float maxVerticalAngle = 45.0f;


	[SerializeField] private float rotationSpeed = 10;
	//기본 카메라와 타겟 사이의 거리
	[SerializeField] private float targetDistance = 3.0f;
	//줌인 / 줌아웃 속도
	[SerializeField] private float zoomSpeed = 10.0f;
	// 최소 거리
	[SerializeField] private float minDistance = 1;
	// 최대 거리
	[SerializeField] private float maxDistance = 5;

	float rotationY = 0;
	float rotationX = 0;
	float scroll = 5;
	Vector2 targetFramingOffset;



    public Vector2 mouseSensitivity = Vector2.one * 100f;
    public Transform player;

    Vector2 startPos;
    private float startRot;
	
	
	public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0);

    // Update is called once per frame
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;
                    startRot = transform.localEulerAngles.y;
                    break;
                case TouchPhase.Ended:
                    var delta = Vector2.Scale(touch.position - startPos, mouseSensitivity);
                    var newRot = Mathf.Clamp(startRot - delta.x, -90, 90);
                    transform.localEulerAngles = new Vector3(0, newRot, 0);


                    break;
            }
        }
        transform.position = player.position - transform.forward * 10 + Vector3.up * 3;
    }

    /*	void Update()
        {
            //마우스 포인트가 좌우로 움직이는 값을 가져온다.
            rotationY += Input.GetAxis("Mouse X");
            rotationY = rotationY % 360;
            //마우스 포인트가 상하로 움직이는 값을 가져온다.
            rotationX += Input.GetAxis("Mouse Y");
            //마우스 스크롤 값을 가져온다.
            scroll = Input.GetAxis("Mouse ScrollWheel");

            // 마우스 스크롤 값에 따라서 카메라와 타겟의 거리를 갱신한다.
            targetDistance -= scroll * zoomSpeed * Time.deltaTime;
            targetDistance = Mathf.Clamp(targetDistance, minDistance,maxDistance);

            // 최소,최대 각도로 제한한다.
            rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

            //오일러 연산을 통해서 마우스 움직임에 따라 회전값을 구한다.
            Quaternion targetRotation = Quaternion.Euler(rotationX, rotationY, 0);

            // 타겟 위치에서 FraimingOffset 값을 더한다.
            Vector3 focusPosition = followTarget.position +
                new Vector3(targetFramingOffset.x, targetFramingOffset.y, 0);

            // taget포지션을 기준으로 오일러 연산을 통해 구한 회전에다가
            // z축으로 targetDistance만큼 뒤로 움직인다.
            transform.position = Vector3.Slerp(transform.position, 
                (focusPosition - (targetRotation * new Vector3(0, 0, targetDistance))),
                rotationSpeed * Time.deltaTime);

            // 타켓 회전방향으로 회전값을 설정한다.
            transform.rotation = targetRotation;
            //Debug.Log("Euler : " + Quaternion.Get());
        }*/
}
