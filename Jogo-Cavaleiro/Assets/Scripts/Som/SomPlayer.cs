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
        // Tenta obter o componente existente
        audioSource = GetComponent<AudioSource>();

        // Se não tiver, cria um e avisa
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            Debug.LogWarning($"AudioSource faltando em {gameObject.name}. Um novo foi adicionado automaticamente.");
        }

        // Garante que não toque automaticamente
        audioSource.playOnAwake = false;
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
