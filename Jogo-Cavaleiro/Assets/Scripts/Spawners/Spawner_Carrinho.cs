using UnityEngine;

public class SpawnerCarrinhoAlien : MonoBehaviour
{
    public GameObject prefabCarrinho;
    public GameObject prefabAviso; 
    public Transform jogador;
    public float intervaloEntreSpawns = 5f;
    public float chanceSpawn = 0.25f;
    public float alturaSpawn = 12f;
    public LayerMask layerInimigos;
    public float distanciaVerificacao = 2f;
    public int maximoCarrinhos = 2;

    private float tempoProximoSpawn = 0f;

    void Update()
    {
        if (Time.time >= tempoProximoSpawn && jogador != null)
        {
            if (Random.value < chanceSpawn && QuantidadeCarrinhosAtivos() < maximoCarrinhos)
            {
                SpawnCarrinho();
            }

            tempoProximoSpawn = Time.time + intervaloEntreSpawns;
        }
    }

    void SpawnCarrinho()
    {
        // Escolhe linha aleatória
        LinhasController.Linha linha = (LinhasController.Linha)Random.Range(0, 3);
        float x = LinhasController.Instance.PosicaoX(linha);
        float y = jogador.position.y + Random.Range(alturaSpawn * 0.8f, alturaSpawn * 1.3f);

        Vector3 posicao = new Vector3(x, y, 0f);

        if (PodeSpawnar(posicao))
        {
            // Primeiro o aviso (!)
            if (prefabAviso != null)
            {
                GameObject aviso = Instantiate(prefabAviso, posicao, Quaternion.identity);
                Destroy(aviso, 0.6f); // destrói o aviso após 0.6 segundos
            }

            // Depois de um pequeno atraso, instancia o carrinho
            StartCoroutine(SpawnComDelay(0.6f, posicao));
        }
    }

    System.Collections.IEnumerator SpawnComDelay(float delay, Vector3 pos)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(prefabCarrinho, pos, Quaternion.identity);
    }

    int QuantidadeCarrinhosAtivos()
    {
        return GameObject.FindGameObjectsWithTag("Inimigo").Length;
    }

    bool PodeSpawnar(Vector3 posicao)
    {
        Collider2D[] colisores = Physics2D.OverlapCircleAll(posicao, distanciaVerificacao, layerInimigos);
        return colisores.Length == 0;
    }
}
