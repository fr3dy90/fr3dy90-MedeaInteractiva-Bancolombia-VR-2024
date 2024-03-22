using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class SceneController : MonoBehaviour
{
   [Header("Singleton")]
   public static SceneController Instance;

   [Header("Target Platform")]
   public Platform actualPlatform;

   [SerializeField] private CanvasGroup _avatar;
   [SerializeField] private Home _home;
   [SerializeField] private GameObject _welcome;
   [SerializeField] private GvrEditorEmulator _gvrEditorEmulator;

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

   public void OnLaunchXperience()
   {
      CameraController.Instance.Init(actualPlatform, OnPlayerRready);
      
      _gvrEditorEmulator.enabled = false;
      _welcome.SetActive(true);
   }

   private void OnPlayerRready()
   {
      StartCoroutine(Tools.Fade(0, 1, 2f, _avatar));
      _avatar.gameObject.SetActive(true);
      _avatar.GetComponent<VideoPlayer>().Play();
      StartCoroutine(OnFadeInAvatar((float)_avatar.GetComponentInChildren<VideoPlayer>().clip.length));
   }

   private IEnumerator OnFadeInAvatar(float _lenght)
   {
      float currentTime = 0f;
      while (currentTime < _lenght)
      {
         currentTime += Time.deltaTime;
         yield return null;
      }
      
      StartCoroutine(Tools.Fade(0, 1, 2f, _avatar, OnLaunch_Home));
      _avatar.gameObject.SetActive(false);
   }

   private void OnLaunch_Home()
   {
      _home.Launch();
   }
}
