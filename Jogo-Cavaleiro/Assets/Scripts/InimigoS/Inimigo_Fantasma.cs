using UnityEngine;
using System.Collections;
using UnityEngine.Assertions.Must;

public class Inimigo_Fantasma : MonoBehaviour
{
    public LinhasController.Linha linhaAtual;
    public float alcanceAtaque;
    public float tempoEntreAtaques = 1.5f;
    public int dano = 1;
    public int distanciadisaparece;

    private float tempoProximoAtaque = 0f;
    private Transform playertransform;
    GameObject Player;
    private Vector3 PosicaoFantasma;
    private Vida vida;
    public float tempoparaatacar;

    public enum DirecaoSpawn { Cima, Baixo }
    public DirecaoSpawn direcaoSpawn = DirecaoSpawn.Cima;
    private void Start()
    {
        playertransform = GameObject.FindWithTag("Player")?.transform;
        Player = GameObject.FindWithTag("Player");
        // Usa LinhasController para posicionar na linha correta
        float x = LinhasController.Instance.PosicaoX(linhaAtual);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
        vida = GetComponent<Vida>();
    }

    private void Update()
    {
        if (playertransform == null || vida == null || vida.Morreu) return;


        Vector2 distancia = playertransform.position - transform.position;
        bool aoLadoNaMesmaAltura = Mathf.Abs(distancia.y) < 1f && Mathf.Abs(distancia.x) <= alcanceAtaque;
        Debug.Log(-distancia.y);
        /* if (Time.time >= tempoProximoAtaque)
         {
             if (naMesmaLinhaVertical || aoLadoNaMesmaAltura)
             {               
                 tempoProximoAtaque = Time.time + tempoEntreAtaques;

                 Vida vidaJogador = player.GetComponent<Vida>();
                 if (vidaJogador != null)
                 {
                     vidaJogador.LevarDano(dano);
                     Destroy(gameObject);
                 }
             }
         }
        */
        if (-distancia.y <  distanciadisaparece && !aoLadoNaMesmaAltura)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        if (Time.time >= tempoProximoAtaque)
        {
            if (aoLadoNaMesmaAltura)
            {
                GetComponent<SpriteRenderer>().enabled = true;
                PosicaoFantasma = new Vector3(transform.position.x, Player.transform.position.y, transform.position.z);
                this.transform.position = PosicaoFantasma;
                StartCoroutine(Atacar());
            }
        }
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
    private IEnumerator Atacar()
    {
        yield return new WaitForSeconds(tempoparaatacar);
        tempoProximoAtaque = Time.time + tempoEntreAtaques;

        Vida vidaJogador = playertransform.GetComponent<Vida>();
        if (vidaJogador != null)
        {
            vidaJogador.LevarDano(dano);
            Destroy(gameObject);
        }
    }
}
