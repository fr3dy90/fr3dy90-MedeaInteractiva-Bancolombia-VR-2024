using System;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    
    [Header("Animations")] [SerializeField]
    private AnimationsController _animationsController;

    [Header("Avatar")] [SerializeField] private AvatarController _avatarController;

    [Header("Home")] [SerializeField] private Home _home;

    [Header("Conoce")] [SerializeField] private ConoceController _conoceController;
    
    [Header("Preguntas")] [SerializeField] private QuestionsController _questionsController;

    [Header("Momento 1")] [SerializeField] private LevelGame1 _momento1;

    [Header("Momento 2")] [SerializeField] private LevelGame2 _momento2;

    public MomentScene actualmoment;
    public int indexMenu;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        ChangeScene(actualmoment);
        _conoceController.onComplete += () =>
        {
            Home.OnSetIndex(1);
            ChangeScene(MomentScene.Menu);
        };
    }

    public void ChangeScene(MomentScene _moment)
    {
        actualmoment = _moment;
        switch (_moment)
        {
            case MomentScene.Intro:
                _animationsController.SetAnimator(CinematicState.Intro);
                break;
            case MomentScene.Avatar:
                _avatarController.PlayVideo();
                break;
            case MomentScene.Home:
                _home.LaunchXperience();
                break;
            case MomentScene.Menu:
                Home.OnSetIndex?.Invoke(indexMenu);
                _home.LoadMenuExternal();
                break;
            case MomentScene.Conoce:
                _conoceController.ManageScreen(0, false, null);
                break;
            case MomentScene.Preguntas:
                _questionsController.StartQuestions();
                break;
            case MomentScene.Momento1:
                break;
            case MomentScene.Momento2:
                _momento2.Game(15);
                break;
        }
    }
}

public enum MomentScene
{
    Intro,
    Avatar,
    Home,
    Menu,
    Conoce,
    Momento1,
    Momento2,
    Preguntas
}
