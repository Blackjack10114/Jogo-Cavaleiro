using UnityEngine;
using UnityEngine.Audio;

public class SomPlayer : MonoBehaviour
{
    [Header("Configuração")]
    public AudioSource source;
    public AudioMixerGroup sfxGroup;

    [Header("Clipes")]
    public AudioClip somAtaque;
    public AudioClip somDano;
    public AudioClip somCura;
    public AudioClip somTrocarLinha;

    private void Awake()
    {
        if (source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = sfxGroup;
            source.playOnAwake = false;
        }
    }

    public void TocarSom(AudioClip clip, float volume = 1f)
    {
        if (clip != null && source != null)
            source.PlayOneShot(clip, volume);
    }


    public void TocarAtaque() => TocarSom(somAtaque, 0.5f);
    public void TocarDano() => TocarSom(somDano, 0.5f);
    public void TocarCura() => TocarSom(somCura);
    public void TocarTrocarLinha() => TocarSom(somTrocarLinha);
}
