using UnityEngine;

public class SomPlayer : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip somTrocarLinha;
    public AudioClip somAtaque;
    public AudioClip somDano;
    public AudioClip somCura;
    public AudioClip somMorte;

    public void Tocar(AudioClip clip)
    {
        if (clip != null && audioSource != null)
            audioSource.PlayOneShot(clip);
    }
}
