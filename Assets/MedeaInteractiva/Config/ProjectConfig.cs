using UnityEngine;

public  class ProjectConfig : MonoBehaviour
{
    public static ProjectConfig Instance;
    public Platform actualPlatform;

    [Header("CameraComponents")] 
    [SerializeField] private GvrPointerPhysicsRaycaster _gvrPointerPhysicsRaycaster;
    [SerializeField] private GvrReticlePointer _gvrReticlePointer;
    [SerializeField] private ReticlePointerController _reticlePointerController;
    [SerializeField] private FPCamera _fpCamera;
    [SerializeField] private GameObject _reticleCanvas;
    [SerializeField] private GameObject _reticleGameObject;
    
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
        
        SetCameraComponents(actualPlatform);
    }

    void SetCameraComponents(Platform _actualPlatform)
    {
        switch (_actualPlatform)
        {
            case Platform.WebGl:
                _gvrPointerPhysicsRaycaster.enabled = false;
                _gvrReticlePointer.enabled = false;
                _reticlePointerController.enabled = false;
                _fpCamera.enabled = true;
                _reticleCanvas.SetActive(false);
                _reticleGameObject.SetActive(false);
                
                break;
            case Platform.Android:
                _gvrPointerPhysicsRaycaster.enabled = true;
                _gvrReticlePointer.enabled = true;
                _reticlePointerController.enabled = true;
                _fpCamera.enabled = true;
                _reticleCanvas.SetActive(true);
                _reticleGameObject.SetActive(true);
                break;
            case Platform.Oculus:
                
                break;
        }
    }
}

public enum Platform
{
    WebGl,
    Android,
    Oculus
}
