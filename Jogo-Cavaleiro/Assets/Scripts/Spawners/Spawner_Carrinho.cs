using UnityEngine;

public class SpawnerCarrinhoAlien : MonoBehaviour
{
    public GameObject prefabCarrinho;
    public GameObject prefabAviso;
    public float alturaAviso = 12f;
    public Transform jogador;
    public float intervaloEntreSpawns = 5f;
    public float chanceSpawn = 0.25f;
    public float alturaSpawn = 30f;
    public LayerMask layerInimigos;
    public float distanciaVerificacao = 2f;
    public int maximoCarrinhos = 2;
    public Vector3 offsetaviso;

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
        // Tenta no máximo 3 vezes (uma por linha)
        for (int tentativas = 0; tentativas < 3; tentativas++)
        {
            LinhasController.Linha linha = (LinhasController.Linha)Random.Range(0, 3);
            float x = LinhasController.Instance.PosicaoX(linha);
            float y = jogador.position.y + alturaSpawn;
            Vector3 posicao = new Vector3(x, y, 0f);

            if (PodeSpawnarNaLinha(linha))
            {
                // AVISO
                if (prefabAviso != null)
                {
                    Vector3 posicaoAviso = new Vector3(x, y - alturaAviso, 0f);
                    GameObject aviso = Instantiate(prefabAviso, posicaoAviso + offsetaviso, Quaternion.identity);
                    Destroy(aviso, 3f);
                }

                // DELAY E CARRINHO
                StartCoroutine(SpawnComDelay(2f, posicao));
                break; // Sai do loop após sucesso
            }
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

    bool PodeSpawnarNaLinha(LinhasController.Linha linha)
    {
        float x = LinhasController.Instance.PosicaoX(linha);
        float y = jogador.position.y + alturaSpawn;
        Vector3 posicao = new Vector3(x, y, 0f);

        // 1. Verifica se já existe carrinho nesta linha
        GameObject[] carrinhos = GameObject.FindGameObjectsWithTag("Carrinho");
        foreach (GameObject carrinho in carrinhos)
        {
            float carrinhoX = carrinho.transform.position.x;
            if (Mathf.Approximately(carrinhoX, x))
            {
                return false; // Linha ocupada
            }
        }

        // 2. Verifica colisores na área
        Collider2D[] colisores = Physics2D.OverlapCircleAll(posicao, distanciaVerificacao, layerInimigos);
        if (colisores.Length > 0)
        {
            return false; // Tem obstáculo/inimigo no local
        }

        return true; // Linha livre e sem sobreposição
    }
   

}
