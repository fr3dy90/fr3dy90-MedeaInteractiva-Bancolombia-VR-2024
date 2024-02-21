using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticlePointerController : MonoBehaviour
{
    private static ReticlePointerController instace;

    public Slider loadingSlider;
    public float maxSliderValue;
    public bool ready, loading;

    public static ReticlePointerController Instace { get => instace; set => instace = value; }

    void Awake()
    {
        Instace = this;
    }

    void Start()
    {
        loadingSlider.maxValue = maxSliderValue;
        loadingSlider.value = 0;
        ready = false;
        loading = false;
    }

    void Update()
    {
        if(loading && !ready){
            loadingSlider.value += Time.deltaTime;
            if (loadingSlider.value >= maxSliderValue){
                loading = false;
                ready = true;
                loadingSlider.value = 0;
            }
        }
    }

    public void StopLoading()
    {
        loading = false;
        ready = false;
        loadingSlider.value = 0;
    }    
}
