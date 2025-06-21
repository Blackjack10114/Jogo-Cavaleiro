using UnityEngine;

public class SpawnerUrsinho : MonoBehaviour
{
    public GameObject prefabUrsinho;
    public Transform jogador;
    public float intervalo = 8f;
    public float chanceSpawn = 0.3f;
    public LayerMask camadaInimigos;
    public float distanciaVerificacao = 2f;

    [Header("Laço Rosa")]
    [Range(0f, 1f)] public float chanceDeLaco = 0.3f;
    public float tempoAutoDestruirComLaco = 10f;

    private float tempoProximoSpawn = 0f;

    void Update()
    {
        if (Time.time >= tempoProximoSpawn && jogador != null)
        {
            if (Random.value < chanceSpawn && QuantidadeUrsinhos() < 1)
            {
                Spawn();
            }

            tempoProximoSpawn = Time.time + intervalo;
        }
    }

    void Spawn()
    {
        LinhasController.Linha linha = (LinhasController.Linha)Random.Range(0, 3);
        float x = LinhasController.Instance.PosicaoX(linha);
        float y = jogador.position.y + Random.Range(10f, 13f);
        Vector3 posicao = new Vector3(x, y, 0f);

        if (PodeSpawnar(posicao))
        {
            GameObject ursinho = Instantiate(prefabUrsinho, posicao, Quaternion.identity);
            ursinho.tag = "Ursinho";

            var script = ursinho.GetComponent<Inimigo_Ursinho>();
            if (script != null)
            {
                script.chancelaco = chanceDeLaco;
                script.AutoDestruircomlaco = tempoAutoDestruirComLaco;
            }
        }
    }

    bool PodeSpawnar(Vector3 posicao)
    {
        Collider2D[] colisores = Physics2D.OverlapCircleAll(posicao, distanciaVerificacao, camadaInimigos);
        return colisores.Length == 0;
    }

    int QuantidadeUrsinhos()
    {
        return GameObject.FindGameObjectsWithTag("Ursinho").Length;
    }
}
