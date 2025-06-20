using UnityEngine;
using System.Collections;

public class Laço : MonoBehaviour
{
    private Transform fantasma;
    private Inimigo_Fantasma ver_laco;
    private SpriteRenderer sr;
    private Color corOriginal;
    private bool fezfade;
    void Start()
    {
        fantasma = transform.parent;
        ver_laco = fantasma.GetComponent<Inimigo_Fantasma>();
        sr = GetComponent<SpriteRenderer>();
        corOriginal = sr.color;
    }

    void Update()
    {
        if (ver_laco != null)
        {
            if (ver_laco.Comecarfade && !fezfade)
            {
                StartCoroutine(FadeOut());
            }
            if (ver_laco.podeatacar)
            {
                sr.color = new Color(corOriginal.r, corOriginal.g, corOriginal.b, 255f);
            }
        }
        else return;
    }
    IEnumerator FadeOut()
    {
        fezfade = true;
        sr = GetComponent<SpriteRenderer>();
        corOriginal = sr.color;

        float tempo = 0f;

        while (tempo < ver_laco.duracaoDoFade)
        {
            float alpha = Mathf.Lerp(1f, 0f, tempo / ver_laco.duracaoDoFade);
            sr.color = new Color(corOriginal.r, corOriginal.g, corOriginal.b, alpha);

            tempo += Time.deltaTime;
            yield return null;
        }
        sr.color = new Color(corOriginal.r, corOriginal.g, corOriginal.b, 0f);
    }
}
