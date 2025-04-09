using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LogicaBrillo : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image panelBrillo;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("brillo", 0.5f);
        panelBrillo.color= new Color(panelBrillo.color.r,panelBrillo.color.g,panelBrillo.color.b, slider.value);
    }

    public void ChangeSlider(float value){
        sliderValue=value;
        PlayerPrefs.SetFloat("brillo", sliderValue);
        panelBrillo.color= new Color(panelBrillo.color.r,panelBrillo.color.g,panelBrillo.color.b, slider.value);
    }
}
