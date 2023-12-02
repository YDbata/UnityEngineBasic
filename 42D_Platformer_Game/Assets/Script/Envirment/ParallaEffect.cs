using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaEffect : MonoBehaviour
{
    // 카메라 참조를 위한 변수(Unity tool에서 지정가능)
    public Camera cam;
    // 팔로우할 대상을 참조하기 위한 변수(Unity에서 지정)
    public Transform followTarget;
    // Parallax Object의 시작 위치 저장 Vector
    Vector2 startingPosition;
    // Parallax의 시작Z값
    float startingZ;

    // 카메라의 이동량을 계산하여 변환하는 속성
    Vector2 camMoveSinceStart;

    // target과 Parallax Object같의 Z거리를 반환
    float zDistanceFromTarget;

    // Start 함수는 첫번째 프레임 이전에 호출되어 실행한다.
    private void Start()
    {
        // 현재 Game Object의 포지션과 Z값 저장
        startingPosition = transform.position;

        //startingZ = transform.localPosition.z;
        startingZ = transform.position.z - followTarget.position.z;
    }

    void Update()
    {
        // 카메라와 현재 나의 시작위치 차이
        camMoveSinceStart = (Vector2)cam.transform.position - startingPosition;

        //따라 다닐 캐릭터(플레이어)와 현 오브젝트의 z축 거리값
        zDistanceFromTarget = transform.position.z - followTarget.transform.position.z;

        // Player한테서 먼 Object와 가까운 Object를 구분 (cam.nearClipPlane : 0.1f)
        float clippingPlane = ((cam.transform.position.z) + zDistanceFromTarget) > 0 ?
            cam.farClipPlane : cam.nearClipPlane;
        //Debug.Log(clippingPlane);
        // 패럴랙스 계수를 반환 ex) BG1의 경우 Abs(-1.2)/0.1 == 1.2/0.1 = 12
        float parallaFactore = Mathf.Abs(zDistanceFromTarget) / clippingPlane;
        // 새로운 Position 생성 ex) BG1 현재 카메라의 위치보다 1/12의 위치만큼 이동하는 Vector2 생성
        Vector2 newPosition = startingPosition + camMoveSinceStart / parallaFactore;

        // transform에 새로운 Vector를 적용시켜 이동
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
