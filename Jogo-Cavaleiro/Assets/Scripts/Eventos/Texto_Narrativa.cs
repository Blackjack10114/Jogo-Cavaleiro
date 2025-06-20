using UnityEngine;
using TMPro;

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
