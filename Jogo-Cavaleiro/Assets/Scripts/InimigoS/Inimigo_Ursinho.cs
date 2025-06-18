using UnityEngine;

public class Inimigo_Ursinho : MonoBehaviour
{
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

    [Header("Recuo após levar dano")]
    public float tempoRecuo = 0.15f;
    public float velocidadeRecuo = 10f;
    public float tempoParadoAposRecuo = 1f;

    private Transform jogador;
    private bool recuando = false;
    private bool podeMover = true;
    private float proximoAtaque = 0f;

    private bool pulando = false;


    private SpriteRenderer sr;

    void Start()
    {
        jogador = GameObject.FindWithTag("Player")?.transform;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (jogador == null || !podeMover) return;

        // Verifica se está muito abaixo e não está pulando nem recuando
        if (!pulando && !recuando && transform.position.y < jogador.position.y - 1.5f)
        {
            StartCoroutine(PularParaCimaDoJogador());
            return;
        }

        // Movimento normal
        Vector3 destino = jogador.position + new Vector3(0, offsetPosicao, 0);
        float distanciaDoJogador = Vector2.Distance(transform.position, jogador.position);
        float distanciaAteDestino = Vector2.Distance(transform.position, destino);

        float velocidade = distanciaDoJogador <= distanciaParaAcelerar ? velocidadeAcelerada : velocidadeBase;

        if (!recuando && distanciaAteDestino > distanciaMinimaAproximacao)
        {
            transform.position = Vector3.MoveTowards(transform.position, destino, velocidade * Time.deltaTime);
        }

        // Ataque
        if (distanciaDoJogador < alcanceAtaque && Time.time >= proximoAtaque)
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
        {
            StartCoroutine(Recuar());
            if (sr != null) StartCoroutine(Piscar());
        }
    }

    private System.Collections.IEnumerator Recuar()
    {
        recuando = true;
        podeMover = false;

        Vector3 direcaoContraria = Vector3.up;
        float tempo = tempoRecuo;

        while (tempo > 0f)
        {
            transform.position += direcaoContraria * velocidadeRecuo * Time.deltaTime;
            tempo -= Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(tempoParadoAposRecuo);

        podeMover = true;
        recuando = false;
    }

    private System.Collections.IEnumerator Piscar()
    {
        Color original = sr.color;
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = original;
    }
    private System.Collections.IEnumerator PularParaCimaDoJogador()
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
    }

}
