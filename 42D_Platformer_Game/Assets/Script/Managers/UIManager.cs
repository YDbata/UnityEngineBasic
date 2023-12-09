using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;

    public Canvas gameCanvas;

    private void Awake()
    {
        // 아래함수는 성능에 이슈가 있어서 Awake외에 많이 쓰지 않는 것이 좋다.
        gameCanvas = FindObjectOfType<Canvas>();
        //WebGLInput.captureAllKeyboardInput = false;
    }

    private void OnEnable()
    {
        CharectorEvents.characterDamaged += CharacterTookDamage;
        CharectorEvents.characterHealed += CharacterHealed;
    }

    private void OnDisable()
    {
        CharectorEvents.characterDamaged -= CharacterTookDamage;
        CharectorEvents.characterHealed -= CharacterHealed;
    }

    public void CharacterTookDamage(GameObject character, int damagereceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        // Quaternion.identity : 0,0,0
        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();

        tmpText.text = damagereceived.ToString();
    }

    public void CharacterHealed(GameObject character, int damageRestored)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        // Quaternion.identity : 0,0,0
        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();

        tmpText.text = damageRestored.ToString();
    }

    public void OnExitGame(InputAction.CallbackContext context)
    {
        // if(Application.platform)
        
        if (context.started)
        {
#if(UNITY_EDITOR || DEVELOPMENT_BUILD)
            Debug.Log(this.name + ": " + this.GetType() + " : " +
                System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
#if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
    Application.Quit();
#elif (UNITY_WEBGL)
    SceneManager.LoadScene("QuitScene");
#endif
        }
    }
}
