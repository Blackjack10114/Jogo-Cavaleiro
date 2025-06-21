using UnityEngine;

public class VidaColetavel : MonoBehaviour
{
    public int valorCura = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vida vida = other.GetComponent<Vida>();
            if (vida != null && vida.VidaAtual() < vida.vidaMaxima)
            {
                vida.Curar(valorCura);
                SomPlayer som = other.GetComponent<SomPlayer>();
                if (som != null)
                    som.Tocar(som.somCura);
                Destroy(gameObject);
            }
        }
    }
}
