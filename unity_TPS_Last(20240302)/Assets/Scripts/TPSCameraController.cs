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
	//�⺻ ī�޶�� Ÿ�� ������ �Ÿ�
	[SerializeField] private float targetDistance = 3.0f;
	//���� / �ܾƿ� �ӵ�
	[SerializeField] private float zoomSpeed = 10.0f;
	// �ּ� �Ÿ�
	[SerializeField] private float minDistance = 1;
	// �ִ� �Ÿ�
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
            //���콺 ����Ʈ�� �¿�� �����̴� ���� �����´�.
            rotationY += Input.GetAxis("Mouse X");
            rotationY = rotationY % 360;
            //���콺 ����Ʈ�� ���Ϸ� �����̴� ���� �����´�.
            rotationX += Input.GetAxis("Mouse Y");
            //���콺 ��ũ�� ���� �����´�.
            scroll = Input.GetAxis("Mouse ScrollWheel");

            // ���콺 ��ũ�� ���� ���� ī�޶�� Ÿ���� �Ÿ��� �����Ѵ�.
            targetDistance -= scroll * zoomSpeed * Time.deltaTime;
            targetDistance = Mathf.Clamp(targetDistance, minDistance,maxDistance);

            // �ּ�,�ִ� ������ �����Ѵ�.
            rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

            //���Ϸ� ������ ���ؼ� ���콺 �����ӿ� ���� ȸ������ ���Ѵ�.
            Quaternion targetRotation = Quaternion.Euler(rotationX, rotationY, 0);

            // Ÿ�� ��ġ���� FraimingOffset ���� ���Ѵ�.
            Vector3 focusPosition = followTarget.position +
                new Vector3(targetFramingOffset.x, targetFramingOffset.y, 0);

            // taget�������� �������� ���Ϸ� ������ ���� ���� ȸ�����ٰ�
            // z������ targetDistance��ŭ �ڷ� �����δ�.
            transform.position = Vector3.Slerp(transform.position, 
                (focusPosition - (targetRotation * new Vector3(0, 0, targetDistance))),
                rotationSpeed * Time.deltaTime);

            // Ÿ�� ȸ���������� ȸ������ �����Ѵ�.
            transform.rotation = targetRotation;
            //Debug.Log("Euler : " + Quaternion.Get());
        }*/
}
