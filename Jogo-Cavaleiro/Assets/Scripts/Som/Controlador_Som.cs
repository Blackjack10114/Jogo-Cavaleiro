using UnityEngine;
using UnityEngine.Audio;

public class Controlador_Som : MonoBehaviour
{
    public static Controlador_Som instancia;

    [Header("Mixer")]
    public AudioMixer AudioMixer;

    [Header("Fontes de Áudio")]
    public AudioSource musicaSource;
    public AudioSource sfxSource;
    public AudioSource vozSource;

    private void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        instancia = this;
        DontDestroyOnLoad(gameObject);
        AplicarVolumesSalvos();
    }

    // Tocar música
    public void TocarMusica(AudioClip clip, bool loop = true)
    {
        if (musicaSource.clip == clip && musicaSource.isPlaying) return;

        musicaSource.loop = loop;
        musicaSource.clip = clip;
        musicaSource.time = 0f;
        musicaSource.Play();
    }

    // Tocar efeito sonoro
    public void TocarSFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
            sfxSource.PlayOneShot(clip);
    }

    // Tocar voz
    public void TocarVoz(AudioClip clip)
    {
        if (vozSource != null && clip != null)
            vozSource.PlayOneShot(clip);
    }

    //  Aplicar volumes salvos
    public void AplicarVolumesSalvos()
    {
        AplicarVolume("MasterVolume");
        AplicarVolume("BGMVolume");
        AplicarVolume("SFXVolume");
        AplicarVolume("VozesVolume");
    }

    private void AplicarVolume(string parametro)
    {
        float valor = PlayerPrefs.GetFloat(parametro, 1f);
        float db = Mathf.Log10(Mathf.Clamp(valor, 0.001f, 1f)) * 20f;
        AudioMixer.SetFloat(parametro, db);
    }
}
