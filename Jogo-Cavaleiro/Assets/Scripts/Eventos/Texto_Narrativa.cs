using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TextoNarrativa : MonoBehaviour
{
    public static TextoNarrativa Instance;

    public TextMeshProUGUI textoUI;
    public GameObject painelFala;

    private bool aguardandoInput = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void MostrarTexto(string frase)
    {
        painelFala.SetActive(true);
        textoUI.text = frase;
        aguardandoInput = true;
    }

    public void Narrador(string frase) => MostrarTexto("Narrador: " + frase);
    public void NarradorPai(string frase) => MostrarTexto("Narrador (Pai): " + frase);
    public void Crianca(string frase) => MostrarTexto("Crian�a: " + frase);
    public void Mae(string frase) => MostrarTexto("M�e: " + frase);

    // Esta fun��o ser� chamada pelo bot�o via PlayerInput
    public void OnAvancarTexto(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (aguardandoInput)
        {
            painelFala.SetActive(false);
            aguardandoInput = false;
        }
    }

    // Voc� pode usar isso nos scripts de narrativa
    public bool EstaMostrandoTexto()
    {
        return aguardandoInput;
    }
}
