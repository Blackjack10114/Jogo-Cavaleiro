using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuOpcoesSom : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider sliderMaster;
    public Slider sliderBGM;
    public Slider sliderSFX;

    void Start()
    {
        // Carregar preferências salvas
        sliderMaster.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        sliderBGM.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sliderSFX.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        AtualizarVolume("MasterVolume", sliderMaster.value);
        AtualizarVolume("BGMVolume", sliderBGM.value);
        AtualizarVolume("SFXVolume", sliderSFX.value);

        // Conectar callbacks
        sliderMaster.onValueChanged.AddListener((v) => OnSliderChange("MasterVolume", v));
        sliderBGM.onValueChanged.AddListener((v) => OnSliderChange("BGMVolume", v));
        sliderSFX.onValueChanged.AddListener((v) => OnSliderChange("SFXVolume", v));
    }

    private void OnSliderChange(string parametro, float valor)
    {
        AtualizarVolume(parametro, valor);
        PlayerPrefs.SetFloat(parametro, valor);
        PlayerPrefs.Save();
    }

    private void AtualizarVolume(string parametro, float valor)
    {
        // Evita log(0), e transforma valor linear (0-1) em decibel (-80 a 0)
        float volumeDB = Mathf.Log10(Mathf.Clamp(valor, 0.0001f, 1f)) * 20f;
        mixer.SetFloat(parametro, volumeDB);
    }
}
