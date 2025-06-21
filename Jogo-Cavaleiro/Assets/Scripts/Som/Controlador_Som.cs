using UnityEngine;
using UnityEngine.Audio;

public class Controlador_Som : MonoBehaviour
{
    public static Controlador_Som instancia;

    [Header("Mixer")]
    public AudioMixer audioMixer;

    [Header("Fontes de Áudio")]
    public AudioSource musicaSource;
    public AudioSource sfxSource;

    private void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
            return;
        }
        instancia = this;
        DontDestroyOnLoad(gameObject);

        // Valida referências
        if (audioMixer == null)
            Debug.LogWarning("AudioMixer não atribuído em Controlador_Som.");
        if (musicaSource == null || sfxSource == null) //|| vozSource == null
            Debug.LogWarning("Alguma AudioSource não foi configurada em Controlador_Som.");

        AplicarVolumesSalvos();
    }

    public void TocarMusica(AudioClip clip, bool loop = true)
    {
        if (clip == null || musicaSource == null)
            return;

        if (musicaSource.clip == clip && musicaSource.isPlaying)
            return;

        StartCoroutine(FadeOutAndPlay(musicaSource, clip, loop, 0.5f));
    }

    public void TocarSFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
            sfxSource.PlayOneShot(clip);
    }

    /*public void TocarVoz(AudioClip clip)
    {
        if (vozSource != null && clip != null)
            vozSource.PlayOneShot(clip);
    }
    */

    public void AplicarVolumesSalvos()
    {
        AplicarVolume("MasterVolume");
        AplicarVolume("BGMVolume");
        AplicarVolume("SFXVolume");
    }

    public void AplicarVolume(string parametro)
    {
        if (audioMixer == null)
            return;

        float valor = PlayerPrefs.GetFloat(parametro, 1f);
        float db = Mathf.Log10(Mathf.Clamp(valor, 0.001f, 1f)) * 20f;
        audioMixer.SetFloat(parametro, db);
    }

    private System.Collections.IEnumerator FadeOutAndPlay(AudioSource source, AudioClip newClip, bool loop, float duration)
    {
        float startVol = source.volume;
        // Fade out
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            source.volume = Mathf.Lerp(startVol, 0f, t / duration);
            yield return null;
        }
        source.Stop();
        source.clip = newClip;
        source.loop = loop;
        source.time = 0f;
        source.Play();
        // Fade in
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            source.volume = Mathf.Lerp(0f, startVol, t / duration);
            yield return null;
        }
        source.volume = startVol;
    }
}
