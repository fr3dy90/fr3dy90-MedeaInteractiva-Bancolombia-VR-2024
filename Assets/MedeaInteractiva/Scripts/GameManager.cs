using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FadeController _fadeController;

    private void Awake()
    {
        SceneManager.LoadSceneAsync(Constants.environmentScene, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(Constants.logicScene, LoadSceneMode.Additive);
    }
}

