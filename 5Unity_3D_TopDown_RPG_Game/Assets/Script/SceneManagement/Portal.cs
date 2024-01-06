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

        // ��Ż Ż�� ���� ����
        SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
        PlayerController playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerController.enabled = false;

        // fadeoutȿ���� �� �ɶ����� ��� �� Scene�ε�
        yield return fader.FadeOut(fadeOutTime);

        // ���� Scene ����
        savingWrapper.Save();

        // Scene�� �� �ε尡 �ɶ����� ���
        yield return SceneManager.LoadSceneAsync(sceneToLoad);

        Debug.Log("LoadSceneAsync Is Done");
        if(playerController == null)
        {
            playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            playerController.enabled = false;
            Debug.Log("PlayerController was null");
        }

        // ���� Scene ������ �ε�
        savingWrapper.Load();

        // �ٸ� �������� �޾ƿͼ� ��ġ �ʱ�ȭ
        Portal otherPortal = GetOtherPortal();
        UpdatePlayer(otherPortal);


        // �̵� �� �ٽ� ����
        savingWrapper.Save();

        // ���̴� ��ٸ���
        yield return new WaitForSeconds(fadeWaitTime);
        // ���̴��� ������ ���̴� �ҷ���
        if(fader == null)
        {
            fader = FindObjectOfType<Fader>();
            Debug.Log("fader was null");
        }

        // ���̴� ���
        yield return fader.FadeIn(fadeInTime);

        // ���� Ȱ��ȭ
        if (playerController == null)
        {
            playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            Debug.Log("PlayerController was null");
        }
        playerController.enabled = true;
        
        // ���� ������ ���� ��Ż �ı�
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
