using UnityEngine;
using UnityEngine.UI;

public class Hud_Coracoes : MonoBehaviour
{
    public Vida vidaJogador;

    public Image[] coracoes;
    public Sprite coracaoCheio;
    public Sprite coracaoVazio;

    void Update()
    {
        if (vidaJogador == null) return;

        int vidaAtual = Mathf.Clamp(vidaJogador.VidaAtual(), 0, coracoes.Length);

        for (int i = 0; i < coracoes.Length; i++)
        {
            if (i < vidaAtual)
                coracoes[i].sprite = coracaoCheio;
            else
                coracoes[i].sprite = coracaoVazio;
        }
    }
}
