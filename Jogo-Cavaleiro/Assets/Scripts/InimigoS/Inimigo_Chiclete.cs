using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Chiclete : MonoBehaviour
{
    int dano = 1;

    public float distanciaMaximaPlayer = 20f;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Qualquer colis�o: " + collision.name);
        if (collision.CompareTag("Player"))
        {
            Vida vida = collision.GetComponent<Vida>();
            if (vida != null)
            {
                vida.LevarDano(dano);
                Debug.Log(collision.name + "Colidiu Chiclete");
            }
        }
    }
}
