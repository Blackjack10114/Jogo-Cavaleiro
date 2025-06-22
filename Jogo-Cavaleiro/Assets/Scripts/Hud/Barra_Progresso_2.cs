using UnityEngine;

public class Barra_Progresso_2 : MonoBehaviour
{
    public RectTransform iconUI;
    public RectTransform progressBar;


    private ControladorNarrativa narrativa;


    private PlayerAtaque numeroKills;
    private GameObject player;


    private void Start()
    {
        narrativa = ControladorNarrativa.Instance;
    }

    private void Update()
    {
        AtualizarBarra();
    }

    private void AtualizarBarra()
    {
        if (narrativa == null) return;

        int etapa = narrativa.EtapaAtual();
        int[] metas = narrativa.Metas();
        int kills = narrativa.Kills();

        if (etapa >= metas.Length) return;

        // Soma das metas anteriores
        int somaMetasAnteriores = 0;
        for (int i = 0; i < etapa; i++)
        {
            somaMetasAnteriores += metas[i];
        }

        int killsEtapaAtual = kills - somaMetasAnteriores;
        int metaAtual = metas[etapa];

        float progressoEtapaAtual = Mathf.Clamp01((float)killsEtapaAtual / metaAtual);

        // Barra sobe apenas dentro da etapa atual
        float alturaTotal = progressBar.rect.height;
        float y = progressoEtapaAtual * alturaTotal;

        Vector2 pos = iconUI.anchoredPosition;
        pos.y = y;
        iconUI.anchoredPosition = pos;

        // DEBUG opcional
        Debug.Log($"[BarraProgresso] Etapa: {etapa} | Kills: {kills} | Meta: {metaAtual} | Progresso: {progressoEtapaAtual}");
    }
}


    /* private void AtualizarBarra()
     {
         if (narrativa == null) return;

         int etapa = narrativa.EtapaAtual();
         int[] metas = narrativa.Metas();
         int kills = narrativa.Kills();

         if (etapa >= metas.Length) return;

         // Soma das metas anteriores
         int somaMetasAnteriores = 0;
         for (int i = 0; i < etapa; i++)
         {
             somaMetasAnteriores += metas[i];
         }

         int killsEtapaAtual = kills - somaMetasAnteriores;
         int metaAtual = metas[etapa];

         float progressoEtapaAtual = Mathf.Clamp01((float)killsEtapaAtual / metaAtual);

         // Progresso total = etapas completas + parte atual
         float progressoTotal = etapa + progressoEtapaAtual;

         // Altura proporcional à quantidade total de etapas
         float alturaTotal = progressBar.rect.height;
         float maxEtapas = metas.Length;

         float y = (progressoTotal / maxEtapas) * alturaTotal;

         Vector2 pos = iconUI.anchoredPosition;
         pos.y = y;
         iconUI.anchoredPosition = pos;
     }
    */


/*private void AtualizarBarra()
{
    if (narrativa == null) return;

    int etapa = narrativa.EtapaAtual();
    int[] metas = narrativa.Metas();
    int kills = narrativa.Kills();

    if (etapa >= metas.Length) return;

    int metaAtual = metas[etapa];
    float alturaTotal = progressBar.rect.height;

    float progresso = Mathf.Clamp01((float)kills / metaAtual);
    float y = progresso * alturaTotal;

    Vector2 pos = iconUI.anchoredPosition;
    pos.y = y;
    iconUI.anchoredPosition = pos;
}
*/





