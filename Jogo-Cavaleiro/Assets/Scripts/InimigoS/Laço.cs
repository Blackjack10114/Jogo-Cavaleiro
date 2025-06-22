using UnityEngine;
using System.Collections;

public class Laço : MonoBehaviour
{
    private Vida vidaDoInimigo;
    private bool puniu = false;

    // Exclusivo do Fantasma
    private Inimigo_Fantasma fantasma;
    private SpriteRenderer sr;
    private Color corOriginal;
    private bool fezfade;

    void Start()
    {
        Transform dono = transform.parent;
        if (dono == null) return;

        vidaDoInimigo = dono.GetComponent<Vida>();

        // Se o inimigo for um fantasma, ativa visual de fade
        fantasma = dono.GetComponent<Inimigo_Fantasma>();
        if (fantasma != null)
        {
            sr = GetComponent<SpriteRenderer>();
            if (sr != null)
                corOriginal = sr.color;
        }
    }

    void Update()
    {
        // 🎭 Apenas para o fantasma: faz o fade visual
        if (fantasma != null)
        {
            if (fantasma.Comecarfade && !fezfade)
                StartCoroutine(FadeOut());

            if (fantasma.podeatacar && sr != null)
                sr.color = new Color(corOriginal.r, corOriginal.g, corOriginal.b, 255f);
        }

        // 💥 Punição global se o inimigo morrer (atacado)
        if (vidaDoInimigo != null && vidaDoInimigo.Morreu && !puniu)
        {
            AplicarPuniçãoAoJogador();
            puniu = true;
        }
    }

    IEnumerator FadeOut()
    {
        fezfade = true;
        float tempo = 0f;

        while (tempo < fantasma.duracaoDoFade)
        {
            float alpha = Mathf.Lerp(1f, 0f, tempo / fantasma.duracaoDoFade);
            sr.color = new Color(corOriginal.r, corOriginal.g, corOriginal.b, alpha);
            tempo += Time.deltaTime;
            yield return null;
        }

        sr.color = new Color(corOriginal.r, corOriginal.g, corOriginal.b, 0f);
    }

    void AplicarPuniçãoAoJogador()
    {
        GameObject jogador = GameObject.FindWithTag("Player");
        if (jogador != null)
        {
            Vida vidaJogador = jogador.GetComponent<Vida>();
            if (vidaJogador != null)
            {
                vidaJogador.LevarDano(1);
                Debug.Log("⚠️ Jogador atacou um inimigo com laço! Perdeu 1 de vida.");
            }
        }
    }
}
