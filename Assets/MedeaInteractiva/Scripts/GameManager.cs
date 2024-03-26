using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FadeController _fadeController;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        _fadeController.HardController(FadeController._one, () => LoaderScenes(ScenesNames.Momento1));
    }
    

    private void LoaderScenes(ScenesNames _sceneName)
    {
        if (OnCheckScene(_sceneName))
        {
            return;
        }
        
        SceneManager.LoadSceneAsync(_sceneName.ToString(), LoadSceneMode.Additive);
    }
    private void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
    {
        switch (_scene.name)
        {
            case "GameManager":
                break;
            case "Momento1":
                LoaderScenes(ScenesNames.Oficina_Model);
                SceneManager.SetActiveScene(_scene);
                break;
            case "Oficina_Model":
                _fadeController.FadeIn(OnLaunchExperience);
                break;
        }
    }
    
    private bool OnCheckScene(ScenesNames _sceneName)
    {
        return SceneManager.GetSceneByName(_sceneName.ToString()).isLoaded;
    }

    private void OnLaunchExperience()
    {
        SceneController.Instance.OnLaunchXperience();
    }
}

public enum ScenesNames
{
    GameManager,
    Momento1,
    Oficina_Model
}

