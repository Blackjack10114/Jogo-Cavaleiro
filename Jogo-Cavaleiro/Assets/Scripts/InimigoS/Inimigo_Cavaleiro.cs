using UnityEngine;
using System.Collections;

public class Inimigo_Cavaleiro : MonoBehaviour
{
    public float velocidadeInicial = 10f;
    public float velocidadeAcompanhamento = 0f;
    public float alcanceVerticalParaAcompanhar = 3f;
    public float distanciaTrocaLinha = 0.1f;
    public float distanciacavaleiroacima;

    public int dano = 1;
    private Transform jogador;
    private PlayerMov playerMov;
    private bool Distanciadiminuida, jaacompanhou;

    private bool acompanhando = false;
    private Vector3 distanciaLinha = new Vector3(8f, 0f, 0f);

    [Header("Laço")]
    [HideInInspector] public float chancelaco = 0.3f;
    [HideInInspector] public GameObject prefabLaco;
    [HideInInspector] public float AutoDestruircomlaco = 10f;
    public bool comlaco;
    private bool lacoinsta;

    void Start()
    {
        jogador = GameObject.FindWithTag("Player")?.transform;
        playerMov = jogador?.GetComponent<PlayerMov>();

        if (Random.value < chancelaco)
        {
            comlaco = true;
        }
    }

    void Update()
    {
        if (comlaco && !lacoinsta && prefabLaco != null)
        {
            GameObject laco = Instantiate(prefabLaco, transform.position, Quaternion.identity);
            laco.transform.parent = this.transform;
            lacoinsta = true;
        }

        if (jogador == null) return;

        float distanciaVertical = jogador.position.y - transform.position.y;
        Debug.Log(acompanhando);

        if (distanciaVertical <= -distanciacavaleiroacima)
        {
            if (!Distanciadiminuida)
            {
                distanciacavaleiroacima = distanciacavaleiroacima - 0.2f;
                Distanciadiminuida = true;
            }
            jaacompanhou = true;
            acompanhando = true;
            // velocidadeAcompanhamento = playerMov != null ? playerMov.velocidade : 0f;
            velocidadeAcompanhamento = 0;
        }
        else
        {
            if (!jaacompanhou)
            {
                acompanhando = false;
            }
            else return;
        }

        if (acompanhando)
        {
             transform.Translate(Vector3.up * velocidadeAcompanhamento * Time.deltaTime);

            /* if (!comlaco)
                StartCoroutine(Atacar());
            else
                StartCoroutine(AutoDestruir());
            */
        }
        else
        {
            transform.Translate(Vector3.up * velocidadeInicial * Time.deltaTime);
        }

        // Troca de linha
       /* if (Mathf.Abs(jogador.position.x - transform.position.x) < distanciaTrocaLinha)
        {
            float novoX = transform.position.x;

            if (Mathf.Approximately(transform.position.x, LinhasController.Instance.PosicaoX(LinhasController.Linha.Direita)))
                novoX = LinhasController.Instance.PosicaoX(LinhasController.Linha.Centro);
            else if (Mathf.Approximately(transform.position.x, LinhasController.Instance.PosicaoX(LinhasController.Linha.Centro)))
                novoX = LinhasController.Instance.PosicaoX(LinhasController.Linha.Esquerda);
            else
                novoX = LinhasController.Instance.PosicaoX(LinhasController.Linha.Centro);

            transform.position = new Vector3(novoX, transform.position.y, transform.position.z);
        }
       */
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (comlaco) return;

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
        if (jogador == null) yield break;

        Vida vidaJogador = jogador.GetComponent<Vida>();
        if (vidaJogador != null)
        {
            vidaJogador.LevarDano(dano);
            Destroy(gameObject);
        }
    }

    private IEnumerator AutoDestruir()
    {
        yield return new WaitForSeconds(AutoDestruircomlaco);
        Destroy(gameObject);
    }
}
