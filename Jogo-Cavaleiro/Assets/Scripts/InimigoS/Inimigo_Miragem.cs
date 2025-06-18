using UnityEngine;

public class Inimigo_Miragem : MonoBehaviour
{
    private GameObject Player;
    private Vector3 posicaomiragem;
    public int dano = 1;
    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        posicaomiragem = new Vector3(Player.transform.position.x, transform.position.y, transform.position.z);
        this.transform.position = posicaomiragem;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vida vidaJogador = collision.GetComponent<Vida>();
            if (vidaJogador != null)
            {
                vidaJogador.LevarDano(dano);
                Destroy(gameObject);
            }
        }
    }
}
