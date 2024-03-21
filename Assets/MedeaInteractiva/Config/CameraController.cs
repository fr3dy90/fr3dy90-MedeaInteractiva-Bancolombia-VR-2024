using System;
using Gvr.Internal;
using UnityEngine;

public  class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [Header("CameraComponents")] 
    [SerializeField] private GvrPointerPhysicsRaycaster _gvrPointerPhysicsRaycaster;
    [SerializeField] private GvrReticlePointer _gvrReticlePointer;
    [SerializeField] private ReticlePointerController _reticlePointerController;
    [SerializeField] private FPCamera _fpCamera;
    [SerializeField] private GameObject _reticleCanvas;
    [SerializeField] private GameObject _reticleGameObject;
    
    [Header("Animation Entrance")]
    [SerializeField] private Animator _animatorController;
    private const string _animatorTrigger = "LaunchAnimation";
    
    [Header("Camera Positions")]
    [SerializeField] private CameraPosAndRot _initialCameraPosition;
    [SerializeField] private CameraPosAndRot _lastCameraPosition;
    [SerializeField] private Transform cameraParent;

    private Action OnComplete;
    
    [ContextMenu("SetCamInit")]
    
    
    
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
        
        if (_animatorController == null)
        {
            _animatorController = GetComponent<Animator>();
        }
        
       
    }

    public void SetCameraComponents(Platform _actualPlatform)
    {
        switch (_actualPlatform)
        {
            case Platform.WebGl:
                _gvrPointerPhysicsRaycaster.enabled = false;
                _gvrReticlePointer.enabled = false;
                _reticlePointerController.enabled = false;
                _fpCamera.enabled = false;
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

    public void Init(Platform actualPlatform, Action onComplete = null)
    {
        SetCameraComponents(actualPlatform);
        SetCameraInitialPosition(CameraState.Start);
        _animatorController.SetTrigger(_animatorTrigger);
       OnComplete = onComplete;
    }

    private void SetCameraInitialPosition(CameraState actualCamState)
    {
        switch (actualCamState)
        {
            case CameraState.Start:
                cameraParent.position = _initialCameraPosition.position;
                cameraParent.rotation = _initialCameraPosition.rotation;
                break;
            case CameraState.End:
                cameraParent.position = _lastCameraPosition.position;
                cameraParent.rotation = _lastCameraPosition.rotation;
                break;
        }
    }
    
    public void OnIntroFinished()
    {
        SetCameraInitialPosition(CameraState.End);
        OnComplete?.Invoke();
        OnComplete = null;
    }

    private void SetCamInit()
    {
        SetCameraInitialPosition(CameraState.Start);
    }
}

public enum Platform
{
    WebGl,
    Android,
    Oculus
}

public enum CameraState
{
    Start,
    End
}

[System.Serializable]
public struct CameraPosAndRot
{
    public Vector3 position;
    public Quaternion rotation;
}
