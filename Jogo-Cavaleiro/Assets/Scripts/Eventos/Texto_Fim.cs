using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Texto_Fim : MonoBehaviour
{
    public string CenaMenu;

    [Header("Referências")]
    [SerializeField] private TextMeshProUGUI texto;
    [SerializeField] private Image fadeImage;

    [Header("Configurações de Frases")]
    [TextArea]
    public string[] frases;

    [Header("Estilo de Texto")]
    public bool negritoAtivado = false;

    private int indiceAtual = 0;
    private bool terminou = false;

    private void Start()
    {
        if (frases.Length > 0 && texto != null)
        {
            AtualizarTexto();

            if (frases.Length == 1)
            {
                Debug.Log("Exibindo última frase.");
            }
        }
    }

    private void Update()
    {
        if (frases.Length == 0 || texto == null || terminou) return;

        if (Input.anyKeyDown)
        {
            indiceAtual++;

            if (indiceAtual < frases.Length)
            {
                AtualizarTexto();

                if (indiceAtual == frases.Length - 2)
                {
                    negritoAtivado = true;
                }
                if (indiceAtual == frases.Length - 1)
                {
                    negritoAtivado = false;
                    Debug.Log("Exibindo última frase.");
                    StartCoroutine(PassarParaMenuComFade());
                }
            }
            else
            {
                terminou = true;
            }
        }
    }

    private void AtualizarTexto()
    {
        texto.text = frases[indiceAtual];
        texto.fontStyle = negritoAtivado ? FontStyles.Bold : FontStyles.Normal;
    }

    private IEnumerator PassarParaMenuComFade()
    {
        float duration = 8f;
        float t = 0f;

        Color c = fadeImage.color;

        while (t < duration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, t / duration);
            fadeImage.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        // Reseta progresso salvo
        PlayerPrefs.DeleteKey("ChaveFase");
        PlayerPrefs.DeleteKey("ChaveKills");
        PlayerPrefs.DeleteKey("ChaveY");

        SceneManager.LoadScene(CenaMenu);
    }
}
