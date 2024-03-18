using System;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public MyAnimations actualAnimation;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetTrigger(actualAnimation.ToString());
    }
}

public enum MyAnimations
{
    Typing,
    Sitting_Idle_Woman,
    Sitting_Idle_Man,
    Seated_Idle,
    Sitting_Talking,
    Sitting,
    Sitting_Talking_1,
    Standing_W_Briefcase_Idle,
    Texting_While_Standing
}
