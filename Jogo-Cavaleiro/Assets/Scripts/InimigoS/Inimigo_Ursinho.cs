using UnityEngine;

public class Inimigo_Ursinho : MonoBehaviour
{
    public float velocidade = 2f;
    public float distanciaRecuo = 2f;
    public float tempoEntreMovimentos = 0.3f;
    public int dano = 1;

    private Transform jogador;
    private bool recuando = false;
    private Vector3 direcaoAtual;
    private Rigidbody2D rb;
    private bool podeMover = true;

    public float tempoEntreAtaques = 1.5f;
    private float proximoAtaque = 0f;
    public float alcanceAtaque = 0.5f;


    void Start()
    {
        jogador = GameObject.FindWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!podeMover || jogador == null) return;

        if (!recuando)
        {
            direcaoAtual = (jogador.position - transform.position).normalized;
            transform.position += direcaoAtual * velocidade * Time.deltaTime;
        }

        // Ataque lento
        float distancia = Vector2.Distance(transform.position, jogador.position);
        if (distancia < alcanceAtaque && Time.time >= proximoAtaque)
        {
            Vida vida = jogador.GetComponent<Vida>();
            if (vida != null)
            {
                vida.LevarDano(dano);
                proximoAtaque = Time.time + tempoEntreAtaques;
            }
        }
    }


    public void LevarDanoRecuo()
    {
        if (!recuando)
            StartCoroutine(Recuar());
    }

    private System.Collections.IEnumerator Recuar()
    {
        recuando = true;
        podeMover = false;

        Vector3 direcaoContraria = -(jogador.position - transform.position).normalized;
        float tempo = 0.15f;
        float velocidadeRecuo = 10f;

        while (tempo > 0f)
        {
            transform.position += direcaoContraria * velocidadeRecuo * Time.deltaTime;
            tempo -= Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(tempoEntreMovimentos);
        podeMover = true;
        recuando = false;
    }

    }

