using UnityEngine;

[RequireComponent(typeof(Vida))]
public class InimigoTeste : MonoBehaviour
{
    public int dano = 1;
    public float raioDeteccaoLateral = 0.6f;
    public LayerMask layerJogador;

    private Vida vida;

    private void Start()
    {
        vida = GetComponent<Vida>();
    }

    private void Update()
    {
        // Detecta o jogador ao lado para atacar
        Collider2D col = Physics2D.OverlapCircle(transform.position, raioDeteccaoLateral, layerJogador);
        if (col != null)
        {
            float diferencaY = Mathf.Abs(transform.position.y - col.transform.position.y);
            float diferencaX = transform.position.x - col.transform.position.x;

            if (diferencaY < 1f && Mathf.Abs(diferencaX) > 0.1f)
            {
                col.GetComponent<Vida>()?.LevarDano(dano);
                Debug.Log("Inimigo atacou o jogador (lado)!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector2 posJogador = other.transform.position;
            if (posJogador.y < transform.position.y - 0.5f)
            {
                other.GetComponent<Vida>()?.LevarDano(dano);
                Debug.Log("Jogador encostou por baixo e levou dano!");
            }
        }
    }

    // Receber dano é responsabilidade do script Vida.cs
}
