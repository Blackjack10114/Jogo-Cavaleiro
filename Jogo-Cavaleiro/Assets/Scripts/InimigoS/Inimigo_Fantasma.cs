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
    public float duracaoDoFade = 1.0f;
    private Color corOriginal;
    [HideInInspector] public bool podeatacar;

    private float tempoProximoAtaque = 0f;
    private Transform playertransform;
    GameObject Player;
    private SpriteRenderer sr;
    private Vector3 PosicaoFantasma;
    private Vida vida;
    public float tempoparaatacar;
    // coisas do laço
    public float chancelaco = 0.3f;
    public bool comlaco;
    private bool lacoinsta;
    private GameObject prefabLaco;
    public float AutoDestruircomlaco;
    [HideInInspector] public bool Comecarfade;
    private bool estaFadeando = false;

    public enum DirecaoSpawn { Cima, Baixo }
    public DirecaoSpawn direcaoSpawn = DirecaoSpawn.Cima;
    private void Start()
    {
        prefabLaco = Resources.Load<GameObject>("Laco_Rosa");
        playertransform = GameObject.FindWithTag("Player")?.transform;
        Player = GameObject.FindWithTag("Player");
        // Usa LinhasController para posicionar na linha correta
        float x = LinhasController.Instance.PosicaoX(linhaAtual);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
        vida = GetComponent<Vida>();
        // verificar se tem laço
        if (Random.value < chancelaco)
        {
            comlaco = true;
        }
        sr = GetComponent<SpriteRenderer>();
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

        Vector2 distancia = playertransform.position - transform.position;
        bool aoLadoNaMesmaAltura = Mathf.Abs(distancia.y) < 1f && Mathf.Abs(distancia.x) <= alcanceAtaque;
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
        if (-distancia.y <  distanciadisaparece && !aoLadoNaMesmaAltura && !estaFadeando)
        {
            StartCoroutine(FadeOut());
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
        if (!comlaco)
        {
           if (aoLadoNaMesmaAltura)
           {
                if (estaFadeando)
                {
                    StopAllCoroutines();
                    estaFadeando = false;
                    Comecarfade = false;
                }
                sr.color = new Color(corOriginal.r, corOriginal.g, corOriginal.b, 1f);
             podeatacar = true;
              if (podeatacar)
              {
                 gameObject.layer = LayerMask.NameToLayer("Inimigo");
                 PosicaoFantasma = new Vector3(transform.position.x, Player.transform.position.y, transform.position.z);
                 this.transform.position = PosicaoFantasma;
                 StartCoroutine(Atacar());
              }
           }
        }
        else
        {
            if (aoLadoNaMesmaAltura)
            {
                if (estaFadeando)
                {
                    StopAllCoroutines();
                    estaFadeando = false;
                    Comecarfade = false;
                }
                sr.color = new Color(corOriginal.r, corOriginal.g, corOriginal.b, 1f);
                podeatacar = true;
                if (podeatacar)
                {
                    gameObject.layer = LayerMask.NameToLayer("Inimigo");
                    PosicaoFantasma = new Vector3(transform.position.x, Player.transform.position.y, transform.position.z);
                    this.transform.position = PosicaoFantasma;
                    StartCoroutine(AutoDestruir());
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !comlaco)
        {
            Vida vidaJogador = collision.GetComponent<Vida>();
            if (vidaJogador != null)
            {
                vidaJogador.LevarDano(dano);
                Destroy(gameObject);
            }
        }
        else return;
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
    IEnumerator FadeOut()
    {
        Comecarfade = true;
        estaFadeando = true;
        sr = GetComponent<SpriteRenderer>();
        corOriginal = sr.color;

        float tempo = 0f;

        while (tempo < duracaoDoFade)
        {
            float alpha = Mathf.Lerp(1f, 0f, tempo / duracaoDoFade);
            sr.color = new Color(corOriginal.r, corOriginal.g, corOriginal.b, alpha);

            tempo += Time.deltaTime;
            yield return null;
        }

        sr.color = new Color(corOriginal.r, corOriginal.g, corOriginal.b, 0f);
    }
    private IEnumerator AutoDestruir()
    {
        yield return new WaitForSeconds(AutoDestruircomlaco);
        Destroy(gameObject);
    }
}
