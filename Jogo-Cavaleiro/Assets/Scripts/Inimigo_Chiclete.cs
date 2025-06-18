using UnityEngine;

public class Chiclete : MonoBehaviour
{
    int dano = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vida vida = collision.GetComponent<Vida>();
            if (vida != null)
            {
                vida.LevarDano(dano);
                Debug.Log("Colidiu Chiclete");
            }
        }

    }

}
