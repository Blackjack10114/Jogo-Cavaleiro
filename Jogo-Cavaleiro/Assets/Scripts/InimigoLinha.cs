using UnityEngine;

public class InimigoLinha : MonoBehaviour
{
    public LinhasController.Linha linhaAtual;
    public float alcanceAtaque;
    public float tempoEntreAtaques = 1.5f;
    public int dano = 1;

    private float tempoProximoAtaque = 0f;
    private Transform player;
    private Vida vida;

    public enum DirecaoSpawn { Cima, Baixo }
    public enum DirecaoMovimento { Subindo, Descendo}
    public DirecaoSpawn direcaoSpawn = DirecaoSpawn.Cima;

    public DirecaoMovimento direcao;
    private void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;

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
    }

    private void Update()
    {
        if (player == null || vida == null || vida.Morreu) 
        {
            Debug.Log("semata");
        }
        

        Vector2 distancia = player.position - transform.position;

        bool naMesmaLinhaVertical = Mathf.Abs(distancia.x) < 0.5f;
        bool aoLadoNaMesmaAltura = Mathf.Abs(distancia.y) < 1f && Mathf.Abs(distancia.x) < alcanceAtaque;

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
        if (aoLadoNaMesmaAltura)
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
}
