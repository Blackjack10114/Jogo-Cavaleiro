using UnityEngine;

public class Barra_Progresso : MonoBehaviour
{
    public RectTransform iconUI;
    public RectTransform progressBar;
    public int[] metasKills = new int[3]; // 3 metas

    private PlayerAtaque numeroKills;
    private GameObject player;

    private int indiceMetaAtual = 0;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        numeroKills = player.GetComponent<PlayerAtaque>();
    }

    private void Update()
    {
        AtualizarBarra();
        VerificarProgressoMeta();
    }

    private void AtualizarBarra()
    {
        if (numeroKills == null || metasKills.Length == 0 || indiceMetaAtual >= metasKills.Length)
            return;

        float alturaTotal = progressBar.rect.height;

        int kills = numeroKills.kills;
        int metaAtual = metasKills[indiceMetaAtual];

        float progresso = Mathf.Clamp01((float)kills / metaAtual);

        // Calcula posição vertical
        float y = progresso * alturaTotal;

        Vector2 pos = iconUI.anchoredPosition;
        pos.y = y;
        iconUI.anchoredPosition = pos;
    }

    private void VerificarProgressoMeta()
    {
        if (numeroKills == null || indiceMetaAtual >= metasKills.Length)
            return;

        int metaAtual = metasKills[indiceMetaAtual];

        if (numeroKills.kills >= metaAtual)
        {
            numeroKills.kills = 0; // Reseta kills
            indiceMetaAtual++;     // Avança para próxima meta
        }
    }
}
