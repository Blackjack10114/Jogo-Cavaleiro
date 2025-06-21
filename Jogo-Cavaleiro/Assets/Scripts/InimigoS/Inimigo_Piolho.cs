using UnityEngine;
using System.Collections;

public class Inimigo_Piolho : MonoBehaviour
{
    [Header("Configuração de linha")]
    public LinhasController.Linha linhaAtual;
    public float alcanceAtaque = 1.5f;
    public float tempoEntreAtaques = 1.5f;
    public int dano = 1;

    [Header("Laço Rosa")]
    public float chanceLaco = 0.3f;
    public bool comLaco;
    public float tempoAutoDestruirComLaco = 10f;

    
    
    public enum DirecaoSpawn { Cima, Baixo }
    public enum DirecaoMovimento { Subindo, Descendo }
    public DirecaoSpawn direcaoSpawn = DirecaoSpawn.Cima;
    public DirecaoMovimento direcao;

    private float tempoProximoAtaque = 0f;
    public float AtrasoAtaque;
    private bool lacoInstanciado = false;

    private GameObject prefabLaco;
    private Transform jogador;
    private Vector3 PosicaoPiolho;

    private Vida vida;
    private MovimentoVertical movimento;

    private void Start()
    {
        jogador = GameObject.FindWithTag("Player")?.transform;
        prefabLaco = Resources.Load<GameObject>("Laco_Rosa");

        float x = LinhasController.Instance.PosicaoX(linhaAtual);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);

        vida = GetComponent<Vida>();
        movimento = GetComponent<MovimentoVertical>();

        if (movimento != null)
        {
            movimento.direcao = (direcao == DirecaoMovimento.Subindo) ?
                                MovimentoVertical.Direcao.Subindo :
                                MovimentoVertical.Direcao.Descendo;
        }

        comLaco = Random.value < chanceLaco;
        if (comLaco)
        {
            StartCoroutine(AutoDestruirComLaco());
        }
    }

    private void Update()
    {
        if (jogador == null || vida == null || vida.Morreu) return;

        if (comLaco && !lacoInstanciado)
        {
            GameObject laco = Instantiate(prefabLaco, transform.position, Quaternion.identity);
            laco.transform.SetParent(this.transform);
            lacoInstanciado = true;
        }

        if (!comLaco)
        {
            float distX = Mathf.Abs(jogador.position.x - transform.position.x);
            float distY = Mathf.Abs(jogador.position.y - transform.position.y);

            bool alinhado = distX < 9f;
            bool dentroDoAlcance = distY <= alcanceAtaque;
            if (alinhado && dentroDoAlcance)
            {
                AlinharYComJogador();
            }
            if (alinhado && dentroDoAlcance && Time.time >= tempoProximoAtaque)
            {
                StartCoroutine(Atacar());
            }
        }
    }

    private IEnumerator Atacar()
    {
        tempoProximoAtaque = Time.time + tempoEntreAtaques;

        yield return new WaitForSeconds(AtrasoAtaque); // atraso do ataque

        if (jogador != null)
        {
            Vida vidaJogador = jogador.GetComponent<Vida>();
            if (vidaJogador != null)
            {
                vidaJogador.LevarDano(dano);
                Destroy(gameObject); // se autodestrói ao atacar
            }
        }
    }

    private IEnumerator AutoDestruirComLaco()
    {
        yield return new WaitForSeconds(tempoAutoDestruirComLaco);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (comLaco) return;

        if (collision.CompareTag("Player"))
        {
            Vida vida = collision.GetComponent<Vida>();
            if (vida != null)
            {
                vida.LevarDano(dano);
                Destroy(gameObject);
            }
        }
    }
    private void AlinharYComJogador()
    {
        if (jogador == null) return;
        PosicaoPiolho = new Vector3(transform.position.x, jogador.transform.position.y, transform.position.z);
        transform.position = PosicaoPiolho;
    }
}
