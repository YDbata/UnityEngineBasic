using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

// 일반적인 singlton 패턴 구현
public class GameManager
{
    public static GameManager instance
    {
        get
        {
            if(_instance == null)
                _instance = new GameManager();
            return _instance;
        }
    }
    private static GameManager _instance;
}

// mono일 때
public class GameManager1 : MonoBehaviour
{
    public static GameManager1 instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameObject().AddComponent<GameManager1>();
            return _instance;
        }
    }
    private static GameManager1 _instance;
}


public class PlayManager : MonoBehaviour
{
    // singlton 패턴 구현
    public static PlayManager instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameObject().AddComponent<PlayManager>();
            return _instance;
        }
    }
    private static PlayManager _instance;
    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(_instance.gameObject);
        }
        _instance = this;
    }

    public Runner lead
    {
        get
        {
            if(_runnerFinishCount > 0)
            {
                return _runnersFinished[0];
            }
            Runner runner = _runners[0];
            for (int i = 0; i < _runners.Length; i++)
            {
                if(runner.transform.position.z < _runners[i].transform.position.z)
                    runner = _runners[i];

                if(runner.transform.position == _platforms[0].position)
                {
                    runner = _runners[i];
                }
            }
            return runner;
        }
    }

    //private ParticleSystem ps = GetComponent<ParticleSystem>();
    private Runner[] _runners = new Runner[5];
    private Runner[] _runnersFinished = new Runner[5];
    private int _runnerCount;
    private int _runnerFinishCount;



    [SerializeField] private Transform[] _platforms;
    public void RegisterRunner(Runner runner)
    {
        _runners[_runnerCount++] = runner;
        
    }

    public void RegisterRunnerFinish(Runner runner)
    {
        _runnersFinished[_runnerFinishCount++] = runner;
        if(_runnerFinishCount == _runnerCount)
        {
            Invoke("PlaceRunnersOnPlatforms", 3.0f);
        }
    }

    private void PlaceRunnersOnPlatforms()
    {

        for (int i = 0; i < _platforms.Length; i++)
        {
            _runnersFinished[i].transform.position = _platforms[i].position;
        }
    }

}
