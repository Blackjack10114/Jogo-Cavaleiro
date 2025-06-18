using UnityEngine;

public class Inimigo_Cavaleiro : MonoBehaviour
{
    public float velocidade = 10f;
    public int dano = 1;
    private Transform jogador;

    void Start()
    {
        jogador = GameObject.FindWithTag("Player")?.transform;
    }

    void Update()
    {
        transform.Translate(Vector3.up * velocidade * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vida vida = other.GetComponent<Vida>();
            if (vida != null)
            {
                vida.LevarDano(dano);
                Destroy(gameObject);
            }
        }
    }
}