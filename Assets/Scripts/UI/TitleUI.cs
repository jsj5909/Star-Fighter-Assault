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

    [SerializeField] GameObject _optionsPanel;


    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _music;
    [SerializeField] private AudioClip _clickSound;
    
    // Start is called before the first frame update
    void Start()
    {
        _ppVolume.profile.TryGet<ColorAdjustments>(out ColorAdjustments colorValues);


        _brightnessSlider.value = 1;


      


        if (_audio == null)
            Debug.LogError("Audio reference on Title UI is Null");

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OptionsPressed()
    {
        
        _audio.PlayOneShot(_clickSound);
        StartCoroutine(ShortDelay());
      
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

    IEnumerator ShortDelay()
    {
        yield return new WaitForSeconds(0.5f);
        _optionsPanel.SetActive(true);
    }
}
