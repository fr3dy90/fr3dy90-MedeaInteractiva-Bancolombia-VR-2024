using System;
using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    
    public float fadeDuration = 1f;
    public const int _zero = 0;
    public const int _one = 1;

    public  Action OnFadeInComplete;
    public  Action OnFadeOutComplete;

    public void FadeIn(Action fadeInCompleteCallback = null)
    {
        HardController(_one);
        OnSetConfig(true);
        StartCoroutine(FadeCanvasGroup(_one, _zero, fadeDuration, () =>
        {
            OnFadeInComplete?.Invoke();
            fadeInCompleteCallback?.Invoke(); 
        }));
        OnSetConfig(false);
    }

    
    public void FadeOut(Action fadeOutCompleteCallback = null)
    {
        HardController(_zero);
        OnSetConfig(true);
        StartCoroutine(FadeCanvasGroup(_zero, _one, fadeDuration, () =>
        {
            OnFadeOutComplete?.Invoke(); 
            fadeOutCompleteCallback?.Invoke();
        }));
    }

    public void HardController(int _start, Action onComplete = null)
    {
        canvasGroup.alpha = _start;
        onComplete?.Invoke();
        
    }

    private void OnSetConfig(bool isActive)
    {
        canvasGroup.gameObject.SetActive(isActive);
        canvasGroup.interactable =isActive; 
        canvasGroup.blocksRaycasts = isActive;
    }

    
    private IEnumerator FadeCanvasGroup(float startAlpha, float targetAlpha, float duration, Action onComplete = null)
    {
        float currentTime = 0f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, currentTime / duration);
            canvasGroup.alpha = alpha;
            yield return null;
        }
        onComplete?.Invoke();
    }
}
