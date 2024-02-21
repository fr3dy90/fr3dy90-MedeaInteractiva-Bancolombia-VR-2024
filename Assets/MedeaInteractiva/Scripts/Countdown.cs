using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public Text txt_cont;
    public Image circleOutline;
    Tweener tween;

    public int level;

    void OnEnable()
    {
        circleOutline.DOFillAmount(1, 1.0f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo)
               .OnStepComplete(() => {
                   circleOutline.fillClockwise = !circleOutline.fillClockwise;
               })
               .Play();

        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        txt_cont.text = "5";
        yield return new WaitForSeconds(1);
        txt_cont.text = "4";
        yield return new WaitForSeconds(1);
        txt_cont.text = "3";
        yield return new WaitForSeconds(1);
        txt_cont.text = "2";
        yield return new WaitForSeconds(1);
        txt_cont.text = "1";
        yield return new WaitForSeconds(1);

        circleOutline.DOPause();
        circleOutline.fillAmount = 0;
        if(level == 0)
        {
            LevelGame1.Instance.StartGame();
        }

        if(level == 1)
        {
            LevelGame2.Instance.StartGame();
        }
    }
}
