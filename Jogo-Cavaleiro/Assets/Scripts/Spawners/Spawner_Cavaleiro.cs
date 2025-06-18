using UnityEngine;

public class Spawner_Cavaleiro : MonoBehaviour
{
    public GameObject prefabCavaleiro;
    public float intervalo = 5f;
    private float proximoSpawn = 0f;
    public Transform jogador;

    public float distanciaVerificacao = 3f; // raio de verificação
    public LayerMask camadaInimigos;        // layer dos inimigos (ex: "Inimigo")


    void Update()
    {
        if (Time.time >= proximoSpawn)
        {
            Spawn();
            proximoSpawn = Time.time + intervalo;
        }
    }

    void Spawn()
    {
        if (jogador == null) return;

        // Escolhe uma linha aleatória (esquerda, centro ou direita)
        int index = Random.Range(0, 3);
        LinhasController.Linha linhaEscolhida = (LinhasController.Linha)index;

        float x = LinhasController.Instance.PosicaoX(linhaEscolhida);
        float y = jogador.position.y - Random.Range(9f, 13f); // nasce de 9 a 13 unidades abaixo


        Vector3 posicao = new Vector3(x, y, 0f);

        if (PodeSpawnar(posicao))
        {
            GameObject cavaleiro = Instantiate(prefabCavaleiro, posicao, Quaternion.identity);
        }
    }


    bool PodeSpawnar(Vector3 posicao)
    {
        Collider2D[] colisores = Physics2D.OverlapCircleAll(posicao, distanciaVerificacao, camadaInimigos);
        return colisores.Length == 0;
    }

}
