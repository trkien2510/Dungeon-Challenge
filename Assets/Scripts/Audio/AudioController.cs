using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] Slider sliderBGM;
    [SerializeField] Slider sliderSFX;

    void Start()
    {
        sliderBGM.onValueChanged.AddListener(SetBGMVolume);
        float savedVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sliderBGM.value = savedVolume;
        SetBGMVolume(savedVolume);

        sliderSFX.onValueChanged.AddListener(SetSFXVolume);
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        sliderSFX.value = savedSFXVolume;
        SetSFXVolume(savedSFXVolume);
    }

    public void SetBGMVolume(float value)
    {
        AudioManager.Instance.SetBGMVolume(value);
        PlayerPrefs.SetFloat("BGMVolume", value);
    }

    public void SetSFXVolume(float value)
    {
        AudioManager.Instance.SetSFXVolume(value);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }
}
