using UnityEngine;
using UnityEngine.UI;

public class Hud_Vida : MonoBehaviour
{
    public Vida vidaJogador;
    public TMPro.TextMeshProUGUI textoHUD;

    
    void Update()
    {
        if (vidaJogador != null)
        {
            textoHUD.text = "Vida: " + vidaJogador.VidaAtual();
        }
    }
}
