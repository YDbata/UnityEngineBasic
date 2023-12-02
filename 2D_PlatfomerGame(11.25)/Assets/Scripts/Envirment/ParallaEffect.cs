using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;//�÷��̾� Transform

    Vector2 startingPosition;//���� Transform�� Position;

    //-1.2, -0.6, -0.24 �� ���ϰ� �÷��̾��� �������� Z���� ����
    float startingZ;

    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z - followTarget.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //ī�޶�� ���� ���� ���� ��ġ�� ���� Vector2
        Vector2 camMoveSinceStart = (Vector2)cam.transform.position - startingPosition;
       
        //���� �ٴ� ĳ����(�÷��̾�)�� �� ������Ʈ�� z�� �Ÿ���
        float zDistasnceFromTarget = transform.position.z - followTarget.transform.position.z;

        //���� ������ 
        // int a = (����) ? (��) : (����) 
        float clippingPlane = ((cam.transform.position.z) + zDistasnceFromTarget) > 0 ? 
            cam.farClipPlane : cam.nearClipPlane;

        //1.2 / 0.3 = 4
        float parallaFactore = Mathf.Abs(zDistasnceFromTarget) / clippingPlane;
        // 0 + 2 / 4 
        Vector2 newPosition = startingPosition + (camMoveSinceStart / parallaFactore);

        transform.position = new Vector3(newPosition.x,newPosition.y,startingZ);
    }
}
