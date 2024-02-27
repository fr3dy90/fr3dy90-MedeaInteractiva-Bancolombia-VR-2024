using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class SceneController : MonoBehaviour
{
   [SerializeField] private CanvasGroup _fade;
   [SerializeField] private AnimationCurve _curve;
   [SerializeField] private Home _home;
   [SerializeField] private GameObject _welcome;
   [SerializeField] private GvrEditorEmulator _gvrEditorEmulator;
   

   private void Awake()
   {
      SceneManager.sceneLoaded += OnLaunchXperience;
      LoadEnvironment();
   }

   private void Start()
   {
      
   }
   
   private void OnLaunchXperience(Scene scene, LoadSceneMode mode)
   {
      if (scene.name == Constants.environmentScene && mode == LoadSceneMode.Additive)
      {
         SceneManager.sceneLoaded -= OnLaunchXperience;
         _gvrEditorEmulator.enabled = false;
         _welcome.SetActive(true);
         _home.Launch();
      }
   }

   private void LoadEnvironment()
   {
      SceneManager.LoadSceneAsync(Constants.environmentScene, LoadSceneMode.Additive);
   }
 
}
