using UnityEngine;

public class SpawnerUnicornio : MonoBehaviour
{
    [Header("Configuração do Spawner")]
    public GameObject prefabUnicornio;
    public float intervaloEntreSpawns = 3f;
    public float alturaSpawn = 10f;
    public bool spawnDeCima = true;

    [Header("Distância mínima para não sobrepor")]
    public float distanciaMinimaEntreInimigos = 1.5f;

    [Header("Layer")]
    public LayerMask layerInimigos;

    private float tempoProximoSpawn = 0f;

    void Update()
    {
        if (Time.time >= tempoProximoSpawn)
        {
            SpawnUnicornio();
            tempoProximoSpawn = Time.time + intervaloEntreSpawns;
        }
    }

    void SpawnUnicornio()
    {
        // Linha aleatória (0 = Esquerda, 1 = Centro, 2 = Direita)
        LinhasController.Linha linha = (LinhasController.Linha)Random.Range(0, 3);
        float x = LinhasController.Instance.PosicaoX(linha);

        // Posição vertical: cima ou baixo
        Transform player = GameObject.FindWithTag("Player")?.transform;
        if (player == null) return;

        float y = player.position.y + Random.Range(alturaSpawn * 0.8f, alturaSpawn * 1.4f);


        Vector3 posicao = new Vector3(x, y, 0f);

        // Verifica se a posição está livre
        if (PodeSpawnar(posicao, distanciaMinimaEntreInimigos))
        {
            GameObject unic = Instantiate(prefabUnicornio, posicao, Quaternion.identity);

            // Atribui a linha corretamente
            Unicornio script = unic.GetComponent<Unicornio>();
            if (script != null)
            {
                script.linhaAtual = linha;
            }
        }
        else
        {
            Debug.Log("Unicórnio NÃO spawnado: posição ocupada");
        }
    }
    bool PodeSpawnar(Vector3 posicaoDesejada, float raio)
    {
        Collider2D[] colisores = Physics2D.OverlapCircleAll(posicaoDesejada, raio, layerInimigos);
        return colisores.Length == 0;
    }
}
