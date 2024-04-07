using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnswerController : MonoBehaviour
{
    [SerializeField] Button _button;

    private void Awake()
    {
        if(_button == null)
            _button = GetComponent<Button>();
    }
    
    public void OnClick()
    {
        StartCoroutine(Click());
    }
    
    IEnumerator Click()
    {
        float seconds = ReticlePointerController.Instace.maxSliderValue;
        ReticlePointerController.Instace.loading = true;

        yield return new WaitForSeconds(seconds);

        if(ReticlePointerController.Instace.ready && !ReticlePointerController.Instace.loading)
        {
            _button.onClick?.Invoke();
            ReticlePointerController.Instace.StopLoading();
            ReticlePointerController.Instace.ready = false;
        }
    }

    public void Exit()
    {
        ReticlePointerController.Instace.StopLoading();
    }
}
