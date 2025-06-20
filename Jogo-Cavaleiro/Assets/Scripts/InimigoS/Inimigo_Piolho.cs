using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;

public class Inimigo_Piolho : MonoBehaviour
{
    public LinhasController.Linha linhaAtual;
    public float alcanceAtaque;
    public float tempoEntreAtaques = 1.5f;
    public int dano = 1;
    private float tempoProximoAtaque = 0f;
    // coisas de laço
    public float chancelaco = 0.3f;
    public bool comlaco;
    private bool lacoinsta;
    private GameObject prefabLaco;

    private Transform playertransform;
    GameObject Player;
    private Vector3 PosicaoInimigo;
    private Vida vida;

    public enum DirecaoSpawn { Cima, Baixo }
    public enum DirecaoMovimento { Subindo, Descendo}
    public DirecaoSpawn direcaoSpawn = DirecaoSpawn.Cima;

    public DirecaoMovimento direcao;
    private void Start()
    {
        prefabLaco = Resources.Load <GameObject>("Laco_Rosa");
        playertransform = GameObject.FindWithTag("Player")?.transform;
        Player = GameObject.FindWithTag("Player");
        // Usa LinhasController para posicionar na linha correta
        float x = LinhasController.Instance.PosicaoX(linhaAtual);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);

        MovimentoVertical mov = GetComponent<MovimentoVertical>();
        if (mov != null)
        {
            mov.direcao = (direcao == DirecaoMovimento.Subindo) ?
                          MovimentoVertical.Direcao.Subindo :
                          MovimentoVertical.Direcao.Descendo;

            vida = GetComponent<Vida>();
        }
        // verificar se tem laço
        if (Random.value < chancelaco)
        {
            comlaco = true;
        }
    }

    private void Update()
    {
        if (playertransform == null || vida == null || vida.Morreu) return;
        // instancia o laço
        if (comlaco && !lacoinsta)
        {
            GameObject laco = Instantiate(prefabLaco, transform.position, Quaternion.identity);
            lacoinsta = true;
            laco.transform.parent = this.transform;
        }
        if (!comlaco)
        {
            Vector2 distancia = playertransform.position - transform.position;

            bool naMesmaLinhaVertical = Mathf.Abs(distancia.x) < 0.5f;
            bool aoLadoNaMesmaAltura = Mathf.Abs(distancia.y) < 1f && Mathf.Abs(distancia.x) <= alcanceAtaque;
            if (Time.time >= tempoProximoAtaque)
            {
                if (aoLadoNaMesmaAltura)
                {
                    PosicaoInimigo = new Vector3(transform.position.x, Player.transform.position.y, transform.position.z);
                    this.transform.position = PosicaoInimigo;
                    StartCoroutine(Atacar());
                }
            }
        }
        else
        {
            Vector2 distancia = playertransform.position - transform.position;
            if (distancia.y > 20)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (comlaco) return; // não ataca se estiver com laço

        if (collision.CompareTag("Player"))
        {
            Vida vida = collision.GetComponent<Vida>();
            if (vida != null)
            {
                vida.LevarDano(dano);
                Destroy(gameObject); // piolho se autodestrói após causar dano
            }
        }
    }

    private IEnumerator Atacar()
    {
        yield return new WaitForSeconds(1f);
        tempoProximoAtaque = Time.time + tempoEntreAtaques;

        Vida vidaJogador = playertransform.GetComponent<Vida>();
        if (vidaJogador != null)
        {
            vidaJogador.LevarDano(dano);
            Destroy(gameObject);
        }
    }
}
