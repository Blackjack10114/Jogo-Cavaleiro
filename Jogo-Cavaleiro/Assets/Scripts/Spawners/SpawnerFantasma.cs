using UnityEngine;

public class SpawnerFantasma : MonoBehaviour
{
    [Header("Configura��o do Spawner")]
    public GameObject prefabfantasma;
    public Transform jogador;
    public float intervaloEntreSpawns = 1.5f;
    private float tempoProximoSpawn = 0f;
    public float chanceSpawn = 0.2f; // 20%
    public float distanciaVertical = 10f;

    [Header("Layer")]
    public LayerMask layerInimigos;

    [Header("La�o Rosa")]
    [Range(0f, 1f)] public float chanceDeLaco = 0.3f;
    public float tempoAutoDestruirComLaco = 10f;

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
            // linha aleat�ria
            LinhasController.Linha linha = (LinhasController.Linha)Random.Range(0, 3);
            float x = LinhasController.Instance.PosicaoX(linha);

            //Random.value < 0.5f ? distanciaVertical : -distanciaVertical;
            float offsetY = distanciaVertical;
            float y = jogador.position.y + (offsetY);

            Vector3 posicao = new Vector3(x, y, 0);
            if (PodeSpawnar(posicao, 3f, "Fantasma")) //raio m�nimo entre inimigos
            {
                GameObject inimigo = Instantiate(prefabfantasma, posicao, Quaternion.identity);

                var verLaco = inimigo.GetComponent<Inimigo_Fantasma>();
                if (verLaco != null)
                {
                    verLaco.chancelaco = chanceDeLaco;
                    verLaco.AutoDestruircomlaco = tempoAutoDestruirComLaco;
                }


                Inimigo_Piolho script = inimigo.GetComponent<Inimigo_Piolho>();
                if (script != null)
                {
                    script.linhaAtual = linha;
                    script.direcao = offsetY > 0 ?
                        Inimigo_Piolho.DirecaoMovimento.Descendo :
                        Inimigo_Piolho.DirecaoMovimento.Subindo;
                }
            }
            else
            {
                Debug.Log("Spawn cancelado: inimigo j� pr�ximo");
            }

            bool PodeSpawnar(Vector3 posicaoDesejada, float raio, string tagAlvo)
            {
                Collider2D[] colisores = Physics2D.OverlapCircleAll(posicaoDesejada, raio, layerInimigos);
                foreach (var col in colisores)
                {
                    if (col.CompareTag(tagAlvo))
                        return false;
                }
                return true;
            }

        }
    }
}
