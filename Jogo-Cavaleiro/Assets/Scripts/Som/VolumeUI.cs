using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeUI : MonoBehaviour
{
    public string mixerParameter;
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
        float db;

        // Evita logaritmo de zero e define um mínimo de volume audível
        if (valor <= 0.0001f)
            db = -80f; // Silêncio total (padrão do Unity)
        else
            db = Mathf.Log10(valor) * 20f;

        if (Controlador_Som.instancia != null)
            Controlador_Som.instancia.AudioMixer.SetFloat(mixerParameter, db);

        PlayerPrefs.SetFloat(mixerParameter, valor);
        PlayerPrefs.Save();
    }

}
