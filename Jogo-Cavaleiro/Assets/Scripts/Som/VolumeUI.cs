using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeUI : MonoBehaviour
{
    [Header("Parâmetro do Audio Mixer")]
    public string mixerParameter = "MasterVolume";

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();

        // Carrega valor salvo
        float valorInicial = PlayerPrefs.GetFloat(mixerParameter, 1f);
        slider.value = valorInicial;

        // Aplica no Mixer
        AplicarVolume(valorInicial);

        // Escuta mudanças
        slider.onValueChanged.AddListener(AplicarVolume);
    }

    private void AplicarVolume(float valor)
    {
        if (Controlador_Som.instancia != null)
        {
            Controlador_Som.instancia.AplicarVolume(mixerParameter);
        }
        else
        {
            Debug.LogWarning("Controlador_Som.instancia não iniciado.");
        }

        // Salva preferência
        PlayerPrefs.SetFloat(mixerParameter, valor);
        PlayerPrefs.Save();
    }
}
