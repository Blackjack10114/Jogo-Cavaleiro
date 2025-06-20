using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class Morcego : MonoBehaviour
{
    private Transform jogador;
    private GameObject Player;
    private Vector3 PosicaoMorcego;
    public float velocidade;
    private Vida vida;
    public float alcanceAtaque, AutoDestruircomlaco;
    public float tempoEntreAtaques = 1.5f;
    private float tempoProximoAtaque = 0f;
    public int dano = 1;
    public bool seguindo;
    // coisas de laço
    public float chancelaco = 0.3f;
    public bool comlaco;
    private bool lacoinsta;
    private GameObject prefabLaco;
    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        jogador = Player.transform;
        vida = GetComponent<Vida>();
        seguindo = true;
        prefabLaco = Resources.Load<GameObject>("Laco_Rosa");
        // verificar se tem laço
        if (Random.value < chancelaco)
        {
            comlaco = true;
        }
    }
    private void Update()
    {
        if (comlaco && !lacoinsta)
        {
            GameObject laco = Instantiate(prefabLaco, transform.position, Quaternion.identity);
            lacoinsta = true;
            laco.transform.parent = this.transform;
        }
        if (jogador == null || vida == null || vida.Morreu) return;
        // movimento
        if (seguindo)
        {
            Vector3 direcao = new Vector3(jogador.position.x - transform.position.x, 0, 0).normalized;
            transform.position += direcao * velocidade * Time.deltaTime;
            PosicaoMorcego = new Vector3(transform.position.x, jogador.position.y, transform.position.z);
            transform.position = PosicaoMorcego;
        }
        // ataque
        if (!comlaco)
        {
            Vector2 distancia = jogador.position - transform.position;
            bool aoLadoNaMesmaAltura = Mathf.Abs(distancia.y) < 1f && Mathf.Abs(distancia.x) <= alcanceAtaque;
            if (Time.time >= tempoProximoAtaque)
            {
                if (aoLadoNaMesmaAltura)
                {
                    seguindo = false;
                    PosicaoMorcego = new Vector3(transform.position.x, Player.transform.position.y, transform.position.z);
                    this.transform.position = PosicaoMorcego;
                    StartCoroutine(Atacar());
                }
            }
        }
        else
        {
            Vector2 distancia = jogador.position - transform.position;
            bool aoLadoNaMesmaAltura = Mathf.Abs(distancia.y) < 1f && Mathf.Abs(distancia.x) <= alcanceAtaque;
            if (aoLadoNaMesmaAltura)
            {
                seguindo = false;
                PosicaoMorcego = new Vector3(transform.position.x, Player.transform.position.y, transform.position.z);
                this.transform.position = PosicaoMorcego;
                StartCoroutine(AutoDestruir());
            }
        }
    }
    private IEnumerator Atacar()
    {
        yield return new WaitForSeconds(1f);
        tempoProximoAtaque = Time.time + tempoEntreAtaques;

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
