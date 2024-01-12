using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class SaveSystem_test : MonoBehaviour
{
    
/*    [SerializeField] private string DataToSave = "SBS 게임 아카데미 주말반 유니티 수업";
    [SerializeField] private string Filename = "수업자료";

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
        // 이미 저장된 파일이 있다면 덮고 아니면 새로 생성 후 저장
        File.WriteAllText(filePath, DataToSave);

        Debug.Log("저장 완료");

    }

    public void Load()
    {
        string filePath = Application.persistentDataPath + "/" + Filename;
        // 저장된 데이터가 있다면
        if (File.Exists(filePath))
        {
            // 저장된 파일을 읽고 Json을 클래스 형식으로 전환
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
