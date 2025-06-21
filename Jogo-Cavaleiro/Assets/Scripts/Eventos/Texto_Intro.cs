using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Texto_Intro : MonoBehaviour
{
    public string CenaMenu;

    [Header("Referências")]
    [SerializeField] private TextMeshProUGUI texto;

    [Header("Configurações de Frases")]
    [TextArea]
    public string[] frases;

    [Header("Estilo de Texto")]
    public bool negritoAtivado = false;

    private int indiceAtual = 0;
    private bool terminou = false;

    void Start()
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

    void Update()
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
                    StartCoroutine(PassarParaMenu());
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

    private IEnumerator PassarParaMenu()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(CenaMenu);
    }
}
