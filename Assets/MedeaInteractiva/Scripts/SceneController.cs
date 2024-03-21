using UnityEngine;

public class SceneController : MonoBehaviour
{
   [Header("Singleton")]
   public static SceneController Instance;

   [Header("Target Platform")]
   public Platform actualPlatform;

   
   [SerializeField] private CanvasGroup _fade;
   [SerializeField] private AnimationCurve _curve;
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
      
   }
}
