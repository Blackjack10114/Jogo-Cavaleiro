using UnityEngine;

public class SpawnerPiolho : MonoBehaviour
{
    [Header("Configuração do Spawner")]
    public GameObject prefabInimigo;
    public Transform jogador;
    public float intervaloEntreSpawns = 1.5f;
    private float tempoProximoSpawn = 0f;
    public int minimoPorSpawn = 1;
    public int maximoPorSpawn = 3;
    public float chanceSpawn = 0.5f; // 50%
    public float distanciaVertical = 10f;

    [Header("Layer")]
    public LayerMask layerInimigos;
    void Update()
    {
        if (Time.time >= tempoProximoSpawn && jogador != null)
        {
            if (Random.value < chanceSpawn)
            {
                SpawnProximoDoJogador();
            }

            tempoProximoSpawn = Time.time + intervaloEntreSpawns;
        }
    }

    void SpawnProximoDoJogador()
    {
        int quantidade = Random.Range(minimoPorSpawn, maximoPorSpawn + 1);

        for (int i = 0; i < quantidade; i++)
        {
            // linha aleatória
            LinhasController.Linha linha = (LinhasController.Linha)Random.Range(0, 3);
            float x = LinhasController.Instance.PosicaoX(linha);

            //Random.value < 0.5f ? distanciaVertical : -distanciaVertical;
            float offsetY = Random.Range(distanciaVertical * 0.8f, distanciaVertical * 1.4f);

            float y = jogador.position.y + (offsetY);

            Vector3 posicao = new Vector3(x, y, 0);
            if (PodeSpawnar(posicao, 3f)) //raio mínimo entre inimigos
            {
                GameObject inimigo = Instantiate(prefabInimigo, posicao, Quaternion.identity);

                InimigoLinha script = inimigo.GetComponent<InimigoLinha>();
                if (script != null)
                {
                    script.linhaAtual = linha;
                    script.direcao = offsetY > 0 ?
                        InimigoLinha.DirecaoMovimento.Descendo :
                        InimigoLinha.DirecaoMovimento.Subindo;
                }
            }
            else
            {
                Debug.Log("Spawn cancelado: inimigo já próximo");
            }

            bool PodeSpawnar(Vector3 posicaoDesejada, float raio)
            {
                Collider2D[] colisores = Physics2D.OverlapCircleAll(posicaoDesejada, raio, layerInimigos);
                return colisores.Length == 0;
            }

        }
    }
}
