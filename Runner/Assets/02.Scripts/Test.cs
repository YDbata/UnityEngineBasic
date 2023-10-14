using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehaviour .. 
// Component �� �⺻ ���� , �����ڸ� ���� ȣ���ؼ� �����ϴ°� �ƴϰ�
// �ش� Script Instance �� �ε�� �� ��ü�� ������ ��. 
// -> ���� �츮�� �����ڸ� ȣ���ϸ� �ȵ�. 
public class Test : MonoBehaviour
{
    // ���� �ε�ǰ��� �� Ŭ������ ������Ʈ�� ������ ���ӿ�����Ʈ�� �ε�ɶ� �� Ŭ������ ���� ��ũ��Ʈ �ν��Ͻ��� �ε��.
    // (�� Ŭ������ ������Ʈ�� ������ ���ӿ�����Ʈ�� ��Ȱ��ȭ�� ä�� ���� �ε�Ǿ��ٸ�, ��ũ��Ʈ�ν��Ͻ��� �ε��������. 
    // Ȱ��ȭ�Ǵ� ���� ��ũ��Ʈ �ν��Ͻ��� �ε��� )
    // ��ũ��Ʈ �ν��Ͻ��� �ε�� �� �ѹ� ȣ��
    // �����ڿ��� ���� �����ϴ� ��� �ʱ�ȭ ����� ������ Awake()���ٰ� ���ָ� �ȴ�..
    private void Awake()
    {
        Debug.Log("Awake");
    }

    // �� Component �� Ȱ��ȭ �� ������ ȣ�� 
    // (�� Ŭ�����ν��Ͻ��� Component �� ������ GameObject �� Ȱ��ȭ �� ������ ���������� ȣ��)
    private void OnEnable()
    {
        Debug.Log("Enabled");
    }

    // Editor ������ ȣ��, �ش� ��ũ��Ʈ�ν��Ͻ��� GameObject �� Add �� �� �� �����ڰ� Editor ���� ���� ȣ���� �� ȣ��
    // ��� ��� ������ �ʱⰪ���� �ǵ���
    private void Reset()
    {
        Debug.Log("Reset");
    }

    // ���� ���� ���� ������ �� �ѹ� ȣ��
    private void Start()
    {
        Debug.Log("Start");
    }


    // ���������� ���� ������ (����������, Fixed frame) ���� ȣ��
    private void FixedUpdate()
    {
        //Debug.Log("Fixed Update");
    }

    // trigger �ɼ��� ���� Collider �� ���� ��ħ �̺�Ʈ
    private void OnTriggerEnter(Collider other)
    {

    }

    // trigger �ɼ��� ���� Collider �� ���� �浹 �̺�Ʈ
    private void OnCollisionStay(Collision collision)
    {

    }

    // OnMouseXXX : ���콺�� �� �ν��Ͻ��� ������Ʈ�ΰ����� ���ӿ�����Ʈ ���� �ö�ͼ� Ư�� Action �� ���Ҷ� ȣ��
    private void OnMouseOver()
    {
        Debug.Log("On Mouse over");
    }

    // �������Ӹ��� ȣ�� (��� ���ɿ� ���� ȣ�� �ֱⰡ �޶���)
    private void Update()
    {
        //Debug.Log("Update");
    }

    // �� �����Ӹ��� ȣ���� �ؾ�������, Animation ������ ������ ��ġ�� �ȵǴ� �����̳�, �켱������ �ڷ� �з��� �Ǵ� ������� ����
    private void LateUpdate()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(Vector3.zero, 2.0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(Vector3.zero, 2.1f);
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Application quit");
    }

    private void OnDisable()
    {
        Debug.Log("Disabled");
    }

    private void OnDestroy()
    {
        Debug.Log("Destroyed");
    }
}