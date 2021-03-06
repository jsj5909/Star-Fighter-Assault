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
    [SerializeField] Slider _volumeSlider;

    [SerializeField] GameObject _optionsPanel;


    [SerializeField] private AudioSource _audioSoundEffects;
    [SerializeField] private AudioSource _audioMusic;
    [SerializeField] private AudioClip _music;
    [SerializeField] private AudioClip _clickSound;
    
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

    public void OptionsPressed()
    {
        
       
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

    public void VolumeSlider()
    {
        GameManager.Instance.volume = _volumeSlider.value;

        _audioMusic.volume = GameManager.Instance.volume / 5;
        _audioSoundEffects.volume = GameManager.Instance.volume;
    }

    IEnumerator ShortDelay()
    {
        yield return new WaitForSeconds(0.5f);
        _optionsPanel.SetActive(true);
    }

    public void PlayClick()
    {
        _audioSoundEffects.PlayOneShot(_clickSound);
    }
}
