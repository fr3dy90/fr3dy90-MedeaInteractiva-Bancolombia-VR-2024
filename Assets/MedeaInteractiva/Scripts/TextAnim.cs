using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnim : MonoBehaviour
{
    public Text txt;
    public string[] texto;
    private float durationText;

    [Space(10)]
    public GameObject sendMessage;
    public string methodName;
    public int sequence;

    public AudioSource _audio;
    public AudioClip[] _clip;

    void OnEnable()
    {
        StartCoroutine(AnimText());
    }

    IEnumerator AnimText()
    {
        for (int i = 0; i < texto.Length; i++)
        {
            durationText = texto[i].Length * 0.07f;
            txt.text = texto[i];
            PlayAudioLocution(i);
            yield return new WaitForSeconds(_audio.clip.length);
        }

        if(sendMessage != null)
        {
            sendMessage.SendMessage(methodName, sequence);
        }
    }

    public void PlayAudioLocution(int a)
    {
        if (_audio.isPlaying)
            _audio.Stop();

        _audio.clip = _clip[a];
        _audio.Play();

    }
}
