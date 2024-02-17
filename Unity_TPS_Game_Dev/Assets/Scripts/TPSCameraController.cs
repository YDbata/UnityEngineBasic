using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TPSCameraController : MonoBehaviour
{
	[SerializeField] Transform followTarget;

	[SerializeField][Range(-15, 0)] private float minVerticalAngle = -15f;
	[SerializeField][Range(0, 90)] private float maxVerticalAngle = 45.0f;

    [SerializeField][Range(-90, 0)] private float minHorizontalAngle = -15f;
    [SerializeField][Range(0, 90)] private float maxHorizontalAngle = 45.0f;

    [SerializeField] Vector2 framingOffset;

	[SerializeField] private float targetdistance = 3.0f;
	[SerializeField] private float zoomSpeed = 10.0f;
	[SerializeField] private float rotationSpeed = 10f;
	[SerializeField] private float minDistance = 1;
	[SerializeField] private float maxDistance = 5;

	float rotationY = 0;
	float rotationX = 0;
	float scroll = 0;
	public Quaternion PlanarRotationY => Quaternion.Euler(0, rotationY, 0);

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
		// 마우스포인트가 좌우로 움직이는 것을 가져온다.
		rotationY += Input.GetAxis("Mouse X");
		rotationY = rotationY % 360;
		// 상하로 움직이는 것
		rotationX += Input.GetAxis("Mouse Y");
		// 스크롤 값
		scroll = Input.GetAxis("Mouse ScrollWheel");
		// 스크롤 값에 따라서 카메라와 타겟의 거리를 갱신(줌인, 줌아웃)
		targetdistance -= scroll * zoomSpeed * Time.deltaTime;
		targetdistance = Mathf.Clamp(targetdistance, minDistance, maxDistance);

		// 좌우의 최대 최소 각도를 제한한다.
		// rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);
		// rotationY = Mathf.Clamp(rotationY, minHorizontalAngle,maxHorizontalAngle );
		float rotationYAmount = Mathf.Abs(rotationY);
;
        Vector2 targetframingOffset;
        if (rotationYAmount < 90 || rotationYAmount > 270)
		{
			targetframingOffset = framingOffset;
		}
		else
		{
			targetframingOffset = framingOffset;
			targetframingOffset.x *= -1;
		}
		Debug.Log("Y" + rotationY);
		// 오일러 연산을 통해서 마우스 움직임에 따라 회전값을 구한다.
		Quaternion targetRotation = Quaternion.Euler(rotationX, rotationY, 0);
		// 타겟 위치의 fraimingOffset 값을 더한다.
		Vector3 focusPosition = followTarget.position + new Vector3(targetframingOffset.x, targetframingOffset.y, 0);
        //Vector3.MoveTowards(transform.position, followTarget.position + new Vector3(targetframingOffset.x, targetframingOffset.y), rotationSpeed*Time.deltaTime);
			//followTarget.position + 
			//new Vector3(targetframingOffset.x, targetframingOffset.y, 0);

		// 오일러 연산을 통해 구한 회전값에 줌을 적용한다.
		transform.position = 
			Vector3.Slerp(transform.position, (focusPosition - (targetRotation * new Vector3(0, 0, targetdistance))), rotationSpeed * Time.deltaTime);
			//focusPosition - 
			//(targetRotation * new Vector3(0, 0, targetdistance));
		// 오브젝트 회전값 업데이트
		transform.rotation = targetRotation;
		//Debug.Log("Euler : " + Quaternion.Get());
    }

}
