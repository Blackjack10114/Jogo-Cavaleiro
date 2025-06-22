using UnityEngine;
using TMPro;

public class TextoObjetivo : MonoBehaviour
{
    public TextMeshProUGUI textoUI;
    private ControladorNarrativa narrativa;

    void Start()
    {
        narrativa = ControladorNarrativa.Instance;
    }

    void Update()
    {
        if (narrativa == null) return;

        int etapa = narrativa.EtapaAtual();
        int[] metas = narrativa.Metas();

        if (etapa >= metas.Length) return;

        int metaAtual = metas[etapa];
        int faltam = narrativa.FaltamParaProximaMeta();

        textoUI.text = $"Objetivo: Derrote os inimigos\nFaltam: {faltam}";

    }
}
