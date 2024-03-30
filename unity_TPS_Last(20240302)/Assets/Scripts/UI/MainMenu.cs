using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnPlayButton()
	{
		SceneManager.LoadScene("MainGame");
	}

	public void OnQuitButton()
	{
		Debug.Log("Quitting Game");
		Application.Quit();
	}
}
