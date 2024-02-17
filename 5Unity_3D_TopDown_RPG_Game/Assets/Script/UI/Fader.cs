using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CanvasGroup), typeof(Image))]
public class Fader : MonoBehaviour
{

    CanvasGroup canvasGroup;
    Coroutine currentActiveFade = null;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        FadeIn(3);
    }
    public void FadeoutImmediate()
    {
        canvasGroup.alpha = 1;
    }
    public Coroutine FadeOut(float time)
    {
        return Fade(1, time);
    }

    public Coroutine FadeIn(float time)
    {
        return Fade(0, time);
    }

    public Coroutine Fade(float target, float time)
    {
        Debug.Log("currentActiveFade"+ currentActiveFade);
        if (currentActiveFade != null)
        {
            StopCoroutine(currentActiveFade);
        }
        currentActiveFade = StartCoroutine(FadeRoutine(target, time));

        return currentActiveFade;
    }

    private IEnumerator FadeRoutine(float target, float time)
    {
        while(!Mathf.Approximately(canvasGroup.alpha, target))
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.unscaledDeltaTime/time);
            // 매프레임마다 체크 가능
            yield return null;
        }
    }
}
