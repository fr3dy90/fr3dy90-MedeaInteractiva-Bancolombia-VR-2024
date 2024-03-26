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
   
   [SerializeField] private VideoPlayer _videoPlayer;
   [SerializeField] private LayerMask _layerAvatar;

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

      _videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "AvatarIntro.mp4");
   }

   public void OnLaunchXperience()
   {
      CameraController.Instance.Init(actualPlatform, OnPlayerRready);
      
      _gvrEditorEmulator.enabled = false;
      _welcome.SetActive(true);
   }

   private void OnPlayerRready()
   {
      StartCoroutine(SetLayer());
      _avatar.gameObject.SetActive(true);
      _videoPlayer.Play();
      StartCoroutine(OnFadeInAvatar(28f));
      //StartCoroutine(OnFadeInAvatar((float)_avatar.GetComponentInChildren<VideoPlayer>().clip.length));
   }

   private IEnumerator SetLayer()
   {
      yield return new WaitForSeconds(.3f);
      _videoPlayer.gameObject.layer = _layerAvatar;
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
