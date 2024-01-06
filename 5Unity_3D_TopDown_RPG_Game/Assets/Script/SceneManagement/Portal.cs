using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    enum DestinationIdentifier
    {
        A,
        B,
        C,
        D,
        E
    }
    [SerializeField] private int sceneToLoad = -1;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private DestinationIdentifier destinatioin;
    [SerializeField] private float fadeOutTime = 1f;
    [SerializeField] private float fadeInTime = 2f;
    [SerializeField] private float fadeWaitTime = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //GetComponent<Collider>().enabled = false;
            StartCoroutine(Transition());

        }
    }


    private IEnumerator Transition()
    {
        if(sceneToLoad < 0)
        {
            Debug.LogError("Scene to load not set.");
            yield break;
        }

        DontDestroyOnLoad(this.gameObject);
        
        Fader fader = FindObjectOfType<Fader>();

        // 포탈 탈때 저장 예정
        SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
        PlayerController playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerController.enabled = false;

        // fadeout효과가 다 될때까지 대기 후 Scene로드
        yield return fader.FadeOut(fadeOutTime);

        // 현재 Scene 저장
        savingWrapper.Save();

        // Scene이 다 로드가 될때까지 대기
        yield return SceneManager.LoadSceneAsync(sceneToLoad);

        Debug.Log("LoadSceneAsync Is Done");
        if(playerController == null)
        {
            playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            playerController.enabled = false;
            Debug.Log("PlayerController was null");
        }

        // 현재 Scene 데이터 로드
        savingWrapper.Load();

        // 다른 목적지를 받아와서 위치 초기화
        Portal otherPortal = GetOtherPortal();
        UpdatePlayer(otherPortal);


        // 이동 후 다시 저장
        savingWrapper.Save();

        // 페이더 기다리기
        yield return new WaitForSeconds(fadeWaitTime);
        // 페이더가 없으면 페이더 불러옴
        if(fader == null)
        {
            fader = FindObjectOfType<Fader>();
            Debug.Log("fader was null");
        }

        // 페이더 재생
        yield return fader.FadeIn(fadeInTime);

        // 조작 활성화
        if (playerController == null)
        {
            playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            Debug.Log("PlayerController was null");
        }
        playerController.enabled = true;
        
        // 현재 유지한 지난 포탈 파괴
        Destroy(gameObject);
        
    }

    private void UpdatePlayer(Portal otherPortal)
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<NavMeshAgent>().enabled = false;
        player.transform.position = otherPortal.spawnPoint.position;
        player.transform.rotation = otherPortal.spawnPoint.rotation;
        
        player.GetComponent<NavMeshAgent>().enabled = true;
    }

    private Portal GetOtherPortal()
    {
        foreach(Portal portal in FindObjectsOfType<Portal>())
        {
            if (portal == this) continue;
            if (portal.destinatioin != destinatioin) continue;

            return portal;
        }
        return null;
    }

}
