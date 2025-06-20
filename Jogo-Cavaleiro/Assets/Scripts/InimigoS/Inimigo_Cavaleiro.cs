using UnityEngine;
using System.Collections;

public class Inimigo_Cavaleiro : MonoBehaviour
{
    public float velocidadeInicial = 10f;
    public float velocidadeAcompanhamento = 0f; // mesma do jogador
    public float alcanceVerticalParaAcompanhar = 3f;
    public float distanciaTrocaLinha = 0.1f; // margem para detectar mesma linha

    public int dano = 1;
    private Transform jogador;
    private PlayerMov playerMov;

    private bool acompanhando = false;
    private Vector3 distanciaLinha = new Vector3(8f, 0f, 0f); // usado na troca de linha (opcional)

    void Start()
    {
        jogador = GameObject.FindWithTag("Player")?.transform;
        playerMov = jogador?.GetComponent<PlayerMov>();
    }

    void Update()
    {
        if (jogador == null) return;

        float distanciaVertical = jogador.position.y - transform.position.y;

        if (Mathf.Abs(distanciaVertical) <= 0.2f) // mesma altura (com tolerância)
        {
            acompanhando = true;
            if (playerMov != null)
                velocidadeAcompanhamento = playerMov.velocidade;
        }
        else
        {
            acompanhando = false;
        }


        if (acompanhando)
        {
            // Sobe junto com o jogador
            transform.Translate(Vector3.up * velocidadeAcompanhamento * Time.deltaTime);
            StartCoroutine(Atacar());
        }
        else
        {
            // Corre em direção ao jogador
            transform.Translate(Vector3.up * velocidadeInicial * Time.deltaTime);
        }

        
        //Troca de linha se estiver na mesma linha
        if (Mathf.Abs(jogador.position.x - transform.position.x) < distanciaTrocaLinha)
        {
            // Troca para outra linha
            float novoX = transform.position.x;

            // Se estiver na direita, vai pro centro. Se no centro, vai pra esquerda, etc.
            if (Mathf.Approximately(transform.position.x, LinhasController.Instance.PosicaoX(LinhasController.Linha.Direita)))
                novoX = LinhasController.Instance.PosicaoX(LinhasController.Linha.Centro);
            else if (Mathf.Approximately(transform.position.x, LinhasController.Instance.PosicaoX(LinhasController.Linha.Centro)))
                novoX = LinhasController.Instance.PosicaoX(LinhasController.Linha.Esquerda);
            else
                novoX = LinhasController.Instance.PosicaoX(LinhasController.Linha.Centro);

            transform.position = new Vector3(novoX, transform.position.y, transform.position.z);
        }
       
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
    private IEnumerator Atacar()
    {
        yield return new WaitForSeconds(1f);
        Vida vidaJogador = jogador.GetComponent<Vida>();
        if (vidaJogador != null)
        {
            vidaJogador.LevarDano(dano);
            Destroy(gameObject);
        }
    }
}
