using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInformer : MonoBehaviour
{
    [SerializeField] private AnimationsController _animationsController;
    public CinematicState cinematicState;
    
  public void SetCinematicState()
  { 
    _animationsController.SetAnimator(cinematicState);  
  }
}

public enum CinematicState
{
    Intro,
    Paneo,
    Work,
    Avatar
}
