using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    //���� ���۵ɶ� �����͸� �ʱ�ȭ �Ѵ�.
    //������ ������� ����, ���ݷ� ����� �������� �����ͼ� �����Ѵ�.
    private void Awake()
    {
        Debug.Log("�÷��̾� ������ ���� �Ϸ�");
    }

    // ���� ������Ʈ�� Ȱ��ȭ �ɶ� ȣ��Ǵ� �Լ�
    private void OnEnable()
    {
        Debug.Log("������Ʈ �ʱ�ȭ");
    }

    //���� �����Ѵ�. => ���͸� �����Ѵ�.
    //������� �ҷ��´�.
    void Start()
    {
        Debug.Log("���� ����");
    }

    //���� �ֱ⸶�� ĳ���� �������� ���������� �����Ѵ�.
    //CPU �� ������ �����ʽ��ϴ�.
    private void FixedUpdate()
    {
        Debug.Log("ĳ���� ������");
    }

    // ������ �������� ������ �����Ѵ�.
    //CPU �� ������ �޽��ϴ�.
    void Update()
    {
        Debug.Log("���� ����ϱ�");
    }

    //������Ʈ �޼ҵ带 �����ϰ� ���� ������ ����Ʈ�� �̷� ������ �ҷ��ɴϴ�.
    //CPU �� ������ �޽��ϴ�.
    private void LateUpdate()
    {
        Debug.Log("ȿ�� �߻� : ����Ʈ");
    }

    // ���� ������Ʈ�� ��Ȱ��ȭ �ɶ� ȣ��Ǵ� �Լ�
    private void OnDisable()
    {
        Debug.Log("������Ʈ �ʱ�ȭ ����");
    }

    // ���Ͱ� ������� ȣ��Ǵ� �Լ�
    // ���̾��Ű���� ���� ������Ʈ�� �����ɶ� ȣ��ȴ�.
    private void OnDestroy()
    {
        Debug.Log("���Ͱ� �������.");
    }
}
