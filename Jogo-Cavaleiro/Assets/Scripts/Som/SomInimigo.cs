using UnityEngine;

public class SomInimigo : MonoBehaviour
{
    public AudioClip somMorte;

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        if (source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
        }
    }

    public void TocarSomMorte()
    {
        if (somMorte != null)
            source.PlayOneShot(somMorte);
    }
}
