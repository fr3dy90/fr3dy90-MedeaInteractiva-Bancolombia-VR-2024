using UnityEngine;

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
      StartCoroutine(Tools.Fade(0, 1, 2f, _avatar, OnFadeInAvatar));
      _avatar.gameObject.SetActive(true);
   }

   private void OnFadeInAvatar()
   {
      
   }
}
