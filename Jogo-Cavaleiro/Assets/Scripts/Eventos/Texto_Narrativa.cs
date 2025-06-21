using TMPro;
using UnityEngine;

public class TextoNarrativa : MonoBehaviour
{
    public static TextoNarrativa Instance;

    public TextMeshProUGUI textoUI;
    public GameObject painelFala;
    public float duracaoTexto = 4f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void Narrador(string frase)
    {
        MostrarTexto("Narrador: " + frase);
    }

    public void NarradorPai(string frase)
    {
        MostrarTexto("Narrador (Pai): " + frase);
    }

    public void Crianca(string frase)
    {
        MostrarTexto("Criança: " + frase);
    }
    public void Mae(string frase)
    {
        MostrarTexto("Mãe: " + frase);
    }

    public void MostrarTexto(string frase)
    {
        painelFala.SetActive(true);
        textoUI.text = frase;
        CancelInvoke(nameof(EsconderTexto));
        Invoke(nameof(EsconderTexto), duracaoTexto);
    }

    void EsconderTexto()
    {
        painelFala.SetActive(false);
    }
}
