using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SaveSystem))]
public class SavingWrapper : MonoBehaviour
{
    private const string currentSavekey = "currentSaveName";
    [SerializeField] private float fadeInTime = 0.2f;
    [SerializeField] private float fadeOutTime = 0.2f;

    public void ContinueGame()
    {
        StartCoroutine(LoadLastScene());
    }

    // 마지막으로 저장된 데이터 기반 게임 시작
    private IEnumerator LoadLastScene()
    {
        Fader fader = FindObjectOfType<Fader>();
        yield return fader.FadeOut(fadeOutTime);
        yield return GetComponent<SaveSystem>().LoadLastScene(currentSavekey);
        yield return fader.FadeIn(fadeInTime);
    }

    // Start is called before the first frame update
    private void Start()
    {
        ContinueGame();
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }


    public void Save()
    {
        GetComponent<SaveSystem>().Save(currentSavekey);
    }

    public void Load()
    {
        GetComponent<SaveSystem>().Load(currentSavekey);
    }
}
