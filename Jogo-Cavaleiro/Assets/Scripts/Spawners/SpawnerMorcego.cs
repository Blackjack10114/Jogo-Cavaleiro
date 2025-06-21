using UnityEngine;

public class SpawnerMorcego : MonoBehaviour
{
    public GameObject prefabMorcego;
    public Transform jogador;
    private Vector3 PosicaoSpawner;
    public float intervaloEntreSpawns = 2f;

    public float distanciaVertical = 0f;
    public float chanceSpawn = 0.3f;

    [Header("Controle do Laço")]
    public float chanceDeLaco = 0.3f;
    public float tempoAutoDestruirComLaco = 2.5f;

    private float tempoProximoSpawn = 0f;

    void Update()
    {
        PosicaoSpawner = new Vector3(transform.position.x, jogador.transform.position.y, transform.position.z);
        transform.position = PosicaoSpawner;

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
        GameObject inimigo = Instantiate(prefabMorcego, transform.position, Quaternion.identity);

        Morcego morcego = inimigo.GetComponent<Morcego>();
        if (morcego != null)
        {
            morcego.chancelaco = chanceDeLaco;
            morcego.AutoDestruircomlaco = tempoAutoDestruirComLaco;
        }
    }
}
