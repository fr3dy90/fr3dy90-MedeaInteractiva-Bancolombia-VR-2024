using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelGame1 : MonoBehaviour
{
    private static LevelGame1 instance;

#pragma warning disable 0649
    [SerializeField]
    private int contHerramientas;
    [SerializeField]
    private int contSeguridad;
    [SerializeField]
    private int contElementos;
    [SerializeField]
    private int maxHerramientas;
    [SerializeField]
    private int maxSeguridad;
    [SerializeField]
    private int maxElementos;
    [SerializeField]
    private int contEstrellas;
    [SerializeField] private int maxEstrellas;
    [SerializeField] private int maxTime;
    [SerializeField]
    private Transform posContEstrella;
    [SerializeField]
    private Text txtContEstrellas;
    [SerializeField]
    private Text txtContTime;
    [SerializeField]
    private Text TextObjectName;
    [SerializeField]
    private GameObject Estrella;
    [SerializeField]
    private Transform posContainerHerramientas;
    [SerializeField]
    private Transform posContainerSeguridad;
    [SerializeField]
    private Transform posContainerElementos;
    [SerializeField]
    private GameObject panelJuego;
    [SerializeField]
    private GameObject triggerJuego;
    [SerializeField]
    private GameObject cuentaRegresiva;
    [SerializeField]
    private GameObject retroExcelente;
    [SerializeField]
    private GameObject retroMuyBien;
    [SerializeField]
    private GameObject retroMal;
    [SerializeField]
    private GameObject[] sequenceObject;
    [SerializeField]
    private AudioSource _audio;
    [SerializeField]
    private AudioClip[] _clip;
#pragma warning restore 0649

    public Image fillHerramientas;
    public Image fillSeguridad;
    public Image fillElementos;

    public GameObject[] objectsToDrag;
    private int currentObject = -1;
    private bool onGame;
    private float timer = 0;

    public AudioSource audioBien;
    public AudioSource audioMal;

    public GameObject home;
    public GameObject cierre;

    public TextMeshProUGUI txtPuntaje;

    public static LevelGame1 Instance { get => instance; set => instance = value; }

    void Awake()
    {
        if (instance == null)
            instance = this;

        //DOTween.Init();
    }

    void OnEnable()
    {
        NextSequence(0);
        GlobalData.level = 0;
    }

    void Update()
    {
        if (onGame)
        {
            timer += Time.deltaTime;
            txtContTime.text = timer.ToString("f0");
        }
    }

    public void NextSequence(int sequence)
    {
        StartCoroutine(StartAnim(sequence));
    }

    IEnumerator StartAnim(int sequence)
    {
        //float seconds = ReticlePointerController.Instace.maxSliderValue;
        
        if(sequence == 0)
        {
            sequenceObject[0].SetActive(true);
        }
        else if(sequence == 1)
        {
            //eticlePointerController.Instace.loading = true;

            yield return new WaitForSeconds(1);

           // if(ReticlePointerController.Instace.ready && !ReticlePointerController.Instace.loading)
            //{
                sequenceObject[0].SetActive(false);
                sequenceObject[1].SetActive(true);
                PlayAudioLocution(0);
                yield return new WaitForSeconds(_audio.clip.length);
                sequenceObject[1].SetActive(false);
                sequenceObject[2].SetActive(true);
        
                //ReticlePointerController.Instace.ready = false;
            //}
        }
        else if (sequence == 2)
        {
            sequenceObject[2].SetActive(false);
            sequenceObject[3].SetActive(true);
        }
        else if (sequence == 3)
        {
            //ReticlePointerController.Instace.loading = true;

            yield return new WaitForSeconds(1);

            //if(ReticlePointerController.Instace.ready && !ReticlePointerController.Instace.loading)
            //{
                sequenceObject[3].SetActive(false);
                sequenceObject[4].SetActive(true);
                yield return new WaitForSeconds(6);
                sequenceObject[4].SetActive(false);
                sequenceObject[5].SetActive(true);
                yield return new WaitForSeconds(6);
                sequenceObject[5].SetActive(false);
                sequenceObject[6].SetActive(true);
                yield return new WaitForSeconds(4);
                sequenceObject[6].SetActive(false);
                cuentaRegresiva.SetActive(true);

                //ReticlePointerController.Instace.ready = false;
            //}
        }        
        else if (sequence == 4)
        {
            
        }
    }

    public void TryAgain()
    {
        StartCoroutine(_TryAgain());
    }

    IEnumerator _TryAgain()
    {
        float seconds = ReticlePointerController.Instace.maxSliderValue;

        ReticlePointerController.Instace.loading = true;

        yield return new WaitForSeconds(seconds);

        if(ReticlePointerController.Instace.ready && !ReticlePointerController.Instace.loading)
        {
            //cuentaRegresiva.SetActive(true);
            retroMal.SetActive(false);
            ResetData();
            NextSequence(0);
            ReticlePointerController.Instace.ready = false;
        }
    }

    public void ResetData()
    {
        contHerramientas = 0;
        contSeguridad = 0;
        contElementos = 0;
        contEstrellas = 0;
        currentObject = -1;
        timer = 0;
        txtContEstrellas.text = "0";
        foreach(GameObject item in objectsToDrag)
        {
            item.transform.localPosition = Vector3.zero;
        }
        fillHerramientas.DOFillAmount(0, 1);
        fillSeguridad.DOFillAmount(0, 1);
        fillElementos.DOFillAmount(0, 1);


        retroExcelente.SetActive(false);
        retroMuyBien.SetActive(false);
        retroMal.SetActive(false);

        //onGame = true;
        //NextSequence(0);
    }

    public void ResetDataBtnFill()
    {
        StartCoroutine(_ResetData());
    }

    IEnumerator _ResetData()
    {
        float seconds = ReticlePointerController.Instace.maxSliderValue;
        ReticlePointerController.Instace.loading = true;

        yield return new WaitForSeconds(seconds);

        if(ReticlePointerController.Instace.ready && !ReticlePointerController.Instace.loading)
        {

            ResetData();
            ReticlePointerController.Instace.ready = false;  
        }
    }

    public void StartGame()
    {
        cuentaRegresiva.SetActive(false);
        panelJuego.SetActive(true);
        triggerJuego.SetActive(true);
        ShowObject();
        onGame = true;
    }

    public void EndGame()
    {
        onGame = false;
        panelJuego.SetActive(false);
        triggerJuego.SetActive(false);

        float totalObjetos = contHerramientas + contSeguridad + contElementos;
        float total80 = totalObjetos * 0.8f;
        
        if(total80 >= maxEstrellas && timer <= maxTime)
        {
            retroExcelente.SetActive(true);
        }
        else if(total80 >= maxEstrellas && timer > maxTime)
        {
            retroMuyBien.SetActive(true);
        }
        else
        {
            retroMal.SetActive(true);
        }
    }

    public void ShowObject()
    {
        if (currentObject < objectsToDrag.Length - 1)
        {
            currentObject++;
            objectsToDrag[currentObject].SetActive(true);
            objectsToDrag[currentObject].transform.localScale = Vector3.zero;
            TextObjectName.text = objectsToDrag[currentObject].GetComponent<DraggableObject>().objectName;
            DOTween.Sequence().SetUpdate(true).SetAutoKill(false).SetRecyclable(true)
                     .Append(objectsToDrag[currentObject].transform.DOScale(1.0f, 0.5f));
        }
        else
        {
            Debug.Log("fin objetos");
            EndGame();
        }
    }

    public void SetScore(Category1 category1)
    {
        float val;
        switch (category1)
        {
            case Category1.NONE:
                break;
            case Category1.HERRAMIENTA:
                contHerramientas++;
                contEstrellas++;
                val = (float)contHerramientas / (float)maxHerramientas;
                fillHerramientas.DOFillAmount(val, 1);
                
                Estrella.transform.position = posContainerHerramientas.position;
                break;
            case Category1.SEGURIDAD:
                contSeguridad++;
                contEstrellas++;
                val = (float)contSeguridad / (float)maxSeguridad;
                fillSeguridad.DOFillAmount(val, 1);

                Estrella.transform.position = posContainerSeguridad.position;
                break;
            case Category1.ELEMENTOS:
                contElementos++;
                contEstrellas++;
                val = (float)contElementos / (float)maxElementos;
                fillElementos.DOFillAmount(val, 1);

                Estrella.transform.position = posContainerElementos.position;
                break;
        }

        DOTween.Sequence().SetUpdate(true).SetAutoKill(false).SetRecyclable(true)
                 .Append(Estrella.transform.DOScale(0.1f, 0.2f))
                 .Append(Estrella.transform.DOMove(posContEstrella.position, 1.0f))
                 .Append(Estrella.transform.DOScale(Vector3.zero, 0.01f))
                 .Append(txtContEstrellas.DOText(contEstrellas.ToString(), 0.5f).SetEase(Ease.Linear).SetAutoKill(false).Pause());

        Invoke("ShowObject", 2);
    }

    public void AudioPlayRetro(bool _correcto)
    {
        if (_correcto)
        {
            audioBien.Play();
        }
        else
        {
            audioMal.Play();
        }
    }

    public void PlayAudioLocution(int a)
    {
        if (_audio.isPlaying)
            _audio.Stop();

        _audio.clip = _clip[a];
        _audio.Play();

    }

    IEnumerator _LoadScene(string level)
    {
        float seconds = ReticlePointerController.Instace.maxSliderValue;
        ReticlePointerController.Instace.loading = true;

        yield return new WaitForSeconds(seconds);

        if(ReticlePointerController.Instace.ready && !ReticlePointerController.Instace.loading)
        {
            SceneManager.LoadScene(level);
            ReticlePointerController.Instace.ready = false;
        }
    }

    public void LoadScene(string level)
    {
        StartCoroutine(_LoadScene(level));
    }

    public void HideRetroalimentacion()
    {
        StartCoroutine(_HideRetroalimentacion());
    }

    IEnumerator _HideRetroalimentacion()
    {
        //float seconds = ReticlePointerController.Instace.maxSliderValue;
        float seconds = 1;
        //ReticlePointerController.Instace.loading = true;

        yield return new WaitForSeconds(seconds);

        //if(ReticlePointerController.Instace.ready && !ReticlePointerController.Instace.loading)
        //{
            retroExcelente.SetActive(false);
            retroMuyBien.SetActive(false);
            retroMal.SetActive(false);

            //ResetData();

            //ReticlePointerController.Instace.ready = false;
        //}
    }

    public void ContinueNextMomento()
    {
        StartCoroutine(_ContinueNextMomento());
    }

    IEnumerator _ContinueNextMomento()
    {
        //float seconds = ReticlePointerController.Instace.maxSliderValue;
        float seconds = 1;
        //ReticlePointerController.Instace.loading = true;

        yield return new WaitForSeconds(seconds);

        //if(ReticlePointerController.Instace.ready && !ReticlePointerController.Instace.loading)
        //{
            ResetData();
            home.GetComponent<Home>().LoadMoment2();

            retroExcelente.SetActive(false);
            retroMuyBien.SetActive(false);
            retroMal.SetActive(false);


           // ReticlePointerController.Instace.ready = false;
        //}
    }

    public void EndMomento()
    {
        StartCoroutine(_EndMomento());
    }

    IEnumerator _EndMomento()
    {
        //float seconds = ReticlePointerController.Instace.maxSliderValue;
        float seconds = 1;
        //ReticlePointerController.Instace.loading = true;

        yield return new WaitForSeconds(seconds);

        //if(ReticlePointerController.Instace.ready && !ReticlePointerController.Instace.loading)
        //{
            ResetData();
            
            cierre.SetActive(false);


            home.GetComponent<Home>().LoadMenuExternal();
            home.GetComponent<Home>().OnCompletedClasifica();

            //ReticlePointerController.Instace.ready = false;
            //}
    }

    public void GoToCierre()
    {
        StartCoroutine(_GotoCierre());
    }

    IEnumerator _GotoCierre()
    {
        //float seconds = ReticlePointerController.Instace.maxSliderValue;
        float seconds = 1;
        //ReticlePointerController.Instace.loading = true;

        yield return new WaitForSeconds(seconds);

        //if(ReticlePointerController.Instace.ready && !ReticlePointerController.Instace.loading)
        //{
            
            retroExcelente.SetActive(false);
            retroMuyBien.SetActive(false);
            retroMal.SetActive(false);

            txtPuntaje.text = "Tu puntaje: " + contEstrellas.ToString();

            cierre.SetActive(true);



            //ReticlePointerController.Instace.ready = false;
        //}
    }
}
