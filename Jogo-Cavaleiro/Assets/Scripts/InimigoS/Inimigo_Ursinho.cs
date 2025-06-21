using UnityEngine;
using System.Collections;

public class Inimigo_Ursinho : MonoBehaviour
{
    // coisas do laço
    public float chancelaco = 0.3f;
    public bool comlaco;
    private bool lacoinsta;
    private GameObject prefabLaco;
    public float AutoDestruircomlaco;
    [Header("Movimento")]
    public float velocidadeBase = 2f;
    public float velocidadeAcelerada = 4f;
    public float distanciaParaAcelerar = 3f;
    public float offsetPosicao = 1.2f; // onde o ursinho tenta ficar acima do jogador
    public float toleranciaAlvo = 0.1f;
    public float distanciaMinimaAproximacao = 1.5f; // distância mínima antes de parar de andar

    [Header("Ataque")]
    public int dano = 1;
    public float tempoEntreAtaques = 1.5f;
    public float alcanceAtaque = 0.6f;
    public float tempoparaatacar;

    [Header("Recuo após levar dano")]
    public float tempoRecuo = 0.15f;
    public float velocidadeRecuo = 10f;
    public float tempoParadoAposRecuo = 1f;

    private Transform jogador;
    private Vector3 PosicaoUrsinho;
    private bool podeatacar;
    private bool alinhando;
    private bool recuando = false;
    private bool podeMover = true;
    private float proximoAtaque = 0f;
    private bool distanciaDoJogador, MesmaAltura, Destruir;
    private bool pulando = false;


    private SpriteRenderer sr;

    void Start()
    {
        jogador = GameObject.FindWithTag("Player")?.transform;
        sr = GetComponent<SpriteRenderer>();
        if (Random.value < chancelaco)
        {
            comlaco = true;
        }
        prefabLaco = Resources.Load<GameObject>("Laco_Rosa");
        podeatacar = true;
    }

    void Update()
    {
        if (jogador == null) return;
        // instancia o laço
        if (comlaco && !lacoinsta)
        {
            GameObject laco = Instantiate(prefabLaco, transform.position, Quaternion.identity);
            lacoinsta = true;
            laco.transform.parent = this.transform;
        }

        // Verifica se está muito abaixo e não está pulando nem recuando
        if (!pulando && !recuando && transform.position.y < jogador.position.y - 1.5f)
        {
            //StartCoroutine(PularParaCimaDoJogador());
            return;
        }

        // Movimento normal
        // Vector3 destino = jogador.position + new Vector3(0, offsetPosicao, 0);
        float distX = Mathf.Abs(jogador.position.x - transform.position.x);
        float distY = Mathf.Abs(jogador.position.y - transform.position.y);
        distanciaDoJogador = distX <= 9;
        MesmaAltura = distY <= alcanceAtaque;
        // Ataque
        if (distanciaDoJogador && MesmaAltura && podeatacar && !comlaco)
        {
            podeatacar = false;
            StartCoroutine(Atacar());
        }
        if (distanciaDoJogador && MesmaAltura && !recuando)
        {
            AlinharYComJogador();
            if (comlaco && !Destruir)
            {
                Destruir = true;
                AutoDestruir();
            }
        }
        /* if (distanciaDoJogador < alcanceAtaque && Time.time >= proximoAtaque && comlaco)
        {
            StartCoroutine(AutoDestruir());
        }
        */
    }

    public void LevarDanoRecuo()
    {
        if (!recuando)
        {
            StartCoroutine(Recuar());
            if (sr != null) StartCoroutine(Piscar());
        }
    }

    private System.Collections.IEnumerator Recuar()
    {
        recuando = true;

        Vector3 direcaoContraria = Vector3.up;
        float tempo = tempoRecuo;

        while (tempo > 0f)
        {
            transform.position += direcaoContraria * velocidadeRecuo * Time.deltaTime;
            tempo -= Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(tempoParadoAposRecuo);
        recuando = false;
    }

    private System.Collections.IEnumerator Piscar()
    {
        Color original = sr.color;
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = original;
    }
   /* private System.Collections.IEnumerator PularParaCimaDoJogador()
    {
        pulando = true;
        podeMover = false;

        float tempo = 0.4f; // duração do pulo
        float alturaInicial = transform.position.y;
        float alturaAlvo = jogador.position.y + offsetPosicao;

        float velocidadeY = (alturaAlvo - alturaInicial) / tempo;

        float t = 0f;
        while (t < tempo)
        {
            transform.position += Vector3.up * velocidadeY * Time.deltaTime;
            t += Time.deltaTime;
            yield return null;
        }

        // Garante que parou na altura exata
        transform.position = new Vector3(transform.position.x, alturaAlvo, transform.position.z);

        podeMover = true;
        pulando = false;
    } */
    private IEnumerator AutoDestruir()
    {
        yield return new WaitForSeconds(AutoDestruircomlaco);
        Debug.Log("autodestruir");
        Destroy(gameObject);
    }
    private IEnumerator Atacar()
    {
        yield return new WaitForSeconds(tempoparaatacar);
        if (distanciaDoJogador && MesmaAltura)
        {
            Debug.Log("Atacando");
            Vida vida = jogador.GetComponent<Vida>();
            if (vida != null)
            {
                vida.LevarDano(dano);
                proximoAtaque = Time.time + tempoEntreAtaques;
            }
        }
        podeatacar = true;
    }
    private void AlinharYComJogador()
    {
        alinhando = true;
        if (!recuando)
        {
            if (jogador == null) return;
            PosicaoUrsinho = new Vector3(transform.position.x, jogador.transform.position.y, transform.position.z);
            transform.position = PosicaoUrsinho;
        }
        else return;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (comlaco) return;

        if (collision.CompareTag("Player"))
        {
            Vida vida = collision.GetComponent<Vida>();
            Vida vidaursinho = GetComponent<Vida>();
            if (vida != null)
            {
                vida.LevarDano(dano);
                vidaursinho.LevarDano(dano);
                StartCoroutine(Recuar());
            }
        }
    }
}
