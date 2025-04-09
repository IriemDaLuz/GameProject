using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LogicalVolumen : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public RawImage imageMute;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumeAudio",0.5f);
        AudioListener.volume = slider.value;
        CheckIfMuted();
    }
    public void ChangeSlider(float value){
        sliderValue=value;
        PlayerPrefs.SetFloat("volumeAudio", sliderValue);
        AudioListener.volume = slider.value;
        CheckIfMuted();
    }

    public void CheckIfMuted(){
        if(sliderValue==0f){
            imageMute.enabled=true;
        }
        else{
            imageMute.enabled=false;
        }
    }

    void Update()
    {
        
    }
}
