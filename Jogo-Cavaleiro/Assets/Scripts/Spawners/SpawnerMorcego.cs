using UnityEngine;

public class SpawnerMorcego : MonoBehaviour
{
    public GameObject prefabMorcego;
    public Transform jogador;
    private Vector3 PosicaoSpawner;
    public float intervaloEntreSpawns = 2f;

    public float distanciaVertical = 0f;
    public float chanceSpawn = 0.3f; // 30%

    private float tempoProximoSpawn = 0f;

    void Update()
    {
        PosicaoSpawner = new Vector3(transform.position.x, jogador.transform.position.y, transform.position.z);
        this.transform.position = PosicaoSpawner;
        if (Time.time >= tempoProximoSpawn && jogador != null)
        {
            if (Random.value < chanceSpawn)
            {
                SpawnMorcego();
            }
            tempoProximoSpawn = Time.time + intervaloEntreSpawns;
        }
    }

    void SpawnMorcego()
    {
        GameObject inimigo = Instantiate(prefabMorcego, this.transform.position, Quaternion.identity);
    }
}

