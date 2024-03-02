using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menus : MonoBehaviour
{
	[Header("All Menus")]
	public GameObject PauseMenuUI;
	public GameObject EndGameMenuUI;
	public GameObject ObjectivesMenuUI;
	public MissionComplete missionComplete;

	private Button ShootButton;
	private Button InteractionButton;

	public static bool GameisStopped = false;
	public static bool ShootButtonClicked = false;
	public static bool InteractionButtonClicked = false;

	private void Awake()
	{
		missionComplete.Init();
		//ShootButton.OnDown.AddListener(OnDownShootButton);
		//ShootButton.OnUp.AddListener(OnUpShootButton);
		//InteractionButton.OnDown.AddListener(OnDownInteractionButton);
		//InteractionButton.OnUp.AddListener(OnUpInteractionButton);
	}

	public void OnDownShootButton()
	{
		Debug.Log("shoot down");
		ShootButtonClicked = true;

    }

    public void OnUpShootButton()
    {
        Debug.Log("shoot up");
        ShootButtonClicked = false;

    }

    public void OnDownInteractionButton()
    {
		InteractionButtonClicked = true;
    }

    public void OnUpInteractionButton()
    {
        InteractionButtonClicked = false;
    }

    public void ShowObjective()
	{
        ObjectivesMenuUI.SetActive(true);
        Time.timeScale = 0;
        //Cursor.lockState = CursorLockMode.None;
        GameisStopped = true;
    }

	public void HideObjective()
	{
        ObjectivesMenuUI.SetActive(false);
        Time.timeScale = 1;
        //Cursor.lockState = CursorLockMode.Locked;
        GameisStopped = false;
    }

	public void Pause()
	{
		PauseMenuUI.SetActive(true);
		Time.timeScale = 0;
		Cursor.lockState = CursorLockMode.None;
		GameisStopped = true;
	}

	public void Resume()
	{
		PauseMenuUI.SetActive(false);
		Time.timeScale = 1;
		Cursor.lockState = CursorLockMode.Locked;
		GameisStopped = false;
	}

	public void ReStart()
	{
		SceneManager.LoadScene("MainGame");
	}

	public void LoadMenu()
	{
		SceneManager.LoadScene("Menu");
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
