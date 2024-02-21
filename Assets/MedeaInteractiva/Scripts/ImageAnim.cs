using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ImageAnim : MonoBehaviour
{
    private Image imageFade;
    private CanvasGroup canvasGroup;

    public bool isFade;
    public bool isFadeCanvasGroup;

    private void OnEnable()
    {
        if (isFade)
        {
            imageFade = GetComponent<Image>();
            DOTween.Sequence().SetUpdate(true).SetAutoKill(false).SetRecyclable(true)
                .Append(imageFade.DOFade(1.0f, 1.0f));
        }

        if (isFadeCanvasGroup)
        {
            canvasGroup = GetComponent<CanvasGroup>();
            DOTween.Sequence().SetUpdate(true).SetAutoKill(false).SetRecyclable(true)
                      .Append(canvasGroup.DOFade(1.0f, 2.0f));
        }
    }
}
