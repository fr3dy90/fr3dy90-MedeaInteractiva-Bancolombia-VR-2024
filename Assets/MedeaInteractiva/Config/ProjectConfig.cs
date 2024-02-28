using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public  class ProjectConfig : MonoBehaviour
{
    public static ProjectConfig Instance;
    public Platform actualPlatform;
    
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
}

public enum Platform
{
    WebGl,
    Android,
    Oculus
}
