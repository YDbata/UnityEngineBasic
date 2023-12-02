using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaEffect : MonoBehaviour
{
    // ī�޶� ������ ���� ����(Unity tool���� ��������)
    public Camera cam;
    // �ȷο��� ����� �����ϱ� ���� ����(Unity���� ����)
    public Transform followTarget;
    // Parallax Object�� ���� ��ġ ���� Vector
    Vector2 startingPosition;
    // Parallax�� ����Z��
    float startingZ;

    // ī�޶��� �̵����� ����Ͽ� ��ȯ�ϴ� �Ӽ�
    Vector2 camMoveSinceStart;

    // target�� Parallax Object���� Z�Ÿ��� ��ȯ
    float zDistanceFromTarget;

    // Start �Լ��� ù��° ������ ������ ȣ��Ǿ� �����Ѵ�.
    private void Start()
    {
        // ���� Game Object�� �����ǰ� Z�� ����
        startingPosition = transform.position;

        //startingZ = transform.localPosition.z;
        startingZ = transform.position.z - followTarget.position.z;
    }

    void Update()
    {
        // ī�޶�� ���� ���� ������ġ ����
        camMoveSinceStart = (Vector2)cam.transform.position - startingPosition;

        //���� �ٴ� ĳ����(�÷��̾�)�� �� ������Ʈ�� z�� �Ÿ���
        zDistanceFromTarget = transform.position.z - followTarget.transform.position.z;

        // Player���׼� �� Object�� ����� Object�� ���� (cam.nearClipPlane : 0.1f)
        float clippingPlane = ((cam.transform.position.z) + zDistanceFromTarget) > 0 ?
            cam.farClipPlane : cam.nearClipPlane;
        //Debug.Log(clippingPlane);
        // �з����� ����� ��ȯ ex) BG1�� ��� Abs(-1.2)/0.1 == 1.2/0.1 = 12
        float parallaFactore = Mathf.Abs(zDistanceFromTarget) / clippingPlane;
        // ���ο� Position ���� ex) BG1 ���� ī�޶��� ��ġ���� 1/12�� ��ġ��ŭ �̵��ϴ� Vector2 ����
        Vector2 newPosition = startingPosition + camMoveSinceStart / parallaFactore;

        // transform�� ���ο� Vector�� ������� �̵�
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
