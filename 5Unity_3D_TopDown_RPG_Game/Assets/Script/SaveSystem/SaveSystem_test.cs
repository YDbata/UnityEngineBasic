using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class SaveSystem_test : MonoBehaviour
{
    
/*    [SerializeField] private string DataToSave = "SBS ���� ��ī���� �ָ��� ����Ƽ ����";
    [SerializeField] private string Filename = "�����ڷ�";

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            Save();
        }
        if (Input.GetKeyUp(KeyCode.L)) { Load(); }
    }
    public void Save()
    {
        string ToJsonData = JsonUtility.ToJson(DataToSave, true);
        string filePath = Application.persistentDataPath + "/" + Filename;
        Debug.Log(ToJsonData);
        Debug.Log(filePath);
        // �̹� ����� ������ �ִٸ� ���� �ƴϸ� ���� ���� �� ����
        File.WriteAllText(filePath, DataToSave);

        Debug.Log("���� �Ϸ�");

    }

    public void Load()
    {
        string filePath = Application.persistentDataPath + "/" + Filename;
        // ����� �����Ͱ� �ִٸ�
        if (File.Exists(filePath))
        {
            // ����� ������ �а� Json�� Ŭ���� �������� ��ȯ
            string FromJsonData = File.ReadAllText(filePath);
            //string data = JsonUtility.FromJson<string>(FromJsonData);
            Debug.Log("Load Data : " + FromJsonData);

        }
    }

    public void Delete()
    {
        string filePath = Application.persistentDataPath + "/" + Filename;
        File.Delete(filePath);
    }
    */
}
