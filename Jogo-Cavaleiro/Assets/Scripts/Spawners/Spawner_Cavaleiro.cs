using UnityEngine;
using System.Collections.Generic;

public class Spawner_Cavaleiro : MonoBehaviour
{
    public GameObject prefabCavaleiro;
    public Transform jogador;
    public float intervalo = 5f;
    public float chanceSpawn = 0.5f;
    public int maximoCavaleiros = 2;

    public float distanciaVerificacao = 2f;
    public LayerMask camadaInimigos;

    [Header("Controle do Laço")]
    [Range(0f, 1f)] public float chanceDeLaco = 0.3f;
    public GameObject prefabLacoRosa;
    public float tempoAutoDestruirComLaco = 10f;

    private float proximoSpawn = 0f;

    void Update()
    {
        if (Time.time >= proximoSpawn && jogador != null)
        {
            if (Random.value < chanceSpawn && QuantidadeCavaleirosAtivos() < maximoCavaleiros)
            {
                SpawnCavaleiro();
            }

            proximoSpawn = Time.time + intervalo;
        }
    }

    void SpawnCavaleiro()
    {
        List<LinhasController.Linha> linhasDisponiveis = new List<LinhasController.Linha>
        {
            LinhasController.Linha.Esquerda,
            LinhasController.Linha.Centro,
            LinhasController.Linha.Direita
        };

        foreach (GameObject cavaleiro in GameObject.FindGameObjectsWithTag("Cavaleiro"))
        {
            float xCav = cavaleiro.transform.position.x;
            foreach (var linha in linhasDisponiveis.ToArray())
            {
                float xLinha = LinhasController.Instance.PosicaoX(linha);
                if (Mathf.Abs(xCav - xLinha) < 0.1f)
                    linhasDisponiveis.Remove(linha);
            }
        }

        if (linhasDisponiveis.Count == 0) return;

        LinhasController.Linha linhaEscolhida = linhasDisponiveis[Random.Range(0, linhasDisponiveis.Count)];
        float x = LinhasController.Instance.PosicaoX(linhaEscolhida);
        float y = jogador.position.y - Random.Range(9f, 13f);

        Vector3 posicao = new Vector3(x, y, 0f);

        if (!PodeSpawnar(posicao)) return;

        GameObject novoCavaleiro = Instantiate(prefabCavaleiro, posicao, Quaternion.identity);
        novoCavaleiro.tag = "Cavaleiro";

        Inimigo_Cavaleiro script = novoCavaleiro.GetComponent<Inimigo_Cavaleiro>();
        if (script != null)
        {
            script.chancelaco = chanceDeLaco;
            script.AutoDestruircomlaco = tempoAutoDestruirComLaco;
            script.prefabLaco = prefabLacoRosa;
        }
    }

    bool PodeSpawnar(Vector3 posicao)
    {
        Collider2D[] colisores = Physics2D.OverlapCircleAll(posicao, distanciaVerificacao, camadaInimigos);
        return colisores.Length == 0;
    }

    int QuantidadeCavaleirosAtivos()
    {
        return GameObject.FindGameObjectsWithTag("Cavaleiro").Length;
    }
}
