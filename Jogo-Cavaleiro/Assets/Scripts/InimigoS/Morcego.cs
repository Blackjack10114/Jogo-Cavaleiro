using System.Collections;
using UnityEngine;

public class Morcego : MonoBehaviour
{
    private Transform jogador;
    private GameObject Player;
    private Vector3 PosicaoMorcego;

    [Header("Movimento e Ataque")]
    public float velocidade;
    public float alcanceAtaque;
    public float tempoEntreAtaques = 1.5f;
    public int dano = 1;

    private float tempoProximoAtaque = 0f;
    private bool seguindo = true;
    private Vida vida;

    [Header("Laço Rosa")]
    public float chancelaco = 0.3f;
    public float AutoDestruircomlaco = 2.5f;
    public bool comlaco;
    private bool lacoinsta;
    [HideInInspector] public GameObject prefabLaco;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        jogador = Player?.transform;
        vida = GetComponent<Vida>();
        seguindo = true;

        prefabLaco = Resources.Load<GameObject>("Laco_Rosa");

        // Sorteia se o morcego nasce com laço
        if (Random.value < chancelaco)
            comlaco = true;
    }

    private void Update()
    {
        if (jogador == null || vida == null || vida.Morreu) return;

        // Instancia o laço (se aplicável)
        if (comlaco && !lacoinsta && prefabLaco != null)
        {
            GameObject laco = Instantiate(prefabLaco, transform.position, Quaternion.identity);
            laco.transform.parent = transform;
            lacoinsta = true;
        }

        // Movimento: acompanha X, alinha Y com o jogador
        if (seguindo)
        {
            Vector3 direcao = new Vector3(jogador.position.x - transform.position.x, 0, 0).normalized;
            transform.position += direcao * velocidade * Time.deltaTime;

            // Fixa Y na mesma altura do jogador
            PosicaoMorcego = new Vector3(transform.position.x, jogador.position.y, transform.position.z);
            transform.position = PosicaoMorcego;
        }

        Vector2 distancia = jogador.position - transform.position;
        bool aoLadoNaMesmaAltura = Mathf.Abs(distancia.y) < 1f && Mathf.Abs(distancia.x) <= alcanceAtaque;

        if (!comlaco && Time.time >= tempoProximoAtaque && aoLadoNaMesmaAltura)
        {
            seguindo = false;
            AlinharYComJogador();
            StartCoroutine(Atacar());
        }
        else if (comlaco && aoLadoNaMesmaAltura)
        {
            seguindo = false;
            AlinharYComJogador();
            StartCoroutine(AutoDestruir());
        }
    }

    private void AlinharYComJogador()
    {
        if (Player == null) return;
        PosicaoMorcego = new Vector3(transform.position.x, Player.transform.position.y, transform.position.z);
        transform.position = PosicaoMorcego;
    }

    private IEnumerator Atacar()
    {
        yield return new WaitForSeconds(1f);
        tempoProximoAtaque = Time.time + tempoEntreAtaques;

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
