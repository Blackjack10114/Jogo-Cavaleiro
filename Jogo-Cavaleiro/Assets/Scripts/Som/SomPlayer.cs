using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SomPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Clips de Efeito")]
    public AudioClip somTrocarLinha;
    public AudioClip somAtaque;
    public AudioClip somDano;
    public AudioClip somCura;
    public AudioClip somMorte;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void TocarSom(AudioClip clip)
    {
        if (clip == null || audioSource == null)
            return;

        // Reproduz via AudioManager para que o volume seja controlado pelo mixer
        Controlador_Som.instancia?.TocarSFX(clip);
    }

    // Métodos de atalho
    public void TocarTrocarLinha() => TocarSom(somTrocarLinha);
    public void TocarAtaque() => TocarSom(somAtaque);
    public void TocarDano() => TocarSom(somDano);
    public void TocarCura() => TocarSom(somCura);
    public void TocarMorte() => TocarSom(somMorte);
}
