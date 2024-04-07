using System;
using UnityEngine;

public class ConoceController : MonoBehaviour
{
    [SerializeField] private BaseController[] _screen;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Transform _parent;
    
    public static Action<int, bool, Action> OnInitScreen;
    public static Action OnClose;
    public Action onComplete;

    private void Awake()
    {
        OnInitScreen += ManageScreen;
        OnClose += OnCLose;
        _parent.gameObject.SetActive(false);
    }
    
    private void Start()
    { 
        //ManageScreen(0, false, null);
    }

    public void ManageScreen(int index, bool fadeOut, Action onComplete)
    {
        if (fadeOut)
        {
            _canvasGroup.alpha = 1;
            StartCoroutine( Tools.Fade(1, 0, 1f, _canvasGroup, () =>
            {
                SetScreen(index, () =>
                {
                    StartCoroutine(Tools.Fade(0, 1, 1f, _canvasGroup, null));
                });
            }));
            
        }
        else
        {
            _canvasGroup.alpha = 0;
            SetScreen(index, () =>
            {
                StartCoroutine(Tools.Fade(0, 1, 1f, _canvasGroup, null));
            });
        }
        _parent.gameObject.SetActive(true);
    }

    public void SetScreen(int index, Action onComplete = null)
    {
        _screen[index].Init();
        for (int i = 0; i < _screen.Length; i++)
        {
            if (i == index)
            {
                _screen[i].gameObject.SetActive(true);
            }
            else
            {
                _screen[i].gameObject.SetActive(false);
            }
        }
        onComplete?.Invoke();
    }
    
    private void OnCLose()
    {
        StartCoroutine(Tools.Fade(1, 0, 1f, _canvasGroup, () =>
        {
            _parent.gameObject.SetActive(false);
            onComplete?.Invoke();
        }));
    }
}
