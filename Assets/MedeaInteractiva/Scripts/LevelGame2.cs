using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelGame2 : MonoBehaviour
{
    private static LevelGame2 instance;

#pragma warning disable 0649
    [SerializeField]
    private int contEstrellas;
    [SerializeField]
    private Transform posContEstrella;
    [SerializeField]
    private Text txtContEstrellas;
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
    private GameObject cuentaRegresiva;
    [SerializeField]
    private GameObject[] sequenceObject;
    [SerializeField]
    private AudioSource _audio;
    [SerializeField]
    private AudioClip[] _clip;
#pragma warning restore 0649

    [HideInInspector]
    public int contRespGood;
    [HideInInspector]
    public int contRespBad;

    public TextMeshProUGUI feedBackText;

    public GameObject triggerJuego;
    public GameObject feedBackObj;
    public GameObject[] objectsToDrag;
    public GameObject[] felicitacionesNivel;
    private int currentObject = -1;

    public Transform[] posDrag;
    public Text lvlIndicator;
    [SerializeField] private GameObject cierre;
    [SerializeField] private GameObject continueWindowObject;
    [SerializeField] private GameObject[] nextPartConfigurationON;
    [SerializeField] private GameObject[] nextPartConfigurationOFF;

    public static LevelGame2 Instance { get => instance; set => instance = value; }

    readonly string[] textSlots = new string[] {
        "Agiliza el conteo de los billetes facilitando" + "\n" + "su agarre.",
        "Humedece los sellos para tener una impresión" + "\n" + "clara sobre el papel.",
        "Facilita mantener en orden los" + "\n" + "soportes de las transacciones que" + "\n" + "deban ser enviados a gestión documental.",
        "Limpia partículas de la máquina" + "\n" + "contadora de billetes.",
        "Sirve para aplicar el removedor o el" + "\n" + "hipoclorito sobre los cheques o los" + "\n" + "títulos valor.",
        "Sirven para amarrar fajos de 100 billetes" + "\n" + "según su denominación.",
        "Permite que la impresión de los sellos" + "\n" + "sobre el papel sea lo más legible posible.",
        "Utilizado para tomar la impresión de la huella dactilar" + "\n" + "del cliente y se utiliza principalmente cuando los" + "\n" + "clientes van a retirar con un cheque más" + "\n" + "de un millón de pesos.",
        "Útil para perforar nuestra papeleria.",
        "Ideal para cualquier clase de cortes" + "\n" + "en papeles u hojas que usemos.",
        "Usando este elemento podemos unir" + "\n" + "cualquier tipo de documentos.",
        "Si debemos separar algún documento" + "\n" + "y deshacernos de su gancho, es ideal" + "\n" + "usar este objeto.",
        "Clasifica, cuenta y valida la autenticidad" + "\n" + "de los billetes cuando se tienen altos" + "\n" + "flujos de efectivo.",
        "Imprime los soportes de las" + "\n" + "transacciones realizadas por los clientes.",
        "Lee las tarjetas débito y crédito para" + "\n" + "realizar transferencias, retiros y pagos.",
        "Escanea los códigos de barras presentes" + "\n" + "en facturas, declaraciones y" + "\n" + "cheques Bancolombia.",
        "Registra manualmente los pagos con" + "\n" + "tarjeta de crédito.",
        "Utilizada para perforar y marcar en tinta el" + "\n" + "valor que se esté registrando en un título" + "\n" + "valor con el fin de dar mayor respaldo o" + "\n" + "seguridad a ese título.",
        "Usada para verificar la autenticidad de los" + "\n" + "cheques Bancolombia, los billetes o las" + "\n" + "cédulas de ciudadanía.",
        "Autentica la identidad del cliente cuando los" + "\n" + "procesos, por seguridad, requieren esta" + "\n" + "verificación.",
        "Verifica la autenticidad de los cheques. Se" + "\n" + "aplica sobre el número consecutivo de estos.",
        "Verifica la autenticidad de los cheques o" + "\n" + "títulos valor. Se aplica sobre un espacio en" + "\n" + "blanco del papel.",
        "Usados para estampar un mensaje en los" + "\n" + "documentos asociados con las diferentes" + "\n" + "transacciones, con el fin de certificarlos." + "\n" + "Algunos son: Sello recibido sin pago, sello recibido" + "\n" + "con pago, sello recibido pagado, sello de canje.",
        "Utilizado para custodiar temporalmente los" + "\n" + "fajos de billetes durante la jornada laboral.",
        "Compartimiento que guarda bajo llave los" + "\n" + "billetes clasificados por denominación.",
        "Envía un mensaje de alerta a la Policía y al" + "\n" + "área de seguridad del Banco y se usa solo" + "\n" + "ante una situación de alerta o sospecha.",
        };

    public TextMesh option_1;
    public TextMesh option_2;
    public TextMesh option_3;

    public GameObject objOption_1;
    public GameObject objOption_2;
    public GameObject objOption_3;

    public GameObject nivel2;
    public GameObject nivel3;

    public AudioSource audioBien;
    public AudioSource audioMal;

    public Sprite azul;
    public Sprite verde;
    public Sprite rojo;

    int currentOptions;
    int currentSecuency;

    public TextMeshProUGUI txtPuntaje;

    public GameObject home;

    void Awake()
    {
        for (int i = 0; i < objectsToDrag.Length; i++)
        {
            posDrag[i].localPosition = objectsToDrag[i].transform.localPosition;
        }

        if (instance == null)
            instance = this;

        //DOTween.Init();
    }

    void OnEnable()
    {
        LevelGame2.Instance.ResetGame(false);
        NextSequence(0);
        GlobalData.level = 1;
    }

    public void NextSequence(int sequence)
    {
        StartCoroutine(StartAnim(sequence));
    }

    IEnumerator StartAnim(int sequence)
    {
        float seconds = ReticlePointerController.Instace.maxSliderValue;

        if (sequence == 0)
        {
            sequenceObject[0].SetActive(true);
        }
        else if (sequence == 1)
        {
            sequenceObject[0].SetActive(false);
            sequenceObject[1].SetActive(true);
        }
        else if (sequence == 2)
        {
            ReticlePointerController.Instace.loading = true;

            yield return new WaitForSeconds(seconds);

            if(ReticlePointerController.Instace.ready && !ReticlePointerController.Instace.loading)
            {
                sequenceObject[0].SetActive(false);
                sequenceObject[1].SetActive(false);
                sequenceObject[2].SetActive(true);
                PlayAudioLocution(1);
                yield return new WaitForSeconds(_audio.clip.length);
                sequenceObject[2].SetActive(false);
                sequenceObject[3].SetActive(true);
                PlayAudioLocution(2);
                yield return new WaitForSeconds(_audio.clip.length);
                sequenceObject[3].SetActive(false);
                sequenceObject[4].SetActive(true);
                PlayAudioLocution(3);
                yield return new WaitForSeconds(_audio.clip.length);
                sequenceObject[4].SetActive(false);
                sequenceObject[1].SetActive(true);

                ReticlePointerController.Instace.ready = false;
            }
        }
        else if (sequence == 3)
        {
            sequenceObject[1].SetActive(false);
            cuentaRegresiva.SetActive(true);
        }
    }

    public void Omitir()
    {
        _audio.Stop();
        StopAllCoroutines();
        sequenceObject[0].SetActive(false);
        sequenceObject[1].SetActive(false);
        sequenceObject[2].SetActive(false);
        sequenceObject[3].SetActive(false);
        sequenceObject[4].SetActive(false);
        cuentaRegresiva.SetActive(true);
    }

    public void StartGame()
    {
        cuentaRegresiva.SetActive(false);
        panelJuego.SetActive(true);
        triggerJuego.SetActive(true);
        Game(0);
    }

    public void Game(int secuency)
    {
        objOption_1.GetComponent<SpriteRenderer>().color = Color.white;
        objOption_2.GetComponent<SpriteRenderer>().color = Color.white;
        objOption_3.GetComponent<SpriteRenderer>().color = Color.white;
        objOption_1.GetComponent<BoxCollider>().enabled = true;
        objOption_2.GetComponent<BoxCollider>().enabled = true;
        objOption_3.GetComponent<BoxCollider>().enabled = true;

        switch (secuency)
        {
            case 0:
                objectsToDrag[0].SetActive(true);
                objectsToDrag[1].SetActive(true);
                objectsToDrag[2].SetActive(true);
                option_1.text = textSlots[0];
                option_2.text = textSlots[1];
                option_3.text = textSlots[2];
                break;
            case 1:
                objectsToDrag[3].SetActive(true);
                objectsToDrag[4].SetActive(true);
                objectsToDrag[5].SetActive(true);
                option_1.text = textSlots[3];
                option_2.text = textSlots[4];
                option_3.text = textSlots[5];
                break;
            case 2:
                objectsToDrag[6].SetActive(true);
                objectsToDrag[7].SetActive(true);
                objectsToDrag[8].SetActive(true);
                option_1.text = textSlots[6];
                option_2.text = textSlots[7];
                option_3.text = textSlots[8];
                break;
            case 3:
                objectsToDrag[9].SetActive(true);
                objectsToDrag[10].SetActive(true);
                objectsToDrag[11].SetActive(true);
                option_1.text = textSlots[9];
                option_2.text = textSlots[10];
                option_3.text = textSlots[11];
                break;
            case 4:
                objOption_1.SetActive(false);
                objOption_2.SetActive(false);
                objOption_3.SetActive(false);
                felicitacionesNivel[0].SetActive(true);
                StartCoroutine(Wait(5));
                break;
            case 5:
                nivel2.SetActive(true);
                lvlIndicator.text = "NIVEL 2";
                felicitacionesNivel[0].SetActive(false);
                AudioPlayRetro(true);
                StartCoroutine(Wait(3));
                break;
            case 6:
                nivel2.SetActive(false);
                objOption_1.SetActive(true);
                objOption_2.SetActive(true);
                objOption_3.SetActive(true);
                objectsToDrag[12].SetActive(true);
                objectsToDrag[13].SetActive(true);
                objectsToDrag[14].SetActive(true);
                option_1.text = textSlots[12];
                option_2.text = textSlots[13];
                option_3.text = textSlots[14];
                break;
            case 7:
                objectsToDrag[15].SetActive(true);
                objectsToDrag[16].SetActive(true);
                option_1.text = textSlots[15];
                option_2.text = textSlots[16];
                objOption_3.SetActive(false);
                break;
            case 8:
                objOption_1.SetActive(false);
                objOption_2.SetActive(false);
                objOption_3.SetActive(false);
                felicitacionesNivel[1].SetActive(true);
                AudioPlayRetro(true);
                StartCoroutine(Wait(5));
                break;
            case 9:
                nivel3.SetActive(true);
                lvlIndicator.text = "NIVEL 3";
                felicitacionesNivel[1].SetActive(false);
                StartCoroutine(Wait(3));
                break;
            case 10:
                nivel3.SetActive(false);
                objOption_1.SetActive(true);
                objOption_2.SetActive(true);
                objOption_3.SetActive(true);
                objectsToDrag[17].SetActive(true);
                objectsToDrag[18].SetActive(true);
                objectsToDrag[19].SetActive(true);
                option_1.text = textSlots[17];
                option_2.text = textSlots[18];
                option_3.text = textSlots[19];
                break;
            case 11:
                objectsToDrag[20].SetActive(true);
                objectsToDrag[21].SetActive(true);
                objectsToDrag[22].SetActive(true);
                option_1.text = textSlots[20];
                option_2.text = textSlots[21];
                option_3.text = textSlots[22];
                break;
            case 12:
                objectsToDrag[23].SetActive(true);
                objectsToDrag[24].SetActive(true);
                objectsToDrag[25].SetActive(true);
                option_1.text = textSlots[23];
                option_2.text = textSlots[24];
                option_3.text = textSlots[25];
                break;
            case 13:
                objOption_1.SetActive(false);
                objOption_2.SetActive(false);
                objOption_3.SetActive(false);
                felicitacionesNivel[2].SetActive(true);
                AudioPlayRetro(true);
                StartCoroutine(Wait(5));
                break;
            case 14:
                felicitacionesNivel[2].SetActive(false);
               SceneController.Instance.ChangeScene(MomentScene.Preguntas);
                break;
            case 15:
                PlayAudioLocution(4);
                txtPuntaje.text = "Tu puntaje: " + contEstrellas.ToString();
                sequenceObject[6].SetActive(true);
                break;
            default:
                break;
        }
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

    public void Compare(bool correct, Category1 category1)
    {
        if (correct)
        {
            switch (category1)
            {
                case Category1.NONE:
                    break;
                case Category1.HERRAMIENTA:
                    objOption_1.GetComponent<BoxCollider>().enabled = false;
                    objOption_1.GetComponent<SpriteRenderer>().sprite = verde;
                    break;
                case Category1.SEGURIDAD:
                    objOption_2.GetComponent<BoxCollider>().enabled = false;
                    objOption_2.GetComponent<SpriteRenderer>().sprite = verde;
                    break;
                case Category1.ELEMENTOS:
                    objOption_3.GetComponent<BoxCollider>().enabled = false;
                    objOption_3.GetComponent<SpriteRenderer>().sprite = verde;
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (category1)
            {
                case Category1.NONE:
                    break;
                case Category1.HERRAMIENTA:
                    objOption_1.GetComponent<SpriteRenderer>().sprite = rojo;
                    break;
                case Category1.SEGURIDAD:
                    objOption_2.GetComponent<SpriteRenderer>().sprite = rojo;
                    break;
                case Category1.ELEMENTOS:
                    objOption_3.GetComponent<SpriteRenderer>().sprite = rojo;
                    break;
                default:
                    break;
            }
            feedBackObj.SetActive(true);
            Invoke("DesactiveFeedBack", 3);
        }
        int compare = 0;
        if (correct)
        {
            currentOptions++;
        }
        if (currentSecuency == 7)
        {
            compare = 2;
        }
        else
        {
            compare = 3;
        }
        if (currentOptions == compare)
        {
            objOption_1.GetComponent<SpriteRenderer>().sprite = azul;
            objOption_2.GetComponent<SpriteRenderer>().sprite = azul;
            objOption_3.GetComponent<SpriteRenderer>().sprite = azul;
            currentOptions = 0;
            if (!correct)
            {
                StartCoroutine(Wait(3));
            }
            else
            {
                currentSecuency++;
                Game(currentSecuency);
            }
        }
    }

    public void ResetGame(bool game)
    {
        currentSecuency = 0;
        currentOptions = 0;
        objOption_1.SetActive(true);
        objOption_2.SetActive(true);
        objOption_3.SetActive(true);

        for (int i = 0; i < objectsToDrag.Length; i++)
        {
            objectsToDrag[i].transform.localPosition = posDrag[i].localPosition;
            objectsToDrag[i].GetComponent<DraggableObject>().isCorrectCategory = false;
            objectsToDrag[i].GetComponent<DraggableObject>().isDetectCategory = false;
            objectsToDrag[i].SetActive(false);
        }
        contEstrellas = 0;
        txtContEstrellas.text = "0";
        DOTween.Sequence().SetUpdate(true).SetAutoKill(false).SetRecyclable(true)
            .Append(txtContEstrellas.DOText(contEstrellas.ToString(), 0.5f).SetEase(Ease.Linear).SetAutoKill(false).Pause());
        currentObject = 0;
        triggerJuego.SetActive(false);
        panelJuego.SetActive(false);
        if (game)
        {
            cuentaRegresiva.SetActive(true);
        }
        else
        {
            cuentaRegresiva.SetActive(false);
        }
    }

    public void DesactiveFeedBack()
    {
        feedBackObj.SetActive(false);
    }

    public void SetScore(Category1 category1)
    {
        switch (category1)
        {
            case Category1.NONE:
                break;
            case Category1.HERRAMIENTA:
                contEstrellas++;
                Estrella.transform.position = posContainerHerramientas.position;
                break;
            case Category1.SEGURIDAD:
                contEstrellas++;
                Estrella.transform.position = posContainerSeguridad.position;
                break;
            case Category1.ELEMENTOS:
                contEstrellas++;
                Estrella.transform.position = posContainerElementos.position;
                break;
        }

        DOTween.Sequence().SetUpdate(true).SetAutoKill(false).SetRecyclable(true)
                 .Append(Estrella.transform.DOScale(0.1f, 0.2f))
                 .Append(Estrella.transform.DOMove(posContEstrella.position, 1.0f))
                 .Append(Estrella.transform.DOScale(Vector3.zero, 0.01f))
                 .Append(txtContEstrellas.DOText(contEstrellas.ToString(), 0.5f).SetEase(Ease.Linear).SetAutoKill(false).Pause());
    }

    public void PlayAudioLocution(int a)
    {
        if (_audio.isPlaying)
            _audio.Stop();

        _audio.clip = _clip[a];
        _audio.Play();

    }

    public void LoadScene(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void ShowFeedBack(bool _correct)
    {
        feedBackObj.SetActive(true);
        StartCoroutine(WaitFeedBack(_correct));
    }

    IEnumerator Wait(float _seg)
    {
        yield return new WaitForSeconds(_seg);
        currentSecuency++;
        Game(currentSecuency);
    }

    IEnumerator WaitFeedBack(bool correct)
    {
        if (correct)
        {
            yield return new WaitForSeconds(3);
            feedBackObj.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(5);
            feedBackObj.SetActive(false);
        }
    }

    public void HideRetroalimentacion()
    {
        StartCoroutine(_HideRetroalimentacion());
    }

    IEnumerator _HideRetroalimentacion()
    {
        float seconds = ReticlePointerController.Instace.maxSliderValue;
        ReticlePointerController.Instace.loading = true;

        yield return new WaitForSeconds(seconds);

        if(ReticlePointerController.Instace.ready && !ReticlePointerController.Instace.loading)
        {
            cierre.SetActive(false);
            cierre.transform.parent.gameObject.SetActive(false);
            continueWindowObject.SetActive(true);
            
            ReticlePointerController.Instace.ready = false;
        }
    }

    public void ConfigureNextPart()
    {
        StartCoroutine(_ConfigureNextPart());
    }

    IEnumerator _ConfigureNextPart()
    {
        float seconds = ReticlePointerController.Instace.maxSliderValue;
        ReticlePointerController.Instace.loading = true;

        yield return new WaitForSeconds(seconds);

        if(ReticlePointerController.Instace.ready && !ReticlePointerController.Instace.loading)
        {
            
            foreach (var go in nextPartConfigurationON)
            {
                go.SetActive(true);
            }

            foreach (var go in nextPartConfigurationOFF)
            {
                go.SetActive(false);
            }

            LevelGame1.Instance.ResetData();
            ReticlePointerController.Instace.ready = false;
        }
    }

    public void EndMomento()
    {
        StartCoroutine(_EndMomento());
    }

    IEnumerator _EndMomento()
    {
        float seconds = ReticlePointerController.Instace.maxSliderValue;
        ReticlePointerController.Instace.loading = true;

        yield return new WaitForSeconds(seconds);

        if(ReticlePointerController.Instace.ready && !ReticlePointerController.Instace.loading)
        {

            cierre.SetActive(false);
            cierre.transform.parent.gameObject.SetActive(false);
            //continueWindowObject.SetActive(true);
            panelJuego.SetActive(false);


            home.GetComponent<Home>().LoadMenuExternal();

            ReticlePointerController.Instace.ready = false;
        }
    }

}
