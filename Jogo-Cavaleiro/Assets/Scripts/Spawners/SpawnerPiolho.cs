using UnityEngine;

public class SpawnerDinâmico : MonoBehaviour
{
    public GameObject prefabInimigo;
    public Transform jogador;
    public float intervaloEntreSpawns = 1.5f;

    public float distanciaVertical = 10f;
    public float chanceSpawn = 0.5f; // 50%

    private float tempoProximoSpawn = 0f;

    public int minimoPorSpawn = 1;
    public int maximoPorSpawn = 3;

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
            float offsetY = distanciaVertical;
            float y = jogador.position.y + (offsetY);

            Vector3 posicao = new Vector3(x, y, 0);
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
    }
}
