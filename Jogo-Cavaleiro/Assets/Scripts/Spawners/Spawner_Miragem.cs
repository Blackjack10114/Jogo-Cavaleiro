using UnityEngine;

public class Spawner_Miragem : MonoBehaviour
{
    [Header("Configura��o do Spawner")]
    public GameObject prefabMiragem;
    public Transform jogador;
    public float intervaloEntreSpawns = 1.5f;
    private float tempoProximoSpawn = 0f;
    public float chanceSpawn = 0.5f; // 50%
    public float distanciaVertical = 10f;

    [Header("Layer")]
    public LayerMask layerInimigos;

    [Header("La�o Rosa")]
    [Range(0f, 1f)] public float chanceDeLaco = 0.3f;
    public float tempoAutoDestruirComLaco = 2.5f;

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
        int quantidade = 1;

        for (int i = 0; i < quantidade; i++)
        {
            LinhasController.Linha linha = (LinhasController.Linha)Random.Range(0, 3);
            float x = LinhasController.Instance.PosicaoX(linha);
            float offsetY = Random.Range(distanciaVertical * 0.8f, distanciaVertical * 1.4f);
            float y = jogador.position.y + offsetY;

            Vector3 posicao = new Vector3(x, y, 0);

            if (PodeSpawnar(posicao, 3f))
            {
                GameObject inimigo = Instantiate(prefabMiragem, posicao, Quaternion.identity);

                var script = inimigo.GetComponent<Inimigo_Miragem>();
                if (script != null)
                {
                    script.chancelaco = chanceDeLaco;
                    script.AutoDestruircomlaco = tempoAutoDestruirComLaco;
                }
            }
            else
            {
                Debug.Log("Spawn cancelado: inimigo j� pr�ximo");
            }
        }
    }

    bool PodeSpawnar(Vector3 posicaoDesejada, float raio)
    {
        Collider2D[] colisores = Physics2D.OverlapCircleAll(posicaoDesejada, raio, layerInimigos);
        return colisores.Length == 0;
    }

}

