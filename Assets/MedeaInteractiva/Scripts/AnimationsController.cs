using UnityEngine;

public class AnimationsController : MonoBehaviour
{
  [SerializeField] private Animator[] _animatorControllers;
  [SerializeField] private Transform _cameraParent;
  [SerializeField] private Animator _door;


  public void SetAnimator(CinematicState cinematicState)
  {
    switch (cinematicState)
    {
      case CinematicState.Intro:
        SetIntro();
        break;
      case CinematicState.Paneo:
        SetPaneo();
        break;
      case CinematicState.Work:
        SetWork();
        break;
      case CinematicState.Avatar:
        SetAvatar();
        break;
    }
  }
  
  private void SetIntro()
  {
    _cameraParent.parent = _animatorControllers[0].transform;
    _cameraParent.localPosition = Vector3.zero;
    _cameraParent.localRotation = Quaternion.identity;
    _animatorControllers[0].SetTrigger("Intro");
    _door.SetTrigger("Open");
  }

  private void SetPaneo()
  {
    _cameraParent.parent = _animatorControllers[1].transform;
    _cameraParent.localPosition = Vector3.zero;
    _cameraParent.localRotation = Quaternion.identity;
    _animatorControllers[1].SetTrigger("Paneo");
    _door.SetTrigger("Close");
  }
  
  private void SetWork()
  {
    _cameraParent.parent = _animatorControllers[2].transform;
    _cameraParent.localPosition = Vector3.zero;
    _cameraParent.localRotation = Quaternion.identity;
    _animatorControllers[2].SetTrigger("Work");
  }
  
  private void SetAvatar()
  {
    SceneController.Instance.ChangeScene(MomentScene.Avatar);    
  }
}
