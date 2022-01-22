using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TitleUI : MonoBehaviour
{

    [SerializeField] Volume _ppVolume;

    [SerializeField] Slider _brightnessSlider;
    
    // Start is called before the first frame update
    void Start()
    {
        _ppVolume.profile.TryGet<ColorAdjustments>(out ColorAdjustments colorValues);


        _brightnessSlider.value = 1;

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BrightnessSlider()
    {
        _ppVolume.profile.TryGet<ColorAdjustments>(out ColorAdjustments colorValues);

        

        colorValues.colorFilter.value = Color.white * _brightnessSlider.value;

        GameManager.Instance.brightness = _brightnessSlider.value;

    }
}
