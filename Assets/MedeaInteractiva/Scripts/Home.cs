using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]

public class Home : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private GameObject[] sequenceObject;
    [SerializeField] private GameObject[] level_menu;
    [SerializeField] private GameObject[] level_home;
    [SerializeField] private GameObject[] level_momento_1;
    [SerializeField] private GameObject[] level_momento_2;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip[] _clip;
    [SerializeField] private float timeScaleSpeed = 1;
#pragma warning restore 0649
    
    void Start()
    {
        return;
        DOTween.Init();

        StartCoroutine(StartAnim());
    }

    public void Launch()
    {
        DOTween.Init();

        StartCoroutine(StartAnim());
    }

    public void Update()
    {
        Time.timeScale = timeScaleSpeed;
    }
    IEnumerator StartAnim()
    {
        sequenceObject[0].SetActive(true);
        yield return new WaitForSeconds(2);
        sequenceObject[0].SetActive(false);
        sequenceObject[1].SetActive(true);
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(StartInstruction());
    }

    IEnumerator StartInstruction()
    {
        sequenceObject[2].GetComponent<CanvasGroup>().DOFade(1.0f, 1.0f);
        PlayAudio(0);
        yield return new WaitForSeconds(_audio.clip.length);
        sequenceObject[2].SetActive(false);

        sequenceObject[3].GetComponent<CanvasGroup>().DOFade(1.0f, 1.0f);
        PlayAudio(1);
        yield return new WaitForSeconds(_audio.clip.length);
        sequenceObject[3].SetActive(false);

        sequenceObject[4].GetComponent<CanvasGroup>().DOFade(1.0f, 1.0f);
        PlayAudio(2);
        yield return new WaitForSeconds(_audio.clip.length);

        sequenceObject[5].SetActive(true);
    }

    IEnumerator _LoadMoment1()
    {
        float seconds = ReticlePointerController.Instace.maxSliderValue;
        ReticlePointerController.Instace.loading = true;

        yield return new WaitForSeconds(seconds);

        if(ReticlePointerController.Instace.ready && !ReticlePointerController.Instace.loading)
        {
            foreach(GameObject component in level_momento_1)
            {
                component.SetActive(true);
            }
            foreach (GameObject component in level_home)
            {
                component.SetActive(false);
            }
            foreach (GameObject component in level_momento_2)
            {
                component.SetActive(false);
            }

            foreach (GameObject component in level_menu)
            {
                component.SetActive(false);
            }

            ReticlePointerController.Instace.ready = false;
        }
    }

    IEnumerator _LoadMoment2()
    {
        float seconds = ReticlePointerController.Instace.maxSliderValue;
        ReticlePointerController.Instace.loading = true;

        yield return new WaitForSeconds(seconds);

        if(ReticlePointerController.Instace.ready && !ReticlePointerController.Instace.loading)
        {
            foreach(GameObject component in level_momento_1)
            {
                component.SetActive(value: false);
            }
            foreach (GameObject component in level_momento_2)
            {
                component.SetActive(true);
            }

            foreach (GameObject component in level_menu)
            {
                component.SetActive(false);
            }

            ReticlePointerController.Instace.ready = false;
        }
    }

    IEnumerator _LoadMenu()
    {
        float seconds = ReticlePointerController.Instace.maxSliderValue;
        ReticlePointerController.Instace.loading = true;

        yield return new WaitForSeconds(seconds);

        if(ReticlePointerController.Instace.ready && !ReticlePointerController.Instace.loading)
        {
            LoadMenuExternal();
            ReticlePointerController.Instace.ready = false;
        }
    }

    public void LoadMoment1()
    {
        StartCoroutine(_LoadMoment1());
    }

    public void LoadMoment2()
    {
        StartCoroutine(_LoadMoment2());
    }

    public void LoadMenu()
    {
        StartCoroutine(_LoadMenu());
    }

    public void LoadMenuExternal()
    {
        foreach (GameObject component in level_home)
        {
            component.SetActive(false);
        }

        foreach (GameObject component in level_momento_1)
        {
            component.SetActive(false);
        }

        foreach (GameObject component in level_momento_2)
        {
            component.SetActive(false);
        }

        foreach (GameObject component in level_menu)
        {
            component.SetActive(true);
        }
    }

    public void PlayAudio(int a)
    {
        if (_audio.isPlaying)
            _audio.Stop();

        _audio.clip = _clip[a];
        _audio.Play();

    }
}
