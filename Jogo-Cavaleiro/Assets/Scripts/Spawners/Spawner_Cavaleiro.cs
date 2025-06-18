using UnityEngine;
using System.Collections.Generic;

public class Spawner_Cavaleiro : MonoBehaviour
{
    public GameObject prefabCavaleiro;
    public Transform jogador;
    public float intervalo = 5f;
    public float chanceSpawn = 0.5f; // 50%
    public int maximoCavaleiros = 2;

    public float distanciaVerificacao = 2f;
    public LayerMask camadaInimigos;

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

        // Remove linhas já ocupadas por outros cavaleiros
        foreach (GameObject cav in GameObject.FindGameObjectsWithTag("Cavaleiro"))
        {
            float xCav = cav.transform.position.x;

            foreach (var linha in linhasDisponiveis.ToArray())
            {
                float xLinha = LinhasController.Instance.PosicaoX(linha);
                if (Mathf.Abs(xCav - xLinha) < 0.1f) // margem de erro
                    linhasDisponiveis.Remove(linha);
            }
        }

        if (linhasDisponiveis.Count == 0) return; // nenhuma linha disponível

        // Escolhe uma linha restante aleatória
        LinhasController.Linha linhaEscolhida = linhasDisponiveis[Random.Range(0, linhasDisponiveis.Count)];
        float x = LinhasController.Instance.PosicaoX(linhaEscolhida);
        float y = jogador.position.y - Random.Range(9f, 13f); // nascendo abaixo

        Vector3 posicao = new Vector3(x, y, 0f);

        if (PodeSpawnar(posicao))
        {
            GameObject cav = Instantiate(prefabCavaleiro, posicao, Quaternion.identity);
            cav.tag = "Cavaleiro"; // necessário para a verificação funcionar
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
