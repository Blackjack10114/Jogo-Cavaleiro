using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeUI : MonoBehaviour
{
    public string mixerParameter = "MasterVolume";
    public Slider slider;

    private void Awake()
    {
        if (slider == null)
            slider = GetComponent<Slider>();

        float valorSalvo = PlayerPrefs.GetFloat(mixerParameter, 1f);
        slider.value = valorSalvo;
        AplicarVolume(valorSalvo);
        slider.onValueChanged.AddListener(AplicarVolume);
    }

    private void AplicarVolume(float valor)
    {
        float db = Mathf.Log10(Mathf.Clamp(valor, 0.001f, 1f)) * 20f;

        if (Controlador_Som.instancia != null)
            Controlador_Som.instancia.AudioMixer.SetFloat(mixerParameter, db);

        PlayerPrefs.SetFloat(mixerParameter, valor);
        PlayerPrefs.Save();
    }
}
